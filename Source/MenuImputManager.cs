namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public class MenuInputManager
    {
        private MenuScreen menu;
        private ScreenManager manager;
        private Background background;

        public MenuInputManager(MenuScreen menu, ScreenManager manager, Background background)
        {
            this.menu = menu;
            this.manager = manager;
            this.background = background;
        }

        public void ProcessInputs(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.P))
                manager.PushScreen(menu);

            else if (state.IsKeyDown(Keys.C))
            {
                manager.PopScreen();
                manager.change = 1;
                menu.setMainMenu();
            }

            else if (state.IsKeyDown(Keys.Q))
                manager.Exit = true;

            else if (state.IsKeyDown(Keys.R))
            {
                menu.Restart();
                manager.PopScreen();
                manager.change = 1;
                background.restart = 1;
            }

            else if (state.IsKeyDown(Keys.B))
                menu.setMainMenu();

            else if (state.IsKeyDown(Keys.I))
                menu.setInstructionMenu();

            else
            {
                manager.change = 0;
                background.restart = 0;
            }
                    
        }

        public void Update()
        {
            ProcessInputs(Keyboard.GetState());
        }
    }
}
