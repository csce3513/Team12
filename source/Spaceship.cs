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
        private readonly Rectangle DEFAULT_BOUNDARY = new Rectangle(0, 0, 720, 240);

        // All these variables could be organized better at a later time
        private Vector2 position;
        private Rectangle boundaries;
        private Texture2D image;
        // List of available beams
        private List<Beam> beams;
        // Used to specify which beam is currently selected
        private int currentBeam;

        // Get currently selected beam
        public Beam CurrentBeam { get { return beams[currentBeam]; } }

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Speed { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool BeamOn { get; set; }

        public int LeftBoundary { get { return boundaries.Left; } set { boundaries.X = value; } }
        public int RightBoundary { get { return boundaries.Right; } set { boundaries.Width = value - boundaries.X; } }
        public int TopBoundary { get { return boundaries.Top; } set { boundaries.Y = value; } }
        public int BottomBoundary { get { return boundaries.Bottom; } set { boundaries.Height = value - boundaries.Y; } }
        public Vector2 Position { get { return position; } set { position = value; } }

        public bool IsAlive { get; set; }

        public Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)X, (int)Y, Width, Height);
            }
        }

        public Spaceship()
        {
            position = Vector2.Zero;
            Width = 150;
            Height = 125;
            Speed = 3;
            BeamOn = false;
            this.boundaries = DEFAULT_BOUNDARY;
            beams = new List<Beam>();
            beams.Add(new TractorBeam(new Vector2(position.X, position.Y + Height)));
            currentBeam = 0;
            IsAlive = true;
        }

        // Used to set boundary for testing
        public Spaceship(Rectangle boundaries)
        {
            position = Vector2.Zero;
            Width = 150;
            Height = 50;
            Speed = 3;
            BeamOn = false;
            this.boundaries = boundaries;
            beams = new List<Beam>();
            beams.Add(new TractorBeam(new Vector2(position.X, position.Y + Height)));
            currentBeam = 0;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("spaceship");
            beams[currentBeam].LoadContent(content);
        }

        // Boundary cases
        public void MoveLeft()
        {
            if (position.X >= LeftBoundary)
                position.X -= Speed;
        }

        public void MoveRight()
        {
            if (position.X + Width < RightBoundary)
                position.X += Speed;
        }

        public void MoveUp()
        {
            if (position.Y > TopBoundary)
                position.Y -= Speed;
        }

        public void MoveDown()
        {
            if (position.Y + Height< BottomBoundary)
                position.Y += Speed; 
        }

        public void Update()
        {
            if (IsAlive)
            {
                beams[currentBeam].LoadNextBeam();

                if (BeamOn)
                {
                    beams[currentBeam].Update(new Vector2(position.X, position.Y + Height / 2));
                }
            }
            else
            {
                BeamOn = false;
                position.X += 0.1f;
                position.Y += 0.5f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (BeamOn && IsAlive)
            {
                beams[currentBeam].Draw(spriteBatch);
            }
            spriteBatch.Draw(image, BoundBox, Color.White);
        }
    }
}
