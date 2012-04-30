namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Audio;

    /// <summary>
    /// Displays score and health
    /// </summary>
    public class GameInfo
    {
        public int Score { get; set; }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > MaxHealth)
                    health = MaxHealth;
                else
                    health = value;

                healthBarSize.Width = (int) (HEALTH_BAR_WIDTH * (float)health / MaxHealth);
            }
        }
        public int MaxHealth { get; set; }

        private SpriteFont font;
        private Vector2 scorePosition;
        private Vector2 healthPosition;
        private Vector2 healthOutlinePosition;
        // Uses rectangle so you can change the width
        private Rectangle healthBarSize;
        // The value to multiply by to get right width for hp bar
        private readonly int HEALTH_BAR_WIDTH = 121;
        private int health;
        private Texture2D healthBar;
        private Texture2D healthOutline;

        public GameInfo()
        {
            healthBarSize = new Rectangle(52, 37, HEALTH_BAR_WIDTH, 21);
            MaxHealth = 100;
            Health = 100;
            scorePosition = new Vector2(10, 10);
            healthPosition = new Vector2(10, 30);
            healthOutlinePosition = new Vector2(50, 35);
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Ariel");
            healthBar = content.Load<Texture2D>("hpbar");
            healthOutline = content.Load<Texture2D>("hpoutline");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + Score, scorePosition, Color.Black);
            spriteBatch.DrawString(font, "HP: ", healthPosition, Color.Black);
            spriteBatch.Draw(healthOutline, healthOutlinePosition, Color.White);
            spriteBatch.Draw(healthBar, healthBarSize, Color.White);
        }
    }
}
