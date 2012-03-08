namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    public class Cow : LiftObject
    {
        public Cow(Vector2 position, float resistance, GameObjectsManager manager)
            : base(position, resistance, manager)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("cartooncow2");
            Width = image.Width;
            Height = image.Height;
        }
    }
}
