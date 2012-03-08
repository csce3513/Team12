using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;

namespace ProjectCowTest
{
    [TestClass]
    public class GameObjectsManagerTest
    {
        [TestMethod]
        public void GenerateRandomObjectTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager());
            manager.Invoke("GenerateRandomObject");
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(1, liftObjects.Count);
        }
    }
}
