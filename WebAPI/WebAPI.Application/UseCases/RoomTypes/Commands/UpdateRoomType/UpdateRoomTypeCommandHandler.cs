using FluentValidation;
using FluentValidation.Results;
using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Amenities.GetOrCreate;
using WebAPI.Application.UseCases.RoomServices.GetOrCreate;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandHandler : ICommandHandler<UpdateRoomTypeCommand>
{
    private readonly ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> _amenitiesCommandHandler;
    private readonly ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> _roomServicesCommandHandler;
    private readonly IValidator<UpdateRoomTypeCommand> _validator;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomTypeCommandHandler(
        ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> amenitiesCommandHandler,
        ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> roomServicesCommandHandler,
        IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork,
        IValidator<UpdateRoomTypeCommand> validator )
    {
        _amenitiesCommandHandler = amenitiesCommandHandler;
        _roomServicesCommandHandler = roomServicesCommandHandler;
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle( UpdateRoomTypeCommand command, CancellationToken cancellationToken )
    {
        ValidationResult validationResult = await _validator.ValidateAsync( command, cancellationToken );
        if ( !validationResult.IsValid )
        {
            List<Error> errors = validationResult.Errors
                .Select( error => new Error( error.ErrorMessage ) )
                .ToList();

            return Result.Failure( errors );
        }

        RoomType? existingRoomType = await _roomTypeRepository.GetById( command.RoomTypeId );
        if ( existingRoomType is null )
        {
            return Result.Failure( new Error( $"Room type with id: {command.RoomTypeId} was not found!" ) );
        }

        if ( command.RoomAmenities is not null )
        {
            GetOrCreateAmenitiesCommand amenitiesCommand = new() { AmenityNames = command.RoomAmenities };
            Result<List<Amenity>> amenitiesResult = await _amenitiesCommandHandler.Handle( amenitiesCommand, cancellationToken );
            List<Amenity> amenities = amenitiesResult.Value;

            existingRoomType.Amenities = amenities;
        }

        if ( command.RoomServices is not null )
        {
            GetOrCreateRoomServicesCommand roomServicesCommand = new() { RoomServiceNames = command.RoomServices };
            Result<List<RoomService>> roomServicesResult = await _roomServicesCommandHandler.Handle( roomServicesCommand, cancellationToken );
            List<RoomService> roomServices = roomServicesResult.Value;

            existingRoomType.Services = roomServices;
        }

        command.Adapt( existingRoomType );
        await _roomTypeRepository.Update( existingRoomType );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}