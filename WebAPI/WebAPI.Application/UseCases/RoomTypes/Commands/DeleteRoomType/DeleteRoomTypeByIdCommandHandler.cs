using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes.Commands.DeleteRoomType;

public class DeleteRoomTypeByIdCommandHandler : BaseCommandHandler<DeleteRoomTypeByIdCommand>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomTypeByIdCommandHandler(
        IRoomTypeRepository roomTypeRepository,
        IUnitOfWork unitOfWork,
        IRequestValidator<DeleteRoomTypeByIdCommand> validator )
        : base( validator )
    {
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( DeleteRoomTypeByIdCommand command, CancellationToken cancellationToken )
    {
        RoomType existingRoomType = await _roomTypeRepository.GetById( command.RoomTypeId );
        if ( existingRoomType is null )
        {
            return Result.Failure( new Error( $"RoomType with id {command.RoomTypeId} was not found!" ) );
        }

        try
        {
            await _roomTypeRepository.Delete( existingRoomType );
            await _unitOfWork.CommitChangesAsync();
        }
        catch ( Exception )
        {
            return Result.Failure( new Error( "Error while trying to delete room type. Perhaps it's belong to unclosed reservation." ) );
        }

        return Result.Success();
    }
}
