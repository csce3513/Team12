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
        public TractorBeam(Vector2 position)
        {
            Position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            // Uncomment when image is found
            //Image = content.Load<Texture2D>("TractorBeam.png");
        }
    }
}
