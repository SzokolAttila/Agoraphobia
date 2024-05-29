using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Rooms
{
    public class RoomEnemyStatus
    {
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int EnemyId { get; set; }
        public Enemy? Enemy { get; set; }
        public double EnemyHp { get; set; }
    }
}
