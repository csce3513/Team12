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

namespace ProjectCow
{
    public class Spaceship
    {
        Vector2 position;
        Texture2D image;

        public Spaceship()
        {
            position = new Vector2(0, 0);
            Speed = 1;

            TractorBeam = false;
        }

        public void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("spaceship");
        }

        public void MoveLeft()
        {
            position.X -= Speed;
        }

        public void MoveRight()
        {
            position.X += Speed;
        }

        public void MoveUp()
        {
            position.Y -= Speed;
        }

        public void MoveDown()
        {
            position.Y += Speed;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, Color.White);
        }

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public float Speed { get; set;  }
        public bool TractorBeam { get; set; }
    }
}
