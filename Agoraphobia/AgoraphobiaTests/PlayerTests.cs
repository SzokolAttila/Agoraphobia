using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Player;

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
}