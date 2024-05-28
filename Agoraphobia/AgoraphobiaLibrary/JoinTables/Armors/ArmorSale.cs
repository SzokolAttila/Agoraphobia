using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Armors
{
    public class ArmorSale
    {
        public int ArmorId { get; set; }
        public Armor? Armor { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public int Quantity { get; set; }
    }
}
