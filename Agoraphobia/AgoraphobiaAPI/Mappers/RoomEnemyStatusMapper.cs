using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomEnemyStatusMapper
    {
        public static RoomEnemyStatusDto ToRoomEnemyStatusDto(this RoomEnemyStatus status)
        {
            return new RoomEnemyStatusDto
            {
                Room = status.Room!.ToRoomDto(),
                EnemyHp = status.EnemyHp
            };
        }

        public static RoomEnemyStatus ToRoomStatusFromCreateDto(this CreateRoomEnemyStatusDto statusDto)
        {
            return new RoomEnemyStatus
            {
                EnemyHp = statusDto.EnemyHp,
                PlayerId = statusDto.PlayerId,
                RoomId = statusDto.RoomId
            };
        }
    }
}
