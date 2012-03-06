using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace ProjectCowTest
{
    [TestClass]
    public class LiftObjectTest
    {
        [TestMethod]
        public void OriginalYTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(Vector2.Zero, 0);

            Assert.AreEqual(0, cow.OriginalY);
        }

        [TestMethod]
        public void GravityTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(new Vector2(0, 200), 0);
            cow.Position = new Vector2(0, 0);
            cow.Update();

            Assert.AreEqual(9.8f, cow.Position.Y);

            cow.Position = new Vector2(100, 200);
            cow.Update();

            Assert.AreEqual(200, cow.Position.Y);
        }

        [TestMethod]
        public void IsInBeamRangeTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(Vector2.Zero, 0);

            Assert.IsTrue(cow.IsInBeamRange(new Vector2(-20, 0), 50));
            Assert.IsFalse(cow.IsInBeamRange(new Vector2(-50, 0), 49));
        }
    }
}
