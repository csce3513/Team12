using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using ProjectCow;

namespace ProjectCowTest
{
    [TestClass]
    public class LiftObjectTest
    {
        GameObjectsManager manager = new GameObjectsManager(new Spaceship(), new Random());

        [TestMethod]
        public void OriginalYTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(Vector2.Zero, 0, manager);

            Assert.AreEqual(0, cow.OriginalY);
        }

        [TestMethod]
        public void GravityTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(new Vector2(0, 200), 0, manager);
            cow.Position = new Vector2(0, 0);
            cow.Update(new GameTime());

            Assert.AreEqual(0.98f, cow.Position.Y);

            // Test that cow doesn't move when on ground
            cow.Position = new Vector2(100, 200);
            cow.Update(new GameTime());

            Assert.AreEqual(200, cow.Position.Y);
        }

        [TestMethod]
        public void IsInBeamRangeTest()
        {
            LiftObjectTestClass cow = new LiftObjectTestClass(Vector2.Zero, 0, manager);

            // Test when beam is in range
            Assert.IsTrue(cow.IsInBeamRange(new Vector2(-20, 0), 50));
            // Test when beam out of range
            Assert.IsFalse(cow.IsInBeamRange(new Vector2(-50, 0), 49));
        }

        [TestMethod]
        public void LiftTest()
        {
            // Test lift that hits
            LiftObjectTestClass cow = new LiftObjectTestClass(new Vector2(0, 400), 0, manager);
            cow.Lift(Vector2.Zero, 15, Vector2.Zero, 500);
            cow.Update(new GameTime());

            Assert.IsTrue(cow.Position.Y < 400);

            // Test lift that doesn't hit cow
            cow = new LiftObjectTestClass(new Vector2(0, 400), 0, manager);
            cow.Lift(Vector2.Zero, 15, new Vector2(300, 0), 500);
            cow.Update(new GameTime());

            Assert.IsTrue(cow.Position.Y == 400);

            // Test lift that captures cow
            cow = new LiftObjectTestClass(Vector2.Zero, 0, manager);
            cow.Lift(Vector2.Zero, 15, Vector2.Zero, 500);
            cow.Update(new GameTime());

            Assert.IsTrue(cow.Captured);
        }

        [TestMethod]
        public void CollisionTest()
        {
            Cow cow = new Cow(new Vector2(0, 100), 0, null, null);
            Tank tank = new Tank(new Vector2(0, 420), null, null);

            Assert.IsFalse(cow.IsCollided(tank));

            cow.Position = new Vector2(0, 400);

            Assert.IsTrue(cow.IsCollided(tank));
        }
    }
}
