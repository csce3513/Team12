namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Enemy that shoots missles at you
    /// </summary>
    public class Tank : LiftObject
    {
        private Random random;
        private readonly int MOVE_SPEED = 1;

        private Texture2D gunImage;
        // Origin to rotate the gun around
        private Vector2 gunOrigin;
        private int gunWidth;
        private int gunHeight;
        private float gunRotation;
        // Timer to randomly shoot
        private TimeSpan shootTimer;
        private Vector2 GunPosition
        {
            get
            {
                return new Vector2(Position.X + Width / 2, Position.Y + Height / 8);
            }
        }
        private Rectangle gunBoundBox
        {
            get
            {
                return new Rectangle((int)GunPosition.X, (int)GunPosition.Y, gunWidth, gunHeight);
            }
        }

        //rocket sound
        SoundEffect rocket_effect;
        string rocket_string;

        public Tank(Vector2 position, GameObjectsManager manager, Random random)
            : base(position, 0.98f, manager, 25, -10)
        {
            Position = position;
            Width = 100;
            Height = 80;
            gunWidth = 60;
            gunHeight = 10;
            gunRotation = 0;
            shootTimer = new TimeSpan();
            this.random = random;

            int temp = random.Next(2);
            if (temp == 0)
                rocket_string = "bomb-02";
            else if (temp == 1)
                rocket_string = "bomb-03";
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("tankbody");
            gunImage = content.Load<Texture2D>("tankgun");
            // Origin based on real image sizes
            gunOrigin = new Vector2(0, gunImage.Height / 2);
            rocket_effect = content.Load<SoundEffect>(rocket_string);
        }

        // Move towards the screen the player is on
        public void Move()
        {
            if (Position.X <= 0)
                speed.X += MOVE_SPEED;
            else if (Position.X >= 720 - Width)
                speed.X -= MOVE_SPEED;
        }

        // Rotate gun towards spaceship
        public void RotateGun(Vector2 spaceshipPosition, int width, int height)
        {
            Vector2 gunCenter = new Vector2(GunPosition.X, GunPosition.Y + Height / 2);
            Vector2 spaceshipCenter = new Vector2(spaceshipPosition.X + width / 2, spaceshipPosition.Y + height / 2);
            Vector2 direction = gunCenter - spaceshipCenter;

            if (direction.X >= 0)
                gunRotation = (float)(Math.PI - Math.Atan(direction.Y / -direction.X));
            else
                gunRotation = (float)Math.Atan(direction.Y / direction.X);
        }

        // Randomly shoot every second
        private void RandomlyShoot(TimeSpan elapsedTime)
        {
            shootTimer += elapsedTime;
            if (shootTimer.Seconds >= 1)
            {
                shootTimer -= TimeSpan.FromSeconds(1);
                if (Position.X > 0 && Position.X < 720 - Width && random.Next(100) < 20)
                {
                    manager.AddObject(new SmallRocket(GunPosition, manager));
                    rocket_effect.Play(); //couldn't find a reliable way to control how much the sound plays
                                          //so I guess we could just scale back how many tanks spawn at a time
                }
            }
        
        }

        public override void Lift(Vector2 spaceshipCenter, float pullAcceleration, Vector2 beamPosition, int beamWidth)
        {
            base.Lift(spaceshipCenter, pullAcceleration, beamPosition, beamWidth);
            if (Captured)
            {
                rocket_effect.Play();
                manager.AddExplosion(new Vector2(Position.X + Width / 2, Position.Y + Height / 2));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.Y == OriginalY)
                Move();

            RandomlyShoot(gameTime.ElapsedGameTime);

            base.Update(gameTime);

            RotateGun(manager.Spaceship.Position, manager.Spaceship.Width, manager.Spaceship.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, BoundBox, Color.White);
            spriteBatch.Draw(gunImage, gunBoundBox, null, Color.White, gunRotation, gunOrigin, SpriteEffects.None, 0);
        }
    }
}
