namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Fake Cow that hurts the player
    /// </summary>
    public class CowBomb : LiftObject
    {
        SoundEffect cowboom;
        public CowBomb(Vector2 position, float resistance, GameObjectsManager manager)
            : base(position, resistance, manager, 0, -10) // Last two digits are score and health modifiers
        {
            Width = 80;
            Height = 60;
        }

        public override void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("bombercow");
            cowboom = content.Load<SoundEffect>("bomb-02");
        }

        public override void Lift(Vector2 spaceshipCenter, float pullAcceleration, Vector2 beamPosition, int beamWidth)
        {
            base.Lift(spaceshipCenter, pullAcceleration, beamPosition, beamWidth);
            if (Captured == true)
            {
                manager.AddExplosion(new Vector2(Position.X + Width / 2, Position.Y + Height / 2));
                cowboom.Play();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, BoundBox, Color.White);
        }
    }
}
