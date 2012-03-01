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

    public class Spaceship
    {
        private readonly Rectangle DEFAULT_BOUNDARY = new Rectangle(0, 0, 720, 480);

        private Vector2 position;
        private Rectangle boundaries;
        private Texture2D image;

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Speed { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool TractorBeam { get; set; }

        public int LeftBoundary { get { return boundaries.Left; } set { boundaries.X = value; } }
        public int RightBoundary { get { return boundaries.Right; } set { boundaries.Width = value - boundaries.X; } }
        public int TopBoundary { get { return boundaries.Top; } set { boundaries.Y = value; } }
        public int BottomBoundary { get { return boundaries.Bottom; } set { boundaries.Height = value - boundaries.Y; } }

        public Spaceship()
        {
            position = Vector2.Zero;
            Speed = 3;
            TractorBeam = false;
            boundaries = DEFAULT_BOUNDARY;
        }

        //this constructor mainly used for testing
        public Spaceship(Rectangle boundaries)
        {
            position = Vector2.Zero;
            Speed = 1;
            TractorBeam = false;
            this.boundaries = boundaries;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("spaceship");
        }

        //Boundary cases
        public void MoveLeft()
        {
            if (position.X > LeftBoundary)
                position.X -= Speed;
        }

        public void MoveRight()
        {
            if (position.X < RightBoundary)
                position.X += Speed;
        }

        public void MoveUp()
        {
            if (position.Y > TopBoundary)
                position.Y -= Speed;
        }

        public void MoveDown()
        {
            if (position.Y < BottomBoundary)
                position.Y += Speed; 
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Rectangle(0, 0, 50, 50), Color.White);
        }
    }
}
