using Accommodations.Models;

namespace Accommodations.Commands;

public class CancelBookingCommand( IBookingService bookingService, Guid bookingId ) : ICommand
{
    private Booking? _canceledBooking;

    public void Execute()
    {
        _canceledBooking = bookingService.FindBookingById( bookingId );
        if ( _canceledBooking != null )
        {
            bookingService.CancelBooking( bookingId );
            decimal cancellationPenalty = bookingService.CalculateCancellationPenaltyAmount( _canceledBooking );
            Console.WriteLine(
                $"Booking {_canceledBooking.Id} was canceled. Cancellation penalty: {cancellationPenalty} {_canceledBooking.Currency.ToString()}"
                );
        }
        else
        {
            Console.WriteLine( $"Booking {bookingId} not found." );
        }
    }

    public void Undo()
    {
        throw new NotSupportedException( "Undo operation is not supported for cancel" );
    }
}
