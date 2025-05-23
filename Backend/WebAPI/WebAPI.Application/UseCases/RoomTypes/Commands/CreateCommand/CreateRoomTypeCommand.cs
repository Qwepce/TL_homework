﻿namespace WebAPI.Application.UseCases.RoomTypes.Commands.CreateCommand;

public class CreateRoomTypeCommand : IRoomTypeCommand
{
    public int PropertyId { get; set; }

    public string Name { get; init; }

    public decimal DailyPrice { get; init; }

    public string Currency { get; init; }

    public int MinPersonCount { get; init; }

    public int MaxPersonCount { get; init; }

    public int TotalRoomsCount { get; init; }

    public List<string> RoomServices { get; init; } = [];

    public List<string> RoomAmenities { get; init; } = [];
}