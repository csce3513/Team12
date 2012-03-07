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

    public class SpaceshipInputManager
    {
        private Spaceship spaceship;

        public SpaceshipInputManager(Spaceship spaceship)
        {
            this.spaceship = spaceship;
        }

        public void ProcessInputs(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A))
                spaceship.MoveLeft();
            else if (state.IsKeyDown(Keys.D))
                spaceship.MoveRight();

            if (state.IsKeyDown(Keys.W))
                spaceship.MoveUp();
            else if (state.IsKeyDown(Keys.S))
                spaceship.MoveDown();

            if (state.IsKeyDown(Keys.Space))
                spaceship.BeamOn = true;
            else
                spaceship.BeamOn = false;
        }

        public void Update()
        {
            ProcessInputs(Keyboard.GetState());
        }
    }
}
