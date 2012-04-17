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
        public Vector2 Position = new Vector2(0,0);

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        private ContentManager manager;
        private Background background;
        private MenuInputManager menuInput;
        public int picture = 0;
        string[] names = new string[2];

        public MenuScreen(Background background)
        {
            this.background = background;
            names[0] = "menu";
            names[1] = "intructions";
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(names[picture]);
            manager = theContentManager;
            menuInput = new MenuInputManager(this, Manager, background);
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

        public void Restart()
        {
            background.Restart(); 
        }

        public void Update(GameTime gameTime)
        {
            menuInput.Update();
        }

        public int getPicture()
        {
            return picture;
        }

        public ScreenManager Manager { get; set; }
    }
}
