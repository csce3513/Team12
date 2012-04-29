namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        protected Texture2D image;
        protected GameObjectsManager manager;
        public float Width { get; set; }
        public float Height { get; set; }
        // Bounding box for collision detection
        public Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }

        public GameObject(GameObjectsManager manager)
        {
            this.manager = manager;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
