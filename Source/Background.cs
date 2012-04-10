using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCow
{
    public class Background
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0,0);

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        private ContentManager manager;

        // Name for background
        string[] names = new string[6];
        // Name of current background
        string name;
        // Index of current background
        int num;

        public Background()
        {
            names[0] = "mountaings";
            names[1] = "mountaings2";
            names[2] = "mountaings3";
            names[3] = "mountaings4";
            names[4] = "mountaings5";
            names[5] = "mountaings6";

            num = 0;
            name = names[0];
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(names[num]);

            // Set manager only once
            if(manager == null)
                manager = theContentManager;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, Color.White);
        }

        public void LoadNextBackground()
        {
            if (num < 5)
                num++;
            else
                num = 0;

            LoadContent(manager);
        }

        public void LoadPreviousBackground()
        {
            if (num > 0)
                num--;
            else
                num = 5;

            LoadContent(manager);
        }

        // Returns name of current background image
        public string getBackgroundImage()
        {
            return names[num];
        }
    }
}
