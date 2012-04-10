namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Screen for displaying the main game.
    /// </summary>
    public class GameScreen : IScreen
    {
        private Background background;
        private Spaceship spaceship;
        private InputManager input;
        private SpaceshipController controller;
        private GameObjectsManager gameObjectsManager;
        private GameInfo hud;
        // Have one random to pass on to all objects
        // or else each random call will be same value
        private Random random;

        public ScreenManager Manager { get; set; }

        public GameScreen()
        {
            background = new Background();
            spaceship = new Spaceship();
            input = new InputManager();
            controller = new SpaceshipController(spaceship, input);
            random = new Random();
            gameObjectsManager = new GameObjectsManager(random);
            gameObjectsManager.AddRandomObject();
            gameObjectsManager.AddRandomObject();
            gameObjectsManager.AddRandomObject();
            hud = new GameInfo();
        }

        public void LoadContent(ContentManager content)
        {
            if (content != null)
            {
                background.LoadContent(content);
                gameObjectsManager.LoadContent(content);
                spaceship.LoadContent(content);
                hud.LoadContent(content);
            }
        }

        // Operate the tractor beam
        private void OperateTractorBeam()
        {
            if (gameObjectsManager.LiftObjects(spaceship))
                hud.Score++;
        }

        // Determine which beam is being used and call the respective function
        private String DetermineBeam()
        {
            if (spaceship.CurrentBeam.Name == "Tractor Beam")
                OperateTractorBeam();

            return spaceship.CurrentBeam.Name;
        }

        public void Update(GameTime gameTime)
        {
            if (random.Next(0, 100) == 0)
                gameObjectsManager.AddRandomObject();

            input.Update();
            controller.Update();
            spaceship.Update();
            gameObjectsManager.Update(gameTime);

            // Check if spaceship tractor beam is on
            if (spaceship.BeamOn)
                DetermineBeam();

            // Check if spaceship hits boundary of screen
            if (spaceship.X + spaceship.Width >= spaceship.RightBoundary)
            {
                background.LoadNextBackground();
                spaceship.X = 0;
                gameObjectsManager.ShiftObjectsLeft();
            }
            else if (spaceship.X < spaceship.LeftBoundary)
            {
                background.LoadPreviousBackground();
                spaceship.X = spaceship.RightBoundary - spaceship.Width - 1;
                gameObjectsManager.ShiftObjectsRight();
            }

            // Check input to call menu
            if (input.IsPKeyPressed())
                Manager.PushScreen(new MenuScreen());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            spaceship.Draw(spriteBatch);
            gameObjectsManager.Draw(spriteBatch);
            hud.Draw(spriteBatch);
        }
    }
}
