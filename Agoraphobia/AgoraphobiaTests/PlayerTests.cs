using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Player;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaTests;

[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void HealthCannotGoAboveMaxHealth()
    {
        var player = new Player(3);
        player.Health += 42132;
        Assert.AreEqual(player.Health, player.MaxHealth);
    }

    [TestMethod]
    public void HealthCannotGoBelowZero()
    {
        var player = new Player(5)
        {
            Health = -35
        };
        Assert.AreEqual(0, player.Health);
    }

    [TestMethod]
    public void EnergyCannotGoAboveMaxEnergy()
    {
        var player = new Player(3);
        player.Energy += 315;
        Assert.AreEqual(player.Energy, player.MaxEnergy);
    }

    [TestMethod]
    public void EnergyGoingBelowZeroThrowsException()
    {
        var player = new Player(3);
        Assert.ThrowsException<NotEnoughEnergyException>(() => player.Energy -= 531);
        Assert.AreEqual(player.Energy, player.MaxEnergy);
    }

    [TestMethod]
    public void DreamCoinsGoingBelowZeroThrowsException()
    {
        var player = new Player(5);
        Assert.ThrowsException<NotEnoughDreamCoinsException>(() => player.DreamCoins -= 135);
    }

    [TestMethod]
    public void SanityCannotGoBeyondMaxSanity()
    {
        var player = new Player(5);
        player.Sanity += 53157;
        Assert.AreEqual(120, player.Sanity);
    }

    [TestMethod]
    public void SanityCannotGoBelowZero()
    {
        var player = new Player(5);
        player.Sanity -= 3519;
        Assert.AreEqual(0, player.Sanity);
    }

    [TestMethod]
    public void PlayerAttackEnemyChangesBothHPsAndEnergy()
    {
        var player = new Player(5);
        var enemy = new Enemy("Me", "I have mental issues thats sure", 10, 3, 2, 10, 5);
        double originalHealth = player.Health;
        double originalHp = enemy.Hp;

        player.AttackEnemy(enemy, new Weapon("stick", "just a stick", 0, 0, 0.5, 1.5, 1));
        Assert.AreNotEqual(player.Health, originalHealth);
        Assert.AreNotEqual(enemy.Hp, originalHp);
        Assert.AreNotEqual(player.Energy, player.MaxEnergy);
    }

    [TestMethod]
    public void GoingOverMaxInventoryCapacityThrowsException()
    {
        var player = new Player(5);

        WeaponInventory wi = new WeaponInventory()
        {
            Weapon = new Weapon("stick", "just a stick", 0, 0, 0.5, 1.5, 1),
            Quantity = 20
        };

        ConsumableInventory ci = new ConsumableInventory()
        {
            Consumable = new Consumable("apple", "just an apple", 0, 0, 2, 2, 2, 2, 2, 10),
            Quantity = 10
        };

        ArmorInventory ai = new ArmorInventory()
        {
            Armor = new Armor("helmet", "just a simple helmet", 0, 0, 1, 2, 1),
            Quantity = 20
        };

        player += ai;
        player += ci;
        player += wi;

        ArmorInventory goesOver = new ArmorInventory()
        {
            Armor = new Armor("helmet", "just a simple helmet", 0, 0, 1, 2, 1),
            Quantity = 2
        };

        Assert.ThrowsException<InventoryAlreadyFullException>(() => player +=goesOver);
    }

    [TestMethod]
    public void SameWeaponOnlyMakesQuantityHigher()
    {
        var player = new Player(5);

        WeaponInventory wi = new WeaponInventory()
        {
            WeaponId = 1,
            Weapon = new Weapon("stick", "just a stick", 0, 0, 0.5, 1.5, 1),
            Quantity = 20
        };

        WeaponInventory wi2 = new WeaponInventory()
        {
            WeaponId = 1,
            Weapon = new Weapon("stick", "just a stick", 0, 0, 0.5, 1.5, 1),
            Quantity = 10
        };

        player += wi2;
        player += wi;

        Assert.AreEqual(30, player.WeaponInventories.First().Quantity);
    }
}