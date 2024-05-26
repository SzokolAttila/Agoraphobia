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
            enemy = new Enemy("Kitten", "He seems cute, but don't buy into his lies!", 10, 5, 2, 10, 3);            
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
            Weapon stick = new Weapon("Stick", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            Weapon sword = new Weapon("Sword", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            Weapon halberd = new Weapon("Halberd", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            Weapon bat = new Weapon("Bat", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);
            Weapon bottle = new Weapon("Bottle", "Really weak, believe me.", 0, 0, 0.5, 1.2, 0);

            WeaponDroprate wpdr1 = new();
            wpdr1.Weapon = stick;
            wpdr1.Droprate = 1.0;
            enemy.WeaponDroprates.Add(wpdr1);

            WeaponDroprate wpdr2 = new();
            wpdr2.Weapon = sword;
            wpdr2.Droprate = 0.0;
            enemy.WeaponDroprates.Add(wpdr2);

            WeaponDroprate wpdr3 = new();
            wpdr3.Weapon = halberd;
            wpdr3.Droprate = 0.0;
            enemy.WeaponDroprates.Add(wpdr3);

            WeaponDroprate wpdr4 = new();
            wpdr4.Weapon = bat;
            wpdr4.Droprate = 1.0;
            enemy.WeaponDroprates.Add(wpdr4);

            WeaponDroprate wpdr5 = new();
            wpdr5.Weapon = bottle;
            wpdr5.Droprate = 1.0;
            enemy.WeaponDroprates.Add(wpdr5);

            Assert.AreEqual(3, enemy.DropWeapons().Count());
        }
    }
}
