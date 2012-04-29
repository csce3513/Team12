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
    public class TankTest
    {
        [TestMethod]
        public void MoveTest()
        {
            Tank tank = new Tank(new Vector2(-20, 0), null, null);
            tank.Move();
            PrivateObject tankObject = new PrivateObject(tank);

            Assert.IsTrue(((Vector2)tankObject.GetField("speed")).X > 0);
        }

        [TestMethod]
        public void RotateGunTest()
        {
            Tank tank = new Tank(new Vector2(0, 400), null, null);
            PrivateObject tankObject = new PrivateObject(tank);
            tank.RotateGun(new Vector2(300, 0), 100, 100);

            Assert.IsTrue((float)tankObject.GetField("gunRotation") < 0);

            tank.RotateGun(new Vector2(-300, 0), 100, 100);

            Assert.IsTrue((float)tankObject.GetField("gunRotation") > 0);
        }
    }
}
