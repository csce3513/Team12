namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Audio;

    public class GameObjectsManager
    {
        // List of liftable objects in game
        private List<LiftObject> liftObjects;
        // List of objects that collides with ship (ammo, bomb, etc)
        private List<Projectile> projectiles;
        // List of explosions
        private List<Explosion> explosions;

        // Explosion delay of spaceship
        private int explodeDelay;

        // List of objects to be removed and added
        // This is so you can add and remove objects while looping through liftObjects
        private List<GameObject> objectsToRemove;
        private List<GameObject> objectsToAdd;

        private Random random;
        private ContentManager contentManager;
        private SoundEffect explosionEffect;

        public Spaceship Spaceship { get; set; }

        public GameObjectsManager(Spaceship spaceship, Random random)
        {
            liftObjects = new List<LiftObject>();
            projectiles = new List<Projectile>();
            explosions = new List<Explosion>();
            objectsToRemove = new List<GameObject>();
            objectsToAdd = new List<GameObject>();
            explodeDelay = 0;
            this.random = random;
            Spaceship = spaceship;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (LiftObject liftObject in liftObjects)
                liftObject.LoadContent(content);
            explosionEffect = content.Load<SoundEffect>("bomb-03");
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

        // Add explosion to screen at given position
        public void AddExplosion(Vector2 position)
        {
            AddObject(new Explosion(position, this, random));
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

        // Play the explosion animation of spaceship
        public void ExplodeShip(int milliseconds)
        {
            explodeDelay += milliseconds;
            if (explodeDelay > 150)
            {
                AddExplosion(new Vector2(Spaceship.X + random.Next(Spaceship.Width), Spaceship.Y + random.Next(Spaceship.Height)));
                explosionEffect.Play(0.1f, 0, 0);
                explodeDelay -= 150;
            }
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
                if (liftObject.Speed.Y > 10)
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
                        AddExplosion(new Vector2(tank.Position.X + tank.Width / 2, tank.Position.Y + tank.Height / 2));
                        explosionEffect.Play();
                        // Cowbombs and tanks gets removed when collide with tank
                        if (fallingObject.GetType().Name != "Cow")
                        {
                            AddExplosion(new Vector2(fallingObject.Position.X + fallingObject.Width / 2, fallingObject.Position.Y + fallingObject.Height / 2));
                            explosionEffect.Play();
                            RemoveObject(fallingObject);
                        }
                        else
                            fallingObject.Speed = new Vector2(fallingObject.Speed.X, -fallingObject.Speed.Y);
                        RemoveObject(tank);
                        break;
                    }
                }
            }

            // Now check collision between projectiles and spaceship
            int healthModifier = 0;
            foreach (Projectile projectile in projectiles)
            {
                Rectangle softBoundBox = new Rectangle((int)(Spaceship.BoundBox.X + Spaceship.Width / 4), (int)(Spaceship.BoundBox.Y + Spaceship.Height / 4), (int)(Spaceship.Width / 2), (int)(Spaceship.Height / 2));
                if (projectile.BoundBox.Intersects(softBoundBox))
                {
                    AddExplosion(projectile.Position);
                    explosionEffect.Play();
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
                Type type = objectsToRemove[i].GetType();
                // Remove object from correct list
                if (type.BaseType.Name == "LiftObject")
                    liftObjects.Remove((LiftObject)objectsToRemove[i]);
                else if (type.BaseType.Name == "Projectile")
                    projectiles.Remove((Projectile)objectsToRemove[i]);
                else
                    explosions.Remove((Explosion)objectsToRemove[i]);
            }
            objectsToRemove.Clear();

            for (int i = 0; i < objectsToAdd.Count; i++)
            {
                Type type = objectsToAdd[i].GetType();
                // Add object from correct list
                if (type.BaseType.Name == "LiftObject")
                    liftObjects.Add((LiftObject)objectsToAdd[i]);
                else if (type.BaseType.Name == "Projectile")
                    projectiles.Add((Projectile)objectsToAdd[i]);
                else
                    explosions.Add((Explosion)objectsToAdd[i]);
            }
            objectsToAdd.Clear();

            foreach(LiftObject liftObject in liftObjects)
                liftObject.Update(gameTime);
            foreach (Projectile projectile in projectiles)
                projectile.Update(gameTime);
            foreach (Explosion explosion in explosions)
                explosion.Update(gameTime);

            Spaceship.Update();
            if (!Spaceship.IsAlive)
                ExplodeShip(gameTime.ElapsedGameTime.Milliseconds);
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
            foreach (Explosion explosion in explosions)
                explosion.Draw(spriteBatch);
        }
    }
}
