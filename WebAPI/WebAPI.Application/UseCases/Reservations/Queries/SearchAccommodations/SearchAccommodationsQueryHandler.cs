using Mapster;
using WebAPI.Application.Filters;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Application.Utils;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.Reservations.Queries.SearchAccommodations;

public class SearchAccommodationsQueryHandler : IQueryHandler<SearchAccommodationsQuery, List<SearchResultDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IPriceCalculator _calculator;

    public SearchAccommodationsQueryHandler(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository,
        IPriceCalculator calculator )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
        _calculator = calculator;
    }

    public async Task<Result<List<SearchResultDto>>> Handle( SearchAccommodationsQuery query, CancellationToken cancellationToken )
    {
        IReadOnlyList<Property> properties = await _propertyRepository.GetAllByCity( query.City );
        List<SearchResultDto> searchResults = new List<SearchResultDto>();

        foreach ( Property property in properties )
        {
            List<RoomType> availableRoomTypes = await GetAvailableRoomTypesForProperty( property, query );

            if ( availableRoomTypes.Any() )
            {
                SearchResultDto searchResult = new()
                {
                    Name = property.Name,
                    Country = property.Country,
                    City = property.City,
                    Address = property.Address,
                    RoomTypes = availableRoomTypes.Adapt<IReadOnlyCollection<RoomTypeDto>>()
                };
                searchResults.Add( searchResult );
            }
        }

        return Result<List<SearchResultDto>>.Success( searchResults );
    }

    private async Task<List<RoomType>> GetAvailableRoomTypesForProperty(
    Property property,
    SearchAccommodationsQuery query )
    {
        SearchRoomTypesFilter filter = new()
        {
            GuestsNumber = query.GuestsNumber
        };
        List<RoomType> roomTypes = await _roomTypeRepository.GetByFilters( property.Id, filter );

        List<RoomType> availableRoomTypes = new List<RoomType>();
        foreach ( RoomType roomType in roomTypes )
        {
            if ( await IsRoomTypeAvailable( roomType, query ) )
            {
                availableRoomTypes.Add( roomType );
            }
        }

        return availableRoomTypes;
    }

    private async Task<bool> IsRoomTypeAvailable( RoomType roomType, SearchAccommodationsQuery query )
    {
        if ( !query.ArrivalDate.HasValue )
        {
            return true;
        }

        DateOnly arrivalDate = query.ArrivalDate.Value;
        DateOnly departureDate = query.DepartureDate ?? arrivalDate.AddDays( 1 );

        if ( !IsWithinPriceRange( roomType, query, arrivalDate, departureDate ) )
        {
            return false;
        }

        return await HasAvailableRooms( roomType, arrivalDate, departureDate );
    }

    private bool IsWithinPriceRange(
        RoomType roomType,
        SearchAccommodationsQuery query,
        DateOnly arrivalDate,
        DateOnly departureDate )
    {
        if ( string.IsNullOrWhiteSpace( query.Currency ) ||
            !Enum.TryParse( query.Currency, ignoreCase: true, out Currency searchingCurrency ) )
        {
            return true;
        }

        int totalDays = departureDate.DayNumber - arrivalDate.DayNumber;
        decimal roomTypeTotalPrice = _calculator.CalculateTotalPrice(
            totalDays,
            roomType.DailyPrice,
            roomType.Currency,
            searchingCurrency );

        return roomTypeTotalPrice <= query.MaxPrice;
    }

    private async Task<bool> HasAvailableRooms(
        RoomType roomType,
        DateOnly arrivalDate,
        DateOnly departureDate )
    {
        int overlappingReservations = await _reservationRepository.GetOverlappingReservationsCount(
            roomType.Id,
            arrivalDate,
            departureDate );

        return overlappingReservations < roomType.TotalRoomsCount;
    }
}