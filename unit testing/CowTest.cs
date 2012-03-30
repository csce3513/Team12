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
        public void MoveRandomlyTest()
        {
            PrivateObject cow = new PrivateObject(new Cow(new Vector2(100, 300), 0, null, new Random()));
            cow.Invoke("MoveRandomly", new GameTime());
            TimeSpan duration = (TimeSpan) cow.GetField("actionDuration");
            Assert.IsTrue(duration.Ticks > 0);

            cow.SetProperty("Position", new Vector2(100, 299));
            cow.Invoke("MoveRandomly", new GameTime());
            duration = (TimeSpan)cow.GetField("actionDuration");
            Assert.AreEqual(0, duration.Ticks);
        }
    }
}
