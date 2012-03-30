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

        public void AddRandomObject()
        {
            // TODO: Implement randomness when more objects are created

            // TODO: Replace min and max position values with real ones
            Vector2 position = new Vector2(random.Next(0, 700), random.Next(380, 420));

            Cow cow = new Cow(position, 0, this, random);
            if (contentManager != null)
                cow.LoadContent(contentManager);
            AddObject(cow);
        }

        public void AddObject(LiftObject liftObject)
        {
            liftObjects.Add(liftObject);
        }

        public void LiftObjects(Spaceship spaceship)
        {
            Vector2 spaceshipCenter = new Vector2(spaceship.X + spaceship.Width / 2, spaceship.Y + spaceship.Height / 2);

            // Currently using spaceships width for beam width
            foreach(LiftObject liftObject in liftObjects)
                liftObject.Lift(spaceshipCenter, spaceship.CurrentBeam.Force, spaceship.CurrentBeam.Position, spaceship.Width);
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
