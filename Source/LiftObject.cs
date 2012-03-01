namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class LiftObject : GameObject
    {
        protected Vector2 speed;
        public float Width { get; set; }
        public float Height { get; set; }
        // How much it resists the pull
        protected float resistance;

        public LiftObject(Vector2 position, float resistance) : base(position)
        {
            this.resistance = resistance;
        }

        public abstract void LoadContent(ContentManager content);
        public override void Update()
        {
            if (this.Position.X != this.OriginalX)
            {
                speed.Y += this.GRAVITY;
                this.Position += speed;
            }
        }

        public void Lift(Vector2 spaceshipCenter, float pullAcceleration, Vector2 beamPosition, int beamWidth)
        {
            if (IsInBeamRange(beamPosition, beamWidth))
            {
                // Algorithm to pull the object to center of spaceship at given acceleration
                Vector2 direction = spaceshipCenter - this.Position;
                direction.Normalize();
                speed.X += direction.X * pullAcceleration + resistance;
                speed.Y += direction.Y * pullAcceleration + resistance;
            }
        }

        // Check if object is in beam's range. Only checking width so far.
        protected bool IsInBeamRange(Vector2 beamPosition, int beamWidth)
        {
            // Edges of object
            float leftEdge = this.Position.X;
            float rightEdge = leftEdge + this.Width;

            if ((leftEdge < beamPosition.X + beamWidth && leftEdge > beamPosition.X) ||
                (rightEdge > beamPosition.X && rightEdge < beamPosition.X + beamWidth))
                return true;
            else
                return false;
        }
    }
}
