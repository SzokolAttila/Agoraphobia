using AgoraphobiaLibrary;
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
            Enemy enemy = new Enemy("Kitten", "He seems cute, but don't buy into his lies!", 10, 5, 2, 10, 3);
            room = new Room(1,"Neu", "Aka Insane Asylum", new List<WeaponInventory>(), new List<ArmorInventory>(),
                new List<ConsumableInventory>(), 2, enemy);
        }

        [TestMethod]
        public void PickupOnlyWeaponRemovesWeaponFromRoom()
        {
            WeaponInventory weaponInventory = new WeaponInventory();
            Weapon stick = new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            weaponInventory.Weapon = stick;
            weaponInventory.Quantity = 1;
            room.Weapons.Add(weaponInventory);
            room.PickupWeapon(0);

            Assert.AreEqual(0, room.Weapons.Count);
        }

        [TestMethod]
        public void PickupWeaponFromSameWeaponsOnlyReducesTheQuantity()
        {
            WeaponInventory weaponInventory = new WeaponInventory();
            Weapon stick = new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            weaponInventory.Weapon = stick;
            weaponInventory.Quantity = 2;
            room.Weapons.Add(weaponInventory);
            room.PickupWeapon(0);

            Assert.AreEqual(1, room.Weapons.Count);
            Assert.AreEqual(1, room.Weapons.ElementAt(0).Quantity);
        }
    }
}
