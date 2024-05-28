using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Consumables
{
    public class ConsumableSale
    {
        public int ConsumableId { get; set; }
        public Consumable? Consumable { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public int Quantity { get; set; }
    }
}
