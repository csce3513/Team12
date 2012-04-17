namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class LiftObject : GameObject
    {
        // Gravity positive since Y increases as you go down
        protected readonly float GRAVITY = .98f;

        // Original y used to determine where the object will land when dropped
        public float OriginalY { get; protected set; }

        // Bounding box for collision detection
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }

            set
            {
                Position = new Vector2(value.X, value.Y);
                Width = value.Width;
                Height = value.Height;
            }
        }

        protected Vector2 speed;
        public float Width { get; set; }
        public float Height { get; set; }
        // How much points the object is worth
        public int Points { get; set; }
        // How much health is added to spaceship
        public int HealthModifier { get; set; }
        // Bool value to determine if object is captured
        public bool Captured { get; set; }

        // How much it resists the pull
        protected float resistance;

        public LiftObject(Vector2 position, float resistance, GameObjectsManager manager, int points, int healthModifier) : base(manager)
        {
            OriginalY = position.Y;
            Position = position;
            this.resistance = resistance;
            Captured = false;
            Points = points;
            HealthModifier = healthModifier;
        }

        public override void Update(GameTime gameTime)
        {
            speed.Y += this.GRAVITY;
            this.Position += speed;
            if (Position.Y >= OriginalY)
            {
                speed.Y = 0;
                speed.X = 0;
                Position = new Vector2(Position.X, OriginalY);
            }
        }

        public void Lift(Vector2 spaceshipCenter, float pullAcceleration, Vector2 beamPosition, int beamWidth)
        {
            if (IsInBeamRange(beamPosition, beamWidth))
            {
                // Algorithm to pull the object to center of spaceship at given acceleration
                Vector2 objectCenter = new Vector2(Position.X + Width / 2, Position.Y + Height / 2);
                Vector2 direction = spaceshipCenter - objectCenter;
                direction.Normalize();
                if (Position.Y <= spaceshipCenter.Y + 80)
                {
                    Captured = true;
                    speed.Y = 0;
                }
                else
                    speed.Y += direction.Y * pullAcceleration + resistance;

                speed.X += direction.X * (pullAcceleration - resistance);
            }
        }

        // Check if object is below and in beam's range.
        private bool IsInBeamRange(Vector2 beamPosition, int beamWidth)
        {
            // Edges of object
            float leftEdge = this.Position.X;
            float rightEdge = leftEdge + this.Width;

            if ( Position.Y >= beamPosition.Y &&
                ((leftEdge <= beamPosition.X + beamWidth && leftEdge >= beamPosition.X) ||
                (rightEdge >= beamPosition.X && rightEdge <= beamPosition.X + beamWidth) ||
                (leftEdge <= beamPosition.X && rightEdge >= beamPosition.X + beamWidth)))
                return true;
            else
                return false;
        }
    }
}
