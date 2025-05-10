using FluentValidation;
using FluentValidation.Results;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Amenities.GetOrCreate;
using WebAPI.Application.UseCases.RoomServices.GetOrCreate;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.CreateRoomType;

public class CreateRoomTypeCommandHandler : ICommandHandlerWithResult<CreateRoomTypeCommand, int>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> _roomServiceCommandHandler;
    private readonly ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> _amenityCommandHandler;
    private readonly IValidator<CreateRoomTypeCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomTypeCommandHandler(
        IRoomTypeRepository roomTypeRepository,
        IUnitOfWork unitOfWork,
        IAmenityRepository amenityRepository,
        IRoomServiceRepository roomServiceRepository,
        IValidator<CreateRoomTypeCommand> validator,
        ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>> amenityCommandHandler,
        ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>> roomServiceCommandHandler )
    {
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _amenityCommandHandler = amenityCommandHandler;
        _roomServiceCommandHandler = roomServiceCommandHandler;
    }

    public async Task<Result<int>> Handle( CreateRoomTypeCommand command, CancellationToken cancellationToken )
    {
        ValidationResult validationResult = await _validator.ValidateAsync( command, cancellationToken );
        if ( !validationResult.IsValid )
        {
            List<Error> errors = validationResult.Errors
                .Select( error => new Error( error.ErrorMessage ) )
                .ToList();

            return Result<int>.Failure( errors );
        }

        GetOrCreateRoomServicesCommand roomServicesCommand = new()
        {
            RoomServiceNames = command.RoomServices.Distinct()
        };

        Result<List<RoomService>> roomServicesResult = await _roomServiceCommandHandler.Handle( roomServicesCommand, cancellationToken );
        List<RoomService> roomServices = roomServicesResult.Value;

        GetOrCreateAmenitiesCommand amenitiesCommand = new()
        {
            AmenityNames = command.RoomAmenities.Distinct()
        };

        Result<List<Amenity>> amenitiesResult = await _amenityCommandHandler.Handle( amenitiesCommand, cancellationToken );
        List<Amenity> amenities = amenitiesResult.Value;

        RoomType roomType = new(
            command.PropertyId,
            command.Name.Trim(),
            command.DailyPrice,
            Enum.Parse<Currency>( command.Currency ),
            command.MinPersonCount,
            command.MaxPersonCount,
            command.TotalRoomsCount );
        roomType.Services = roomServices;
        roomType.Amenities = amenities;

        await _roomTypeRepository.Create( roomType );
        await _unitOfWork.CommitChangesAsync();

        return Result<int>.Success( roomType.Id );
    }
}
