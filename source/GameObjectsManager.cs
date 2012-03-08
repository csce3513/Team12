namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    public class GameObjectsManager
    {
        private List<LiftObject> liftObjects;
        private Random random;

        public GameObjectsManager()
        {
            liftObjects = new List<LiftObject>();
            random = new Random();
        }

        public void AddRandomObject()
        {
            // TODO: Implement randomness once more objects are created

            // TODO: Replace min and max position values with real ones
            Vector2 position = new Vector2(random.Next(0, 700), random.Next(380, 420));

            AddObject(new Cow(position, 0, this));
        }

        public void AddObject(LiftObject liftObject)
        {
            liftObjects.Add(liftObject);
        }
    }
}
