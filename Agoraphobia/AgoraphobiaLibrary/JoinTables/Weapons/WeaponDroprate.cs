using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Weapons
{
    public class WeaponDroprate
    {
        public int Id { get; set; }
        public int EnemyId { get; set; }
        public int WeaponId { get; set; }
        public double Droprate { get; set; }
        public Enemy? Enemy { get; set; }
        public Weapon? Weapon { get; set; }
        public WeaponDroprate() { }
    }
}
