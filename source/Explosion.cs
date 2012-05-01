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
    /// Explosion that contains many explosion particles
    /// </summary>
    public class Explosion : GameObject
    {
        // List of explosion particles
        private List<ExplosionParticle> particles;
        // Amount of particles
        public int Amount { get; set; }

        public Explosion(Vector2 position, GameObjectsManager manager, Random random)
            : base(manager)
        {
            Position = position;
            particles = new List<ExplosionParticle>();
            int amount = random.Next(50, 100);
            Amount = amount;
            while (amount > 0)
            {
                particles.Add(new ExplosionParticle(position, new Vector2(random.Next(-2, 3), random.Next(-2, 3)), random.Next(200, 300), manager));
                amount--;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (ExplosionParticle particle in particles)
                particle.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Amount; i++)
            {
                particles[i].Update(gameTime);
                if (particles[i].Life < 0)
                {
                    particles.RemoveAt(i);
                    i--;
                    Amount--;
                }
            }

            if (Amount == 0)
                manager.RemoveObject(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            foreach (ExplosionParticle particle in particles)
                particle.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}
