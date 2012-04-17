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
            PrivateObject manager = new PrivateObject(new GameObjectsManager(new Random()));
            manager.Invoke("AddObject", new LiftObjectTestClass(Vector2.Zero, 0, null));
            manager.Invoke("Update", new GameTime());
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(1, liftObjects.Count);
        }

        [TestMethod]
        public void RemoveObjectTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager(new Random()));
            manager.Invoke("AddObject", new LiftObjectTestClass(Vector2.Zero, 0, null));
            manager.Invoke("Update", new GameTime());
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            manager.Invoke("RemoveObject", liftObjects.Last());
            manager.Invoke("Update", new GameTime());
            liftObjects = (List<LiftObject>)manager.GetField("liftObjects");
            Assert.AreEqual(0, liftObjects.Count);
        }

        [TestMethod]
        public void AddRandomObjectTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager(new Random()));
            manager.Invoke("AddRandomObject");
            manager.Invoke("Update", new GameTime());
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(1, liftObjects.Count);
        }

        [TestMethod]
        public void LiftObjectsTest()
        {
            Spaceship spaceship = new Spaceship();
            spaceship.X = 49;
            spaceship.Width = 200;
            GameObjectsManager manager = new GameObjectsManager(new Random());
            manager.AddRandomObject();
            manager.LiftObjects(spaceship);
            manager.Update(new GameTime());

            PrivateObject privateManager = new PrivateObject(manager);
            List<LiftObject> liftObjects = (List<LiftObject>)privateManager.GetField("liftObjects");

            Assert.IsTrue(liftObjects[0].Position.Y < 500);
        }

        [TestMethod]
        public void ShiftObjectsLeftTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager(new Random()));
            manager.Invoke("AddObject", new Cow(Vector2.Zero, 0, null, new Random(2)));
            manager.Invoke("Update", new GameTime());
            manager.Invoke("ShiftObjectsLeft");
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(-720, liftObjects[0].Position.X);
        }

        [TestMethod]
        public void ShiftObjectsRightTest()
        {
            PrivateObject manager = new PrivateObject(new GameObjectsManager(new Random()));
            manager.Invoke("AddObject", new Cow(Vector2.Zero, 0, null, new Random(2)));
            manager.Invoke("Update", new GameTime());
            manager.Invoke("ShiftObjectsRight");
            List<LiftObject> liftObjects = (List<LiftObject>)manager.GetField("liftObjects");

            Assert.AreEqual(720, liftObjects[0].Position.X);
        }

        [TestMethod]
        public void CheckCollisionTest()
        {
            GameObjectsManager gameObjectsManager = new GameObjectsManager(new Random());
            Cow cow1 = new Cow(new Vector2(0, 500), 0, gameObjectsManager, new Random());

            Assert.IsTrue(true);
        }
    }
}
