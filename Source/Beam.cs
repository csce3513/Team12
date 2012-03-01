namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Abstract class to implement the various beams.
    /// </summary>
    public abstract class Beam
    {
        public Vector2 Position { get; protected set; }
        protected Texture2D Image { get; set; }

        public abstract void LoadContent(ContentManager content);

        // Get position from spaceship to align beam
        public void Update(Vector2 position)
        {
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}
