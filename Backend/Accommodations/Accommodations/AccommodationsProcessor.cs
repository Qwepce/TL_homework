using System.Globalization;
using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        string input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( ArgumentException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
            // Added exception handling from TryParseDate and TryParseGuid
            catch ( FormatException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
            // Added exception handling for undo when executed commands list is empty
            catch ( KeyNotFoundException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
            // Added exception handling for commands that don't support undo operation
            catch ( NotSupportedException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    private static void ProcessCommand( string input )
    {
        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];
        DateTime startDate;
        DateTime endDate;

        switch ( commandName )
        {
            case "book":
                if ( parts.Length != 6 )
                {
                    Console.WriteLine( "Invalid number of arguments for booking." );
                    return;
                }

                // Customized the error message in case of unsuccessful parsing
                if ( !Enum.TryParse( typeof( CurrencyDto ), parts[ 5 ], ignoreCase: true, out object? currency ) )
                {
                    throw new ArgumentException( $"Invalid currency {parts[ 5 ]}" );
                }

                startDate = TryParseDate( parts[ 3 ] );
                endDate = TryParseDate( parts[ 4 ] );

                // When create a new booking, start date cannot be earlier than the current date.
                if ( startDate < DateTime.Now.Date )
                {
                    throw new ArgumentException( "Start date cannot be earlier than today" );
                }

                BookingDto bookingDto = new()
                {
                    UserId = int.Parse( parts[ 1 ] ),
                    Category = parts[ 2 ].ToLower(),
                    StartDate = startDate,
                    EndDate = endDate,
                    Currency = ( CurrencyDto )currency,
                };

                BookCommand bookCommand = new( _bookingService, bookingDto );
                bookCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, bookCommand );
                Console.WriteLine( "Booking command run is successful." );
                break;

            case "cancel":
                if ( parts.Length != 2 )
                {
                    Console.WriteLine( "Invalid number of arguments for canceling." );
                    return;
                }

                Guid bookingId = TryParseGuid( parts[ 1 ] );
                CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
                cancelCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, cancelCommand );
                Console.WriteLine( "Cancellation command run is successful." );
                break;

            case "undo":

                // Added a check for the length of executed commands list 
                if ( _executedCommands.Count == 0 )
                {
                    throw new KeyNotFoundException( "The history of commands is empty. There's nothing to undo" );
                }

                _executedCommands[ s_commandIndex ].Undo();

                _executedCommands.Remove( s_commandIndex );
                s_commandIndex--;
                Console.WriteLine( "Last command undone." );

                break;
            case "find":
                if ( parts.Length != 2 )
                {
                    Console.WriteLine( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
                    return;
                }

                // Added a check for valid entered bookingId
                Guid id = TryParseGuid( parts[ 1 ] );

                FindBookingByIdCommand findCommand = new( _bookingService, id );
                findCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, findCommand );
                break;

            case "search":
                if ( parts.Length != 4 )
                {
                    Console.WriteLine( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'" );
                    return;
                }

                startDate = TryParseDate( parts[ 1 ] );
                endDate = TryParseDate( parts[ 2 ] );
                string categoryName = parts[ 3 ].ToLower().Trim();
                SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
                searchCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, searchCommand );
                break;

            default:
                Console.WriteLine( "Unknown command." );
                break;
        }
    }

    // Made date parsing a separate method. Added a check: if we can't parse it in the right format, then we throw FormatException.
    private static DateTime TryParseDate( string stringDate )
    {
        if ( !DateTime.TryParse( stringDate, CultureInfo.GetCultureInfo( "en-US" ), out DateTime parsedDate ) )
        {
            throw new FormatException( "Invalid date format input. Please enter the date in format MM/dd/yyyy" );
        }

        return parsedDate;
    }

    // Made guid parsing a separate method. Added a check: if we can't parse it in the right format, then we throw FormatException.
    private static Guid TryParseGuid( string stringGuid )
    {
        if ( !Guid.TryParse( stringGuid, out Guid parsedGuid ) )
        {
            throw new FormatException( "Invalid booking id" );
        }

        return parsedGuid;
    }
}
