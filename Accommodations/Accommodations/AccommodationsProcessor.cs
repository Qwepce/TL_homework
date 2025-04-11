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
            //Добавил обработку исключения из TryParseDate и TryParseGuid
            catch ( FormatException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
            //Добавил обработку исключения, если пытаемся выполнить undo, когда список команд пуст
            catch ( KeyNotFoundException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
            //Обработка исключения для команд, которые не поддерживают операцию undo
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

                // Кастомизировал сообщение об ошибке при неудачном парсинге
                if ( !Enum.TryParse( typeof( CurrencyDto ), parts[ 5 ], ignoreCase: true, out object? currency ) )
                {
                    throw new ArgumentException( $"Invalid currency {parts[ 5 ]}" );
                }

                startDate = TryParseDate( parts[ 3 ] );
                endDate = TryParseDate( parts[ 4 ] );

                // При создании нового бронирования дата заезда не может быть раньше текущей даты
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
                //Добавил проверку на длину списка команд
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

                //Добавил проверку на валидность введённого ID
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

    // Вынес парсинг даты в отдельный метод. Добавил проверку: если не можем спарсить в нужном формате, то выбрасываем ошибку
    private static DateTime TryParseDate( string stringDate )
    {
        if ( !DateTime.TryParse( stringDate, CultureInfo.GetCultureInfo( "en-US" ), out DateTime parsedDate ) )
        {
            throw new FormatException( "Invalid date format input. Please enter the date in format MM/dd/yyyy" );
        }

        return parsedDate;
    }

    // Вынес парсинг GUID в отдельный метод. Добавил проверку: если не можем спарсить в нужном формате, то выбрасываем ошибку
    private static Guid TryParseGuid( string stringGuid )
    {
        if ( !Guid.TryParse( stringGuid, out Guid parsedGuid ) )
        {
            throw new FormatException( "Invalid booking id" );
        }

        return parsedGuid;
    }
}
