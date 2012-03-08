namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Screen for displaying the main game.
    /// </summary>
    public class GameScreen : IScreen
    {
        private Background background;
        private Spaceship spaceship;
        private SpaceshipInputManager spaceshipInput;
        private Cow cow;

        public ScreenManager Manager { get; set; }

        public GameScreen()
        {
            background = new Background();
            spaceship = new Spaceship();
            spaceshipInput = new SpaceshipInputManager(spaceship);
            cow = new Cow(new Vector2(100, 400), 0);
        }

        public void LoadContent(ContentManager content)
        {
            if (content != null)
            {
                background.LoadContent(content);
                spaceship.LoadContent(content);
                cow.LoadContent(content);
            }
        }

        private void OperateTractorBeam()
        {
            Vector2 spaceshipCenter = new Vector2(spaceship.X + spaceship.Width / 2, spaceship.Y + spaceship.Height / 2);

            // Objects manager would be useful here to separate liftable objects from others
            // Currently using spaceships width for beam width
            cow.Lift(spaceshipCenter, spaceship.CurrentBeam.Force, spaceship.CurrentBeam.Position, spaceship.Width);
        }

        private String DetermineBeam()
        {
            if (spaceship.CurrentBeam.Name == "Tractor Beam")
                OperateTractorBeam();

            return spaceship.CurrentBeam.Name;
        }

        public void Update(GameTime gameTime)
        {
            spaceshipInput.Update();
            spaceship.Update();
            cow.Update();

            if (spaceship.BeamOn)
                DetermineBeam();

            if (spaceship.X + spaceship.Width >= spaceship.RightBoundary)
            {
                background.setBackgroundImage();
                spaceship.X = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            spaceship.Draw(spriteBatch);
            cow.Draw(spriteBatch);
        }
    }
}
