using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaAPI.Dtos.Enemy;
using AgoraphobiaAPI.Dtos.WeaponLoot;

namespace AgoraphobiaAPI.Dtos.Room
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WeaponLootDto> Weapons { get; set; }
        public List<ArmorLootDto> Armors { get; set; }
        public List<ConsumableLootDto> Consumables { get; set; }
        public EnemyDto Enemy { get; set; }
        public int OrientationId { get; set; }
        public int EnemyId { get; set; }
    }
}
