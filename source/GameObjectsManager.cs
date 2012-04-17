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
        // List of liftable objects in game
        private List<LiftObject> liftObjects;

        // List of objects to be removed and added
        // This is so you can add and remove objects while looping through liftObjects
        private List<GameObject> objectsToRemove;
        private List<GameObject> objectsToAdd;

        private Random random;
        private ContentManager contentManager;

        public GameObjectsManager(Random random)
        {
            liftObjects = new List<LiftObject>();
            objectsToRemove = new List<GameObject>();
            objectsToAdd = new List<GameObject>();
            this.random = random;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.LoadContent(content);

            contentManager = content;
        }

        // Add random cow off screen
        public void AddRandomCow()
        {
            Vector2 position = new Vector2(random.Next(0, 600), random.Next(360, 400));

            if (random.Next(2) == 0)
                position.X -= 720; // Object appears on previous screen
            else
                position.X += 720; // Object appears on next screen

            Cow cow = new Cow(position, random.Next(97) / 100.0f, this, random);
            if (contentManager != null)
                cow.LoadContent(contentManager);
            AddObject(cow);
        }

        // Add random cow bomb off screen
        public void AddRandomCowBomb()
        {
            Vector2 position = new Vector2(random.Next(0, 600), random.Next(360, 400));

            if (random.Next(2) == 0)
                position.X -= 720; // Object appears on previous screen
            else
                position.X += 720; // Object appears on next screen

            CowBomb bomb = new CowBomb(position, 0, this);
            if (contentManager != null)
                bomb.LoadContent(contentManager);
            AddObject(bomb);
        }

        // Add a random object off screen
        public void AddRandomObject()
        {
            if (random.Next(2) == 0)
                AddRandomCow();
            else
                AddRandomCowBomb();
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
            objectsToAdd.Add(liftObject);
        }

        public void RemoveObject(LiftObject liftObject)
        {
            objectsToRemove.Add(liftObject);
        }

        // Returns the captured object
        public LiftObject LiftObjects(Spaceship spaceship)
        {
            Vector2 spaceshipCenter = new Vector2(spaceship.X + spaceship.Width / 2, spaceship.Y + spaceship.Height / 2);

            // Currently using spaceships width for beam width
            foreach (LiftObject liftObject in liftObjects)
            {
                liftObject.Lift(spaceshipCenter, spaceship.CurrentBeam.Force, spaceship.CurrentBeam.Position, spaceship.Width);
                if (liftObject.Captured)
                {
                    RemoveObject(liftObject);
                    return liftObject;
                }
            }

            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (LiftObject liftObject in objectsToRemove)
                liftObjects.Remove(liftObject);
            objectsToRemove.Clear();

            foreach (LiftObject liftObject in objectsToAdd)
                liftObjects.Add(liftObject);
            objectsToAdd.Clear();

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
