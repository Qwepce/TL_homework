using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Amenities.GetOrCreateAmenities;
using WebAPI.Application.UseCases.RoomServices.GetOrCreateRoomServices;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;

public class CreateRoomTypeCommandHandler : BaseCommandHandlerWithResult<CreateRoomTypeCommand, int>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> _roomServiceCommandHandler;
    private readonly ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> _amenityCommandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomTypeCommandHandler(
        IRoomTypeRepository roomTypeRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<CreateRoomTypeCommand> validator,
        ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> amenityCommandHandler,
        ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> roomServiceCommandHandler )
        : base( validator )
    {
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
        _amenityCommandHandler = amenityCommandHandler;
        _roomServiceCommandHandler = roomServiceCommandHandler;
    }

    protected override async Task<Result<int>> HandleCommand( CreateRoomTypeCommand command, CancellationToken cancellationToken )
    {
        GetOrCreateRoomServicesCommand roomServicesCommand = new()
        {
            RoomServiceNames = command.RoomServices.Distinct( StringComparer.OrdinalIgnoreCase )
        };
        Result<List<RoomService>> roomServicesResult = await _roomServiceCommandHandler.Handle( roomServicesCommand, cancellationToken );
        if ( roomServicesResult.IsFailure )
        {
            return Result<int>.Failure( roomServicesResult.Errors );
        }
        List<RoomService> roomServices = roomServicesResult.Value;

        GetOrCreateAmenitiesCommand amenitiesCommand = new()
        {
            AmenityNames = command.RoomAmenities.Distinct( StringComparer.OrdinalIgnoreCase )
        };
        Result<List<Amenity>> amenitiesResult = await _amenityCommandHandler.Handle( amenitiesCommand, cancellationToken );
        if ( amenitiesResult.IsFailure )
        {
            return Result<int>.Failure( amenitiesResult.Errors );
        }
        List<Amenity> roomAmenities = amenitiesResult.Value;

        RoomType roomType = new(
            command.PropertyId,
            command.Name.Trim(),
            command.DailyPrice,
            Enum.Parse<Currency>( command.Currency ),
            command.MinPersonCount,
            command.MaxPersonCount,
            command.TotalRoomsCount );
        roomType.Amenities = roomAmenities;
        roomType.RoomServices = roomServices;

        await _roomTypeRepository.Add( roomType );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( roomType.Id );
    }
}
