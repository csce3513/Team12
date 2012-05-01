using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;

namespace ProjectCowTest
{
    [TestClass]
    public class MenuScreenTest
    {
        [TestMethod]
        public void RestartTest()
        {
            ScreenManager manager = new ScreenManager();
            GameScreen game = new GameScreen();
            MenuScreen menu = new MenuScreen();
            
            menu.Manager = manager;
            manager.Screens.Push(game);
            manager.Screens.Push(menu);
            menu.Restart();

            Assert.AreNotEqual(game, manager.Screens.Peek());
        }
    }
}
