namespace ProjectCowTest
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProjectCow;

    [TestClass]
    public class ScreenManagerTest
    {
        ScreenManager manager = new ScreenManager();

        [TestMethod]
        public void PushScreenTest()
        {
            GameScreen screen = new GameScreen();
            manager.PushScreen(screen);

            Assert.AreEqual(screen, manager.Screens.Peek());
        }

        [TestMethod]
        public void PopScreenTest()
        {
            manager.PushScreen(new GameScreen());
            manager.PushScreen(new GameScreen());
            manager.PopScreen();

            Assert.AreEqual(1, manager.Screens.Count);
        }
    }
}
