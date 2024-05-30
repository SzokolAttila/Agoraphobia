﻿using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos
{
    public class RoomArmorLootStatusDto
    {
        public int PlayerId { get; set; }
        public RoomDto Room { get; set; } = new();
        public ArmorDto Armor { get; set; } = new();
        public int Quantity { get; set; }
    }
}
