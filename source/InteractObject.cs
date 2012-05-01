/*
 * 
 * File: InteractObject.cs
 * 
 * Author: Ronnie Mathes
 * 
 * Date: 02/13/12
 * 
 * Description: InteractObject is a base class for objects the ufo can interact with. 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace ProjectCow
{
    public class InteractObject
    {

        //Variables for position on screen, uploaded image, and size.
        public Vector2 position = new Vector2(300, 380);
        private Texture2D image;
        public Rectangle size;
        public float scale = 1.0f;
        
     
        //Getter for uploaded image
        public object IAOImage 
        {
            get
            { return image; }
         
        }

        //Method for loading the image
        public void LoadIAO(ContentManager contentmanager, string imagename)
        {
            image = contentmanager.Load<Texture2D>(imagename);
            size = new Rectangle(0, 0, (int)(image.Width * scale), (int)(image.Height * scale));
        }

        //Basic drawing method
        public void DrawIAO(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, position, new Rectangle(0, 0, image.Width, image.Height), Color.White,

                0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        //Basic update method
        public void UpdateIAO(GameTime gametime, Vector2 speed, Vector2 direction)
        {
            position += direction * speed * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        public int getWidth()
        {
            return size.Width;
        }

        public int getHeight()
        {
            return size.Height;
        }

    }
}
