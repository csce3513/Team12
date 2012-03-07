namespace ProjectCowTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using ProjectCow;

    public class LiftObjectTestClass : LiftObject
    {
        public LiftObjectTestClass(Vector2 position, float resistance)
            : base(position, resistance)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public bool IsInBeamRange(Vector2 beamPosition, int beamWidth)
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
