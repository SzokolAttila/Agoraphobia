using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Weapons
{
    public class WeaponSale
    {
        public int WeaponId { get; set; }
        public Weapon? Weapon { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public int Quantity { get; set; }
    }
}
