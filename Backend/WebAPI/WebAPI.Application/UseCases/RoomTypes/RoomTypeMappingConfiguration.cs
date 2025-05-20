using Mapster;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateCommand;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomTypes;

public static class RoomTypeMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<RoomType, RoomTypeDto>.NewConfig()
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.Name, src => src.Name )
            .Map( dest => dest.DailyPrice, src => src.DailyPrice )
            .Map( dest => dest.Currency, src => src.Currency.ToString() )
            .Map( dest => dest.MinPersonCount, src => src.MinPersonCount )
            .Map( dest => dest.MaxPersonCount, src => src.MaxPersonCount )
            .Map( dest => dest.TotalRoomsCount, src => src.TotalRoomsCount )
            .Map( dest => dest.RoomServices, src => src.RoomServices.Select( s => s.Name ).ToList() )
            .Map( dest => dest.RoomAmenities, src => src.Amenities.Select( s => s.Name ).ToList() );

        TypeAdapterConfig<UpdateRoomTypeCommand, RoomType>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.Currency, src => src.Currency.Trim() )
            .Map( dest => dest.DailyPrice, src => src.DailyPrice )
            .Map( dest => dest.MinPersonCount, src => src.MinPersonCount )
            .Map( dest => dest.MaxPersonCount, src => src.MaxPersonCount )
            .Map( dest => dest.TotalRoomsCount, src => src.TotalRoomsCount );
    }
}
