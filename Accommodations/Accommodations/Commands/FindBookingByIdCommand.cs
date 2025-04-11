using Accommodations.Models;

namespace Accommodations.Commands;

public class FindBookingByIdCommand( IBookingService bookingService, Guid bookingId ) : ICommand
{
    public void Execute()
    {
        // Changed booking.RoomCategory to booking.RoomCategory.Name
        Booking? booking = bookingService.FindBookingById( bookingId );
        Console.WriteLine( booking != null
            ? $"Booking found: {booking.RoomCategory.Name} for User {booking.UserId}"
            : "Booking not found." );
    }

    public void Undo()
    {
        throw new NotSupportedException( "Undo operation is not supported for find" );
    }
}
