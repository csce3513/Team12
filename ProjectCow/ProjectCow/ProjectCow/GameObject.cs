using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCow
{
    public class GameObject
    {
        //Variables for position on screen, uploaded image, and size.
        public Vector2 position = new Vector2(300, 380);
        private Texture2D image;
        public Rectangle size;
        public float scale = 1.0f;

        //Getter for uploaded image
        public object getImage
        {
            get
            { return image; }

        }

        //Method for loading the image
        public void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("cartooncow2");
            size = new Rectangle(0, 0, (int)(image.Width * scale), (int)(image.Height * scale));
        }
    }
}
