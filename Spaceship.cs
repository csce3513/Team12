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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Spaceship
    {
        Vector2 position;
        float moveSpeed;

        Texture2D image;

        public Spaceship()
        {
        }

        public void Initialize()
        {
            position = new Vector2(0, 0);
            moveSpeed = 1;
        }

        public void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("spaceship");
        }

        public void MoveLeft()
        {
            position.X -= moveSpeed;
        }

        public void MoveRight()
        {
            position.X += moveSpeed;
        }

        public void MoveUp()
        {
            position.Y -= moveSpeed;
        }

        public void MoveDown()
        {
            position.Y += moveSpeed;
        }

        public void ProcessInput(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }
            
            if (state.IsKeyDown(Keys.Up))
            {
                MoveUp();
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                MoveDown();
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            ProcessInput(currentState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, Color.White);
        }

        public Vector2 Position { get { return position; } set { position = value; } }
        public float Speed { get { return moveSpeed; } set { moveSpeed = value; } }
    }
}
