using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCow
{
    public class MenuScreen : IScreen
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        private ContentManager manager;
        private InputManager input;
        
        // Index of picture name
        public int picture = 0;
        // Name of pictures (menu, instruction)
        string[] names = new string[2];

        public ScreenManager Manager { get; set; }

        public MenuScreen()
        {
            input = new InputManager();
            names[0] = "menu";
            names[1] = "intructions";
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(names[picture]);

            // Only set content manager once
            if(manager == null)
                manager = theContentManager;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, Color.White);
        }

        public void setMainMenu()
        {
            picture = 0;
            LoadContent(manager);
        }

        public void setInstructionMenu()
        {
            picture = 1;
            LoadContent(manager);
        }

        // Restart the game
        public void Restart()
        {
            // Pop menu and game screen then recreate game screen
            Manager.PopScreen();
            Manager.PopScreen();
            Manager.PushScreen(new GameScreen());
        }

        public void ProcessInputs()
        {
            if (input.IsCKeyPressed())
                Manager.PopScreen();
            else if (input.IsQKeyPressed())
                Manager.Exit = true;
            else if (input.IsRKeyPressed())
                Restart();
            else if (input.IsBKeyPressed())
                setMainMenu();
            else if (input.IsIKeyPressed())
                setInstructionMenu();
        }

        public void Update(GameTime gameTime)
        {
            input.Update();
            ProcessInputs();
        }

        // Get index of picture name
        public int getPicture()
        {
            return picture;
        }
    }
}
