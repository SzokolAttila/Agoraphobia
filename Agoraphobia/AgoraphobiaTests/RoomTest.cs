using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaTests
{
    [TestClass]
    public class RoomTest
    {
        Room room;
        [TestInitialize] 
        public void Initialize()
        {
            room = new Room(1,"Neu", "Aka Insane Asylum", new List<WeaponLoot>(), new List<ArmorLoot>(),
                new List<ConsumableLoot>(), 2, 0);
        }

        [TestMethod]
        public void PickupOnlyWeaponRemovesWeaponFromRoom()
        {
            WeaponLoot weaponLoot = new WeaponLoot();
            Weapon stick = new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            weaponLoot.Weapon = stick;
            weaponLoot.Quantity = 1;
            room.Weapons.Add(weaponLoot);
            room.PickupWeapon(0);

            Assert.AreEqual(0, room.Weapons.Count);
        }

        [TestMethod]
        public void PickupWeaponFromSameWeaponsOnlyReducesTheQuantity()
        {
            WeaponLoot weaponLoot = new WeaponLoot();
            Weapon stick = new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            weaponLoot.Weapon = stick;
            weaponLoot.Quantity = 2;
            room.Weapons.Add(weaponLoot);
            room.PickupWeapon(0);

            Assert.AreEqual(1, room.Weapons.Count);
            Assert.AreEqual(1, room.Weapons.ElementAt(0).Quantity);
        }
    }
}
