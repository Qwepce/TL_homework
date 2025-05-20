using Mapster;
using WebAPI.Application.UseCases.RoomTypes.Commands.CreateCommand;
using WebAPI.Application.UseCases.RoomTypes.Commands.UpdateCommand;
using WebAPI.Application.UseCases.RoomTypes.Dto;
using WebAPI.Web.Contracts.RoomTypeContracts;

namespace WebAPI.Web.Mappings.RoomType;

public static class RoomTypeMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<CreateRoomTypeContract, CreateRoomTypeCommand>.NewConfig()
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.DailyPrice, src => src.DailyPrice )
            .Map( dest => dest.Currency, src => src.Currency.Trim() )
            .Map( dest => dest.MinPersonCount, src => src.MinPersonCount )
            .Map( dest => dest.MaxPersonCount, src => src.MaxPersonCount )
            .Map( dest => dest.TotalRoomsCount, src => src.TotalRoomsCount )
            .Map( dest => dest.RoomServices, src => src.RoomServices )
            .Map( dest => dest.RoomAmenities, src => src.RoomAmenities );

        TypeAdapterConfig<UpdateRoomTypeContract, UpdateRoomTypeCommand>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.DailyPrice, src => src.DailyPrice )
            .Map( dest => dest.Currency, src => src.Currency.Trim() )
            .Map( dest => dest.MinPersonCount, src => src.MinPersonCount )
            .Map( dest => dest.MaxPersonCount, src => src.MaxPersonCount )
            .Map( dest => dest.TotalRoomsCount, src => src.TotalRoomsCount )
            .Map( dest => dest.RoomServices, src => src.RoomServices )
            .Map( dest => dest.RoomAmenities, src => src.RoomAmenities );

        TypeAdapterConfig<RoomTypeDto, ReadRoomTypeContract>.NewConfig()
            .Map( dest => dest.PropertyId, src => src.PropertyId )
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.DailyPrice, src => src.DailyPrice )
            .Map( dest => dest.Currency, src => src.Currency.Trim() )
            .Map( dest => dest.MinPersonCount, src => src.MinPersonCount )
            .Map( dest => dest.MaxPersonCount, src => src.MaxPersonCount )
            .Map( dest => dest.TotalRoomsCount, src => src.TotalRoomsCount )
            .Map( dest => dest.RoomServices, src => src.RoomServices )
            .Map( dest => dest.RoomAmenities, src => src.RoomAmenities );
    }
}
