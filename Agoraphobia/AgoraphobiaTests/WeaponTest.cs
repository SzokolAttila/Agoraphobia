using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaTests
{
    [TestClass]
    public class WeaponTest
    {
        public Weapon weapon;

        [TestInitialize]
        public void Init()
        {
            weapon = new Weapon(1, "Stick", "It's a stick", 0, 2, 1.0, 1.5, 1);
        }

        [TestMethod]
        public void GreaterMinThanMaxThrowsException()
        {
            Assert.ThrowsException<MinGreaterThanMaxException>(() => weapon.MinMultiplier = 2.0);
            Assert.AreEqual(1.0, weapon.MinMultiplier);
        }

        [TestMethod]
        public void SmallerMaxThanMinThrowsException()
        {
            Assert.ThrowsException<MaxSmallerThanMinException>(() => weapon.MaxMultiplier = 0.5);
        }

        [TestMethod]
        public void MinAndMaxCantBeNegativeNumbers()
        {
            Assert.ThrowsException<NegativeMaxOrMinException>(() => weapon.MaxMultiplier = -0.5);
            Assert.ThrowsException<NegativeMaxOrMinException>(() => weapon.MinMultiplier = -0.5);
        }
    }
}
