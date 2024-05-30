﻿using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IRoomWeaponLootStatusRepository
    {
        public Task<List<RoomWeaponLootStatus>> GetRoomWeaponLootStatusesAsync(int playerId);

    }
}
