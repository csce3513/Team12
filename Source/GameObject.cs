﻿namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        protected Texture2D image;
        protected GameObjectsManager manager;

        public GameObject(GameObjectsManager manager)
        {
            this.manager = manager;
            Scale = 1.0f;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
