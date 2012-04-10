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
    /// Displays score and health
    /// </summary>
    public class GameInfo
    {
        public int Score { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        private SpriteFont font;
        private Vector2 scorePosition;
        private Vector2 healthPosition;

        public GameInfo()
        {
            Health = 500;
            MaxHealth = 500;
            scorePosition = new Vector2(10, 10);
            healthPosition = new Vector2(10, 30);
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Ariel");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + Score, scorePosition, Color.Black);
            spriteBatch.DrawString(font, "HP: " + Health + "/" + MaxHealth, healthPosition, Color.Black);
        }
    }
}
