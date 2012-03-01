namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class GameObject
    {
        // Gravity positive since Y increases as you go down
        protected readonly float GRAVITY = 9.8f;

        // Original x used to determine where the object will land when dropped
        public float OriginalX { get; protected set; }
        public Vector2 Position { get; set; }
        protected Texture2D image;

        // Initialize position
        public GameObject(Vector2 position)
        {
            OriginalX = position.X;
            Position = position;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
