using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Consumable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaTests
{
    [TestClass]
    public class ConsumableTests
    {
        public Consumable cons;
        [TestInitialize]
        public void Init()
        {
            cons = new("Apple juice", "Tasty isnt it?", 0, 2, 2, 2, 1, 1, 3, 10);
        }

        [TestMethod]
        public void NegativeDurationThrowsException()
        {
            Assert.ThrowsException<NegativeDurationException>(() => cons.Duration = -1);
        }
    }
}
