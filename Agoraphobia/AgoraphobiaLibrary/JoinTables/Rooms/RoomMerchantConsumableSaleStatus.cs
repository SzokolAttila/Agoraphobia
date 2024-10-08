﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Rooms
{
    public class RoomMerchantConsumableSaleStatus
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }
        public int ConsumableId { get; set; }
        public Consumable? Consumable { get; set; }
        public int Quantity { get; set; }
    }
}
