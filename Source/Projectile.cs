namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Objects shot at the spaceship
    /// </summary>
    public abstract class Projectile : GameObject
    {
        public int HealthModifier { get; set; }
        protected Vector2 velocity;
        protected float rotation;

        public Projectile(Vector2 position, int healthModifier, GameObjectsManager manager)
            : base(manager)
        {
            Position = position;
            HealthModifier = healthModifier;
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity;
        }
    }
}
