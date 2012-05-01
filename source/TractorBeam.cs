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
        
        // Names for tractorbeam
            string[] names = new string[30];
        // Index of current tractorbeam
            int num;
        //Name of current tractorbeam
            string name;

        public TractorBeam(Vector2 position)
        {
            Position = position;
            Name = "Tractor Beam";
            Force = FORCE;
            Width = WIDTH;

            names[0] = "redbeam4";
            names[1] = "redbeam4";
            names[2] = "redbeam4";
            names[3] = "redbeam4";
            names[4] = "redbeam4";
            names[5] = "redbeam4";
            names[6] = "redbeam4";
            names[7] = "redbeam4";
            names[8] = "redbeam4";
            names[9] = "redbeam4";
            names[10] = "greenbeam4";
            names[11] = "greenbeam4";
            names[12] = "greenbeam4";
            names[13] = "greenbeam4";
            names[14] = "greenbeam4";
            names[15] = "greenbeam4";
            names[16] = "greenbeam4";
            names[17] = "greenbeam4";
            names[18] = "greenbeam4";
            names[19] = "greenbeam4";
            names[20] = "yellowbeam4";
            names[21] = "yellowbeam4";
            names[22] = "yellowbeam4";
            names[23] = "yellowbeam4";
            names[24] = "yellowbeam4";
            names[25] = "yellowbeam4";
            names[26] = "yellowbeam4";
            names[27] = "yellowbeam4";
            names[28] = "yellowbeam4";
            names[29] = "yellowbeam4";
            
            num = 0;

            name = names[0];
        }

        public override void LoadContent(ContentManager content)
        {
            if (manager != null)
            {
                Image = content.Load<Texture2D>(names[num]);
                Width = Image.Width;
            }

            if (manager == null)
                manager = content;
        }
        public override void LoadNextBeam()
        {
            if (num < 29)
                num++;
            else
                num = 0;

            LoadContent(manager);
        }

    }
}
