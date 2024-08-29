using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Consumables
{
    public class ConsumableDroprate
    {
        public int Id { get; set; }
        public int EnemyId { get; set; }
        public int ConsumableId { get; set; }
        public double Droprate { get; set; }
        public Enemy? Enemy { get; set; }
        public Consumable? Consumable { get; set; }
        public ConsumableDroprate() { }
    }
}
