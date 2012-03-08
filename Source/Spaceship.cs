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

        public Spaceship()
        {
            position = Vector2.Zero;
            Speed = 3;
            BeamOn = false;
            this.boundaries = DEFAULT_BOUNDARY;
            beams = new List<Beam>();
            beams.Add(new TractorBeam(position));
            currentBeam = 0;
        }

        // Used to set boundary for testing
        public Spaceship(Rectangle boundaries)
        {
            position = Vector2.Zero;
            Speed = 3;
            BeamOn = false;
            this.boundaries = boundaries;
            beams = new List<Beam>();
            beams.Add(new TractorBeam(position));
            currentBeam = 0;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("spaceship");
            Width = image.Width;
            Height = image.Height;
        }

        // Boundary cases
        public void MoveLeft()
        {
            if (position.X > LeftBoundary)
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
            if (position.Y < BottomBoundary)
                position.Y += Speed; 
        }

        public void Update()
        {
            if (BeamOn)
                beams[currentBeam].Update(position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, Color.White);
            if (BeamOn)
                beams[currentBeam].Draw(spriteBatch);
        }
    }
}
