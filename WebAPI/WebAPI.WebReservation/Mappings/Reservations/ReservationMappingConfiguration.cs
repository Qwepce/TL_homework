using Mapster;
using WebAPI.Application.UseCases.Reservations.Commands.CreateReservation;
using WebAPI.Application.UseCases.Reservations.Dto;
using WebAPI.Application.UseCases.Reservations.Queries.GetAllReservations;
using WebAPI.Application.UseCases.Reservations.Queries.SearchAvailableReservations;
using WebAPI.WebReservation.Contracts.Reservations;
using WebAPI.WebReservation.Filters.AvailableReservations;
using WebAPI.WebReservation.Filters.Reservations;

namespace WebAPI.WebReservation.Mappings.Reservations;

public static class ReservationMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<CreateReservationContract, CreateReservationCommand>.NewConfig()
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.RoomTypeId, src => src.RoomTypeId )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestName, src => src.GuestName )
            .Map( dest => dest.GuestPhoneNumber, src => src.GuestPhoneNumber )
            .Map( dest => dest.GuestsCount, src => src.GuestsCount )
            .Map( dest => dest.ReservationCurrency, src => src.Currency );

        TypeAdapterConfig<SearchAvailableReservationsFilter, SearchAvailableReservationsQuery>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.City, src => src.City.Trim() )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestsNumber, src => src.GuestsNumber )
            .Map( dest => dest.MaxPrice, src => src.MaxPrice );

        TypeAdapterConfig<ReservationDto, ReadReservationContract>.NewConfig()
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.RoomTypeId, src => src.RoomTypeId )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestName, src => src.GuestName )
            .Map( dest => dest.GuestPhoneNumber, src => src.GuestPhoneNumber )
            .Map( dest => dest.ReservationCurrency, src => src.ReservationCurrency );

        TypeAdapterConfig<ReservationsFilter, GetAllReservationsQuery>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.RoomTypeId, src => src.RoomTypeId )
            .Map( dest => dest.ArrivalDate, src => src.ArrivalDate )
            .Map( dest => dest.DepartureDate, src => src.DepartureDate )
            .Map( dest => dest.GuestName, src => src.GuestName )
            .Map( dest => dest.GuestPhoneNumber, src => src.GuestPhoneNumber )
            .Map( dest => dest.ReservationCurrency, src => src.ReservationCurrency );
    }
}