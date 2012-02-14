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
    public class SpaceshipInputManager
    {
        Spaceship spaceship;

        public SpaceshipInputManager(Spaceship spaceship)
        {
            this.spaceship = spaceship;
        }

        public void ProcessInputs(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left))
                spaceship.MoveLeft();
            else if (state.IsKeyDown(Keys.Right))
                spaceship.MoveRight();

            if (state.IsKeyDown(Keys.Up))
                spaceship.MoveUp();
            else if (state.IsKeyDown(Keys.Down))
                spaceship.MoveDown();

            if (state.IsKeyDown(Keys.Space))
                spaceship.TractorBeam = true;
            else
                spaceship.TractorBeam = false;
        }

        public void Update()
        {
            ProcessInputs(Keyboard.GetState());
        }
    }
}
