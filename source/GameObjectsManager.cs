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
                liftObjects.Add(new Cow(position, 0.8f, this, random));
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

            Cow cow = new Cow(position, 0.8f, this, random);
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
            foreach (Projectile projectile in projectiles)
                projectile.Position = new Vector2(projectile.Position.X - 720, projectile.Position.Y);
        }

        // Shift all objects right one screen
        public void ShiftObjectsRight()
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.Position = new Vector2(liftObject.Position.X + 720, liftObject.Position.Y);
            foreach (Projectile projectile in projectiles)
                projectile.Position = new Vector2(projectile.Position.X + 720, projectile.Position.Y);
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

        // Checks for all collisions and return the amount of damage to spaceship
        public int CheckCollisions()
        {
            // First check collisions between cows and tanks

            // Separate cows and tanks
            List<LiftObject> fallingObjects = new List<LiftObject>();
            List<Tank> tanks = new List<Tank>();

            foreach(LiftObject liftObject in liftObjects)
            {
                if (liftObject.Speed.Y > 15)
                    fallingObjects.Add(liftObject);
                else if (liftObject.GetType().Name == "Tank" && liftObject.Position.X > -liftObject.Width && liftObject.Position.X < 720)
                    tanks.Add((Tank)liftObject);
            }

            foreach (LiftObject fallingObject in fallingObjects)
            {
                foreach (Tank tank in tanks)
                {
                    if (fallingObject.IsCollided(tank))
                    {
                        RemoveObject(tank);
                        fallingObject.Speed = new Vector2(fallingObject.Speed.X, -fallingObject.Speed.Y);
                        break;
                    }
                }
            }

            // Now check collision between projectiles and spaceship
            int healthModifier = 0;
            foreach (Projectile projectile in projectiles)
            {
                Rectangle softBoundBox = new Rectangle((int)(projectile.BoundBox.X + projectile.Width / 8), (int)(projectile.BoundBox.Y + projectile.Height / 8), (int)(3 * projectile.Width / 4), (int)(3 * projectile.Height / 4));
                if (Spaceship.BoundBox.Intersects(softBoundBox))
                {
                    healthModifier += projectile.HealthModifier;
                    RemoveObject(projectile);
                }
            }

            return healthModifier;
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
