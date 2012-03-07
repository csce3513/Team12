namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class TractorBeam : Beam
    {
        private readonly float FORCE = 1.98f;
        private readonly int WIDTH = 50;

        public TractorBeam(Vector2 position)
        {
            Position = position;
            Name = "Tractor Beam";
            Force = FORCE;
            Width = WIDTH;
        }

        public override void LoadContent(ContentManager content)
        {
            // Uncomment when image is found
            // Image = content.Load<Texture2D>("TractorBeam.png");
            // Width = Image.Width;
        }
    }
}
