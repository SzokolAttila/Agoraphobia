﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.JoinTables.Rooms
{
    public class RoomWeaponLootStatus
    {
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int WeaponId { get; set; }
        public Weapon? Weapon { get; set; }
        public int Quantity { get; set; }
    }
}
