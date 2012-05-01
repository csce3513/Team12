namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class LiftObject : GameObject
    {
        // Gravity positive since Y increases as you go down
        protected readonly float GRAVITY = .98f;

        // Original y used to determine where the object will land when dropped
        public float OriginalY { get; protected set; }

        protected Vector2 speed;
        public Vector2 Speed { get { return speed; } set { speed = value; } }
        // How much points the object is worth
        public int Points { get; set; }
        // How much health is added to spaceship
        public int HealthModifier { get; set; }
        // Bool value to determine if object is captured
        public bool Captured { get; set; }

        // How much it resists the pull
        protected float resistance;

        // How long the object stays in object manager
        protected TimeSpan timeAlive;

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

        public virtual void Lift(Vector2 spaceshipCenter, float pullAcceleration, Vector2 beamPosition, int beamWidth)
        {
            if (IsInBeamRange(beamPosition, beamWidth))
            {
                // Algorithm to pull the object to center of spaceship at given acceleration
                Vector2 objectCenter = new Vector2(Position.X + Width / 2, Position.Y + Height / 2);
                Vector2 direction = spaceshipCenter - objectCenter;
                direction.Normalize();
                if (Position.Y <= spaceshipCenter.Y + 20)
                {
                    Captured = true;
                    speed.Y = 0;
                }
                else
                    speed.Y += direction.Y * pullAcceleration + resistance;

                speed.X += direction.X * (pullAcceleration - resistance);
            }
        }

        // Check collision with other lift objects
        public bool IsCollided(LiftObject liftObject)
        {
            if (liftObject.Position.Y == liftObject.OriginalY)
            {
                // Create new box that's 1/2 size
                Rectangle softBoundBox = new Rectangle((int)(liftObject.BoundBox.X + liftObject.Width / 4), (int)(liftObject.BoundBox.Y + liftObject.Height / 4), (int)(liftObject.Width / 2), (int)(liftObject.Height / 2));
                return BoundBox.Intersects(softBoundBox);
            }
            else
                return false;
        }

        // Check if object is below and in beam's range.
        private bool IsInBeamRange(Vector2 beamPosition, int beamWidth)
        {
            // Edges of object
            float leftEdge = this.Position.X;
            float rightEdge = leftEdge + this.Width;

            if ( Position.Y >= beamPosition.Y - 20 &&
                ((leftEdge <= beamPosition.X + beamWidth && leftEdge >= beamPosition.X) ||
                (rightEdge >= beamPosition.X && rightEdge <= beamPosition.X + beamWidth) ||
                (leftEdge <= beamPosition.X && rightEdge >= beamPosition.X + beamWidth)))
                return true;
            else
                return false;
        }
    }
}
