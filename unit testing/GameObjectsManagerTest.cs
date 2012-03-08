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
    public class GameObjectsManagerTest
    {
        [TestMethod]
        public void AddObjectTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager());
            manager.Invoke("AddObject", new LiftObjectTestClass(Vector2.Zero, 0, null));
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(1, liftObjects.Count);
        }

        [TestMethod]
        public void AddRandomObjectTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager());
            manager.Invoke("AddRandomObject");
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(1, liftObjects.Count);
        }
    }
}
