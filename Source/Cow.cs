namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework;

    public class Cow : LiftObject
    {
        private readonly TimeSpan MAX_ALIVE_TIME = TimeSpan.FromSeconds(60);

        private Random random;
        private TimeSpan actionDuration;

        // Frightened image (currently angry image)
        private Texture2D frightenedImage;
        // Current image to display
        private Texture2D currentImage;

        // Actions
        private bool moveLeft;
        private bool moveRight;
        // Causes cow to move 3x as fast
        private bool frightened;

        // Save direction it was facing
        private SpriteEffects spriteEffect;

        private readonly int MOVE_SPEED = 1;
        private int currentMoveSpeed;

        //cow sound
        SoundEffect cowmoo;
        string cowsound;
        bool isMooing = false;

        public Cow(Vector2 position, float resistance, GameObjectsManager manager, Random random)
            : base(position, resistance, manager, 1, 1)
        {
            this.random = random;
            actionDuration = new TimeSpan();
            spriteEffect = SpriteEffects.None;
            currentMoveSpeed = MOVE_SPEED;
            Width = 80;
            Height = 60;

            moveLeft = false;
            moveRight = false;
            frightened = false;

            int temp = random.Next(5);
            if (temp == 0)
                cowsound = "cow-moo1";
            else if (temp == 1)
                cowsound = "cow-moo2";
            else if (temp == 2)
                cowsound = "cow-moo3";
            else if (temp == 3)
                cowsound = "cow-moo4";
            else if (temp == 4)
                cowsound = "cow-moo5";
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("cartooncow2");
            frightenedImage = content.Load<Texture2D>("angrycartooncow");
            currentImage = image;
            cowmoo = content.Load<SoundEffect>(cowsound);
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
                    {
                        moveLeft = true;
                        spriteEffect = SpriteEffects.None;
                    }
                    else if (nextAction == 1)
                    {
                        moveRight = true;
                        spriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    // If nextAction > 1 then stay still

                    int milliseconds = random.Next(500, 1000);
                    actionDuration = actionDuration.Add(new TimeSpan(0, 0, 0, 0, milliseconds));
                }
            }
            else
            {
                // Object is being lifted so be frightened and don't move
                moveLeft = false;
                moveRight = false;
                frightened = true;
                currentMoveSpeed = MOVE_SPEED * 3;
                currentImage = frightenedImage;
                actionDuration = TimeSpan.Zero;

                if (isMooing == false)
                {
                    cowmoo.Play();
                }

                isMooing = true;
            }
        }

        // Move according to current action
        private void Move()
        {
            if (moveLeft)
                speed.X -= currentMoveSpeed;
            else if (moveRight)
                speed.X += currentMoveSpeed;
            else if (Position.Y == OriginalY)
                speed.X = 0;
        }

        public override void Update(GameTime gameTime)
        {
            MoveRandomly(gameTime);
            Move();

            base.Update(gameTime);

            // Remove object after a certain amount of time if it's off screen
            timeAlive += gameTime.ElapsedGameTime;
            if (timeAlive >= MAX_ALIVE_TIME && (Position.X < -Width || Position.X > 720))
                manager.RemoveObject(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentImage, BoundBox, null, Color.White, 0, Vector2.Zero, spriteEffect, 0);
        }
    }
}
