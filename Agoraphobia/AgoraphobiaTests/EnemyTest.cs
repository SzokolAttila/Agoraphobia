using AgoraphobiaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaTests
{
    [TestClass]
    public class EnemyTest
    {
        public Enemy enemy;
        [TestInitialize]
        public void Init()
        {
            enemy = new Enemy("Kitten", "He seems cute, but don't buy into his lies!", 10, 5, 2, 10, 3,
                new Dictionary<Weapon, double>(), new Dictionary<Armor, double>(), new Dictionary<Consumable, double>());            
        }

        [TestMethod]
        public void TakeHitReducesHpByDamageMinusDefense()
        {
            enemy.TakeHit(6);
            Assert.AreEqual(9, enemy.Hp);
        }

        [TestMethod]
        public void HpUnchangedOnLessDmgThanDefense()
        {
            enemy.TakeHit(5);
            enemy.TakeHit(4);
            Assert.AreEqual(10, enemy.Hp);
        }

        [TestMethod]
        public void EnemyTakeDmgEqualToHpPlusDefDies()
        {
            Assert.AreEqual(true, enemy.TakeHit(15));
        }


        [TestMethod]
        public void EnemyTakeDmgLessThenHpPlusDefStaysAlive()
        {
            Assert.AreEqual(false, enemy.TakeHit(14));
        }

        [TestMethod]
        public void ThreeWeaponsWithFullDroprateAndTwoWithZeroReturnsTheThreeWeapons()
        {
            enemy.Weapons.Add(new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0), 1.0);
            enemy.Weapons.Add(new Weapon("Sword", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0), 0);
            enemy.Weapons.Add(new Weapon("Halberd", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0), 0);
            enemy.Weapons.Add(new Weapon("Bat", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0), 1.0);
            enemy.Weapons.Add(new Weapon("Bottle", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0), 1.0);
            Assert.AreEqual(3, enemy.DropWeapons().Count);
        }
    }
}
