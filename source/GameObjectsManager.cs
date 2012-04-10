namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;

    public class GameObjectsManager
    {
        private List<LiftObject> liftObjects;
        private Random random;
        private ContentManager contentManager;

        public GameObjectsManager(Random random)
        {
            liftObjects = new List<LiftObject>();
            this.random = random;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.LoadContent(content);

            contentManager = content;
        }

        // Add a random object off screen
        public void AddRandomObject()
        {
            // TODO: Implement randomness when more objects are created

            Vector2 position = new Vector2(random.Next(0, 600), random.Next(380, 420));

            if (random.Next(2) == 0)
                position.X -= 720; // Object appears on previous screen
            else
                position.X += 720; // Object appears on next screen

            Cow cow = new Cow(position, 0, this, random);
            if (contentManager != null)
                cow.LoadContent(contentManager);
            AddObject(cow);
        }

        // Shift all objects left one screen
        public void ShiftObjectsLeft()
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.Position = new Vector2(liftObject.Position.X - 720, liftObject.Position.Y);
        }

        // Shift all objects right one screen
        public void ShiftObjectsRight()
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.Position = new Vector2(liftObject.Position.X + 720, liftObject.Position.Y);
        }

        public void AddObject(LiftObject liftObject)
        {
            liftObjects.Add(liftObject);
        }

        public void RemoveObject(LiftObject liftObject)
        {
            liftObjects.Remove(liftObject);
        }

        // Returns true if a cow was captured
        public bool LiftObjects(Spaceship spaceship)
        {
            Vector2 spaceshipCenter = new Vector2(spaceship.X + spaceship.Width / 2, spaceship.Y + spaceship.Height / 2);

            // Currently using spaceships width for beam width
            foreach (LiftObject liftObject in liftObjects)
            {
                liftObject.Lift(spaceshipCenter, spaceship.CurrentBeam.Force, spaceship.CurrentBeam.Position, spaceship.Width);
                if (liftObject.Captured)
                {
                    RemoveObject(liftObject);
                    return true;
                }
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            foreach(LiftObject liftObject in liftObjects)
                liftObject.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.Draw(spriteBatch);
        }
    }
}
