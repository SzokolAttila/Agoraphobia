using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Armors
{
    public class ArmorDroprate
    {
        public int Id { get; set; }
        public int EnemyId { get; set; }
        public int ArmorId { get; set; }
        public double Droprate { get; set; }
        public Enemy? Enemy { get; set; }
        public Armor? Armor { get; set; }

        public ArmorDroprate() { }
    }
}
