using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public class ConsumableDroprate
    {
        public int EnemyId { get; set; }
        public int ConsumableId { get; set; }
        public double Droprate { get; set; }
        public Enemy? Enemy { get; set; }
        public Consumable? Consumable { get; set; }
    }
}
