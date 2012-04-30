namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Controls the spaceship
    /// </summary>
    public class SpaceshipController
    {
        private InputManager input;
        private Spaceship spaceship;

        public SpaceshipController(Spaceship spaceship, InputManager input)
        {
            this.spaceship = spaceship;
            this.input = input;
        }

        public void ProcessInput()
        {
            if (input.IsAKeyDown())
                spaceship.MoveLeft();
            else if (input.IsDKeyDown())
                spaceship.MoveRight();

            if (input.IsWKeyDown())
                spaceship.MoveUp();
            else if (input.IsSKeyDown())
                spaceship.MoveDown();

            if (input.IsSpaceKeyDown())
                spaceship.BeamOn = true;
            else
                spaceship.BeamOn = false;
        }

        public void Update()
        {
            ProcessInput();
        }
    }
}
