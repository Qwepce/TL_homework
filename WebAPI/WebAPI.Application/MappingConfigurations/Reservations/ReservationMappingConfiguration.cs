using Mapster;
using WebAPI.Application.Filters;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAll;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.MappingConfigurations.Reservations;

public static class ReservationMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<Reservation, ReservationDto>.NewConfig()
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.RoomTypeId, src => src.RoomTypeId )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestName, src => src.GuestName )
            .Map( dest => dest.GuestPhoneNumber, src => src.GuestPhoneNumber )
            .Map( dest => dest.TotalPrice, src => src.TotalPrice )
            .Map( dest => dest.ReservationCurrency, src => src.ReservationCurrency );

        TypeAdapterConfig<GetAllReservationsQuery, SearchReservationsFilter>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.RoomTypeId, src => src.RoomTypeId )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestName, src => src.GuestName.Trim() )
            .Map( dest => dest.GuestPhoneNumber, src => src.GuestPhoneNumber.Trim() )
            .Map( dest => dest.ReservationCurrency, src => src.ReservationCurrency.Trim() );
    }
}
