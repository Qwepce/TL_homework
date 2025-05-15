using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Reservations.Commands.Delete;

public class DeleteReservationByIdCommandHandler : BaseCommandHandler<DeleteReservationByIdCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationByIdCommandHandler(
        IRequestValidator<DeleteReservationByIdCommand> validator,
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork )
        : base( validator )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> HandleCommand( DeleteReservationByIdCommand command, CancellationToken cancellationToken )
    {
        Reservation existingReservation = await _reservationRepository.GetById( command.ReservationId );

        await _reservationRepository.Delete( existingReservation );
        await _unitOfWork.CommitChangesAsync();

        return Result.Success();
    }
}
