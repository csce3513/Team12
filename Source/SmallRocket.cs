// -----------------------------------------------------------------------
// <copyright file="SmallRocket.cs" company="">
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
    /// Small rockets from tanks
    /// </summary>
    public class SmallRocket : Projectile
    {
        private Vector2 origin;

        public SmallRocket(Vector2 position, GameObjectsManager manager)
            : base(position, -5, manager)
        {
            Width = 30;
            Height = 10;
            SetVelocity(manager.Spaceship.Position, manager.Spaceship.Width, manager.Spaceship.Height);
            SetRotation();
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("bomb");
            origin = new Vector2(0, image.Height / 2);
        }

        private void SetVelocity(Vector2 spaceshipPosition, int width, int height)
        {
            Vector2 rocketCenter = new Vector2(Position.X, Position.Y + Height / 2);
            Vector2 spaceshipCenter = new Vector2(spaceshipPosition.X + width / 2, spaceshipPosition.Y + height / 2);
            velocity = spaceshipCenter - rocketCenter;
            velocity.Normalize();
            velocity *= 12;
        }

        private void SetRotation()
        {
            if (velocity.X >= 0)
                rotation = (float)(Math.PI - Math.Atan(velocity.Y / -velocity.X));
            else
                rotation = (float)Math.Atan(velocity.Y / velocity.X);
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.X > 720 || Position.X < -Width || Position.Y > 480 || Position.Y < -Height)
                manager.RemoveObject(this);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, BoundBox, null, Color.White, rotation, origin, SpriteEffects.None, 0);
        }
    }
}
