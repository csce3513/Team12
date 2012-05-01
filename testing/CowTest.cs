using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;
using Microsoft.Xna.Framework;

namespace ProjectCowTest
{
    [TestClass]
    public class CowTest
    {
        [TestMethod]
        public void MoveTest()
        {
            Cow cow = new Cow(Vector2.Zero, 0, null, new Random());
            PrivateObject cowObject = new PrivateObject(cow);
            cowObject.SetField("moveRight", true);
            cowObject.Invoke("Move");
            Assert.IsTrue(((Vector2)cowObject.GetField("speed")).X > 0);
        }

        [TestMethod]
        public void MoveRandomlyTest()
        {
            // Test that it moves when on ground
            PrivateObject cow = new PrivateObject(new Cow(new Vector2(100, 300), 0, null, new Random()));
            cow.Invoke("MoveRandomly", new GameTime());
            TimeSpan duration = (TimeSpan) cow.GetField("actionDuration");
            Assert.IsTrue(duration.Ticks > 0);

            // Test that it doesn't move when off ground
            cow.SetProperty("Position", new Vector2(100, 299));
            cow.Invoke("MoveRandomly", new GameTime());
            duration = (TimeSpan)cow.GetField("actionDuration");
            Assert.AreEqual(0, duration.Ticks);
        }
    }
}
