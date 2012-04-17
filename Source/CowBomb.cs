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
    /// Fake Cow that hurts the player
    /// </summary>
    public class CowBomb : LiftObject
    {
        public CowBomb(Vector2 position, float resistance, GameObjectsManager manager)
            : base(position, resistance, manager, 0, -10) // Last two digits are score and health modifiers
        {
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("bombercow");
            Width = image.Width;
            Height = image.Height;
        }
    }
}
