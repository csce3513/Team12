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

        string[] names = new string[3];
        string name;
        int num;

        public Background()
        {
            names[0] = "farm1";
            names[1] = "farm2";
            names[2] = "farm3";

            num = 0;
            name = names[0];
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(names[num]);
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, Color.White);
        }

        public void setBackgroundImage()
        {
            Random random = new Random();
            num = random.Next(0, 3);
        }

        public string getBackgroundImage()
        {
            return names[num];
        }
    }
}
