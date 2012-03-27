namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    public class Cow : LiftObject
    {
        private Random random;
        private TimeSpan actionDuration;

        // Actions
        private bool moveLeft;
        private bool moveRight;
        // Causes cow to move 3x as fast
        private bool frightened;

        private readonly int MOVE_SPEED = 1;

        public Cow(Vector2 position, float resistance, GameObjectsManager manager)
            : base(position, resistance, manager)
        {
            random = new Random();
            actionDuration = new TimeSpan();

            moveLeft = false;
            moveRight = false;
            frightened = false;
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("cartooncow2");
            Width = image.Width;
            Height = image.Height;
        }

        private void MoveRandomly(GameTime gameTime)
        {
            if (Position.Y == OriginalY)
            {
                // Subtract the elapsed time
                actionDuration = actionDuration.Subtract(gameTime.ElapsedGameTime);

                // If action duration runs out
                if (actionDuration.Ticks <= 0)
                {
                    moveLeft = false;
                    moveRight = false;
                    int nextAction;

                    // Always move if frightened
                    if (frightened)
                        nextAction = random.Next(2);
                    else
                        nextAction = random.Next(4);

                    if (nextAction == 0)
                        moveLeft = true;
                    else if (nextAction == 1)
                        moveRight = true;
                    // If nextAction > 1 then stay still

                    int milliseconds = random.Next(500, 1000);
                    actionDuration = actionDuration.Add(new TimeSpan(0, 0, 0, 0, milliseconds));
                }
            }
            else
            {
                moveLeft = false;
                moveRight = false;
                frightened = true;
                actionDuration = TimeSpan.Zero;
            }
        }

        public override void Update(GameTime gameTime)
        {
            int moveSpeed;

            if (frightened)
                moveSpeed = MOVE_SPEED * 3;
            else
                moveSpeed = MOVE_SPEED;

            MoveRandomly(gameTime);

            if (moveLeft)
                speed.X -= moveSpeed;
            else if (moveRight)
                speed.X += moveSpeed;
            else if (Position.Y == OriginalY)
                speed.X = 0;
            
            base.Update(gameTime);
        }
    }
}
