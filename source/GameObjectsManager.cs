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
        // List of objects that collides with ship (ammo, bomb, etc)
        private List<Projectile> projectiles;

        // List of objects to be removed and added
        // This is so you can add and remove objects while looping through liftObjects
        private List<GameObject> objectsToRemove;
        private List<GameObject> objectsToAdd;

        private Random random;
        private ContentManager contentManager;

        public Spaceship Spaceship { get; set; }

        public GameObjectsManager(Spaceship spaceship, Random random)
        {
            liftObjects = new List<LiftObject>();
            projectiles = new List<Projectile>();
            objectsToRemove = new List<GameObject>();
            objectsToAdd = new List<GameObject>();
            this.random = random;
            Spaceship = spaceship;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.LoadContent(content);

            Spaceship.LoadContent(content);
            contentManager = content;
        }

        // Add the cows at beginning of game
        public void AddStartingCows()
        {
            while(liftObjects.Count < 12)
            {
                Vector2 position = new Vector2(random.Next(-720, 1440), random.Next(360, 400));
                liftObjects.Add(new Cow(position, random.Next(97) / 100.0f, this, random));
            }
        }

        // Add random cow off screen
        public void AddRandomCow()
        {
            Vector2 position = new Vector2(random.Next(0, 600), random.Next(360, 400));

            if (random.Next(2) == 0)
                position.X -= 1440; // Object appears on 2 screens before
            else
                position.X += 1440; // Object appears on 2 screens ahead

            Cow cow = new Cow(position, random.Next(97) / 100.0f, this, random);
            AddObject(cow);
        }

        // Add random cow bomb off screen
        public void AddRandomCowBomb()
        {
            Vector2 position = new Vector2(random.Next(0, 600), random.Next(360, 400));

            if (random.Next(2) == 0)
                position.X -= 1440; // Object appears on previous screen
            else
                position.X += 1440; // Object appears on next screen

            CowBomb bomb = new CowBomb(position, 0, this);
            AddObject(bomb);
        }
        
        // Add random tank off screen
        public void AddRandomTank()
        {
            Vector2 position = new Vector2(random.Next(0, 600), random.Next(360, 400));

            if (random.Next(2) == 0)
                position.X -= 1440; // Object appears on previous screen
            else
                position.X += 1440; // Object appears on next screen

            Tank tank = new Tank(position, this, random);
            AddObject(tank);
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

        public void AddObject(GameObject gameObject)
        {
            if (contentManager != null)
                gameObject.LoadContent(contentManager);
            objectsToAdd.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            objectsToRemove.Add(gameObject);
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
            for (int i = 0; i < objectsToRemove.Count; i++)
            {
                // Remove object from correct list
                if (objectsToRemove[i].GetType().BaseType.Name == "LiftObject")
                    liftObjects.Remove((LiftObject)objectsToRemove[i]);
                else
                    projectiles.Remove((Projectile)objectsToRemove[i]);
            }
            objectsToRemove.Clear();

            for (int i = 0; i < objectsToAdd.Count; i++)
            {
                // Add object from correct list
                if (objectsToAdd[i].GetType().BaseType.Name == "LiftObject")
                    liftObjects.Add((LiftObject)objectsToAdd[i]);
                else
                    projectiles.Add((Projectile)objectsToAdd[i]);
            }
            objectsToAdd.Clear();

            foreach(LiftObject liftObject in liftObjects)
                liftObject.Update(gameTime);
            foreach (Projectile projectile in projectiles)
                projectile.Update(gameTime);

            Spaceship.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Sort so objects that are lower on screen appears in front
            List<LiftObject> sortedList = liftObjects.OrderBy(x => x.OriginalY).ToList();

            foreach (LiftObject liftObject in sortedList)
                liftObject.Draw(spriteBatch);
            foreach (Projectile projectile in projectiles)
                projectile.Draw(spriteBatch);

            Spaceship.Draw(spriteBatch);
        }
    }
}
