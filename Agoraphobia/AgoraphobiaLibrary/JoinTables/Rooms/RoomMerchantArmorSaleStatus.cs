﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Rooms
{
    public class RoomMerchantArmorSaleStatus
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public int ArmorId { get; set; }
        public Armor? Armor { get; set; }
        public int Quantity { get; set; }
    }
}
