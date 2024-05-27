using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public class Effect
    {
        public int ConsumableId { get; set; }
        public Consumable Consumable { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int CurrentDuration { get; set; }


    }
}
