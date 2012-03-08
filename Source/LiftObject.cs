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
        private readonly int POINTS = 1;

        // Original y used to determine where the object will land when dropped
        public float OriginalY { get; protected set; }

        protected Vector2 speed;
        public float Width { get; set; }
        public float Height { get; set; }
        public int Points { get { return POINTS; } }

        // How much it resists the pull
        protected float resistance;

        public LiftObject(Vector2 position, float resistance, GameObjectsManager manager) : base(manager)
        {
            OriginalY = position.Y;
            Position = position;
            this.resistance = resistance;
        }

        public override void Update()
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
                if (direction.Y > 0)
                    // Add logic for capturing here
                    speed.Y = 0;
                else
                    speed.Y += direction.Y * pullAcceleration + resistance;

                speed.X += direction.X * pullAcceleration + resistance;
            }
        }

        // Check if object is in beam's range. Only checking width so far.
        private bool IsInBeamRange(Vector2 beamPosition, int beamWidth)
        {
            // Edges of object
            float leftEdge = this.Position.X;
            float rightEdge = leftEdge + this.Width;

            if ((leftEdge <= beamPosition.X + beamWidth && leftEdge >= beamPosition.X) ||
                (rightEdge >= beamPosition.X && rightEdge <= beamPosition.X + beamWidth) ||
                (leftEdge <= beamPosition.X && rightEdge >= beamPosition.X + beamWidth))
                return true;
            else
                return false;
        }
    }
}
