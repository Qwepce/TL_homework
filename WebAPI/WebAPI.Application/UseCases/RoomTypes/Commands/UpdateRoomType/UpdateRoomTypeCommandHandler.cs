using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Amenities.GetOrCreate;
using WebAPI.Application.UseCases.RoomServices.GetOrCreate;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandHandler : BaseCommandHandler<UpdateRoomTypeCommand>
{
    private readonly ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> _amenitiesCommandHandler;
    private readonly ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> _roomServicesCommandHandler;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomTypeCommandHandler(
        ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> amenitiesCommandHandler,
        ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> roomServicesCommandHandler,
        IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork,
        IRequestValidator<UpdateRoomTypeCommand> validator ) : base( validator )
    {
        _amenitiesCommandHandler = amenitiesCommandHandler;
        _roomServicesCommandHandler = roomServicesCommandHandler;
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( UpdateRoomTypeCommand command, CancellationToken cancellationToken )
    {
        RoomType roomType = await _roomTypeRepository.GetById( command.RoomTypeId );

        GetOrCreateAmenitiesCommand amenitiesCommand = new()
        {
            AmenityNames = command.RoomAmenities.Distinct( StringComparer.OrdinalIgnoreCase )
        };
        Result<List<Amenity>> amenitiesResult = await _amenitiesCommandHandler.Handle( amenitiesCommand, cancellationToken );
        if ( amenitiesResult.IsFailure )
        {
            return Result.Failure( amenitiesResult.Errors );
        }
        List<Amenity> amenities = amenitiesResult.Value;

        GetOrCreateRoomServicesCommand roomServicesCommand = new()
        {
            RoomServiceNames = command.RoomServices.Distinct( StringComparer.OrdinalIgnoreCase )
        };
        Result<List<RoomService>> roomServicesResult = await _roomServicesCommandHandler.Handle( roomServicesCommand, cancellationToken );
        if ( roomServicesResult.IsFailure )
        {
            return Result.Failure( roomServicesResult.Errors );
        }
        List<RoomService> roomServices = roomServicesResult.Value;

        roomType.Amenities = amenities;
        roomType.RoomServices = roomServices;

        command.Adapt( roomType );
        await _roomTypeRepository.Update( roomType );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}