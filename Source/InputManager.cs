namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Capture all inputs
    /// </summary>
    public class InputManager
    {
        // Previous state included to determine when a key is pressed down then released
        private KeyboardState currentState;
        private KeyboardState previousState;

        public InputManager()
        {
            currentState = Keyboard.GetState();
            previousState = currentState;
        }

        public bool IsWKeyDown()
        {
            if (currentState.IsKeyDown(Keys.W))
                return true;
            else
                return false;
        }

        public bool IsAKeyDown()
        {
            if (currentState.IsKeyDown(Keys.A))
                return true;
            else
                return false;
        }

        public bool IsSKeyDown()
        {
            if (currentState.IsKeyDown(Keys.S))
                return true;
            else
                return false;
        }

        public bool IsDKeyDown()
        {
            if (currentState.IsKeyDown(Keys.D))
                return true;
            else
                return false;
        }

        public bool IsSpaceKeyDown()
        {
            if (currentState.IsKeyDown(Keys.Space))
                return true;
            else
                return false;
        }

        public bool IsPKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.P) && !previousState.IsKeyDown(Keys.P))
                return true;
            else
                return false;
        }

        public bool IsCKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.C) && !previousState.IsKeyDown(Keys.C))
                return true;
            else
                return false;
        }

        public bool IsQKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.Q) && !previousState.IsKeyDown(Keys.Q))
                return true;
            else
                return false;
        }

        public bool IsRKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.R) && !previousState.IsKeyDown(Keys.R))
                return true;
            else
                return false;
        }

        public bool IsBKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.B) && !previousState.IsKeyDown(Keys.B))
                return true;
            else
                return false;
        }

        public bool IsIKeyPressed()
        {
            if (currentState.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                return true;
            else
                return false;
        }

        public void Update()
        {
            // Set the previous state before updating current state
            previousState = currentState;
            currentState = Keyboard.GetState();
        }
    }
}
