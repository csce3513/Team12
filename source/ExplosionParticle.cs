// -----------------------------------------------------------------------
// <copyright file="ExplosionParticle.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Particles of an explosion
    /// </summary>
    public class ExplosionParticle : GameObject
    {
        // Life of particle in milliseconds
        public int Life { get; set; }
        private Vector2 speed;
        private Vector2 origin;

        public ExplosionParticle(Vector2 position, Vector2 speed, int life, GameObjectsManager manager)
            : base(manager)
        {
            Position = position;
            this.speed = speed;
            Life = life;
            Width = 80;
            Height = 80;
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("explosion");
            origin = new Vector2(image.Width / 2, image.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            Life -= gameTime.ElapsedGameTime.Milliseconds;
            Position += speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, BoundBox, null, Color.White, 0, origin, SpriteEffects.None, 0);
        }
    }
}
