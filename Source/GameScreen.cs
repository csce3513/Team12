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
        public ScreenManager Manager { get; set; }
        private Background background;
        private Spaceship spaceship;
        private SpaceshipInputManager spaceshipInput;
        private GameObjectsManager gameObjectsManager;
        private MenuScreen menu;//////////////////////////////////////////////////////////
        private MenuInputManager menuInput;////////////////////////////////////////////////
        private Game1 game;
        // Have one random to pass on to all objects
        // or each random call will be same value
        private Random random;


        public GameScreen()
        {
            background = new Background();
            spaceship = new Spaceship();
            spaceshipInput = new SpaceshipInputManager(spaceship);
            random = new Random();
            gameObjectsManager = new GameObjectsManager(random);
            game = new Game1();
            gameObjectsManager.AddRandomObject();
            gameObjectsManager.AddRandomObject();
            gameObjectsManager.AddRandomObject();
        }

        public void LoadContent(ContentManager content)
        {
            if (content != null)
            {
                background.LoadContent(content);
                gameObjectsManager.LoadContent(content);
                spaceship.LoadContent(content);
                menu = new MenuScreen(background);
                menuInput = new MenuInputManager(menu, Manager, background);
            }
        }

        private void OperateTractorBeam()
        {
            gameObjectsManager.LiftObjects(spaceship);
        }

        private String DetermineBeam()
        {
            if (spaceship.CurrentBeam.Name == "Tractor Beam")
                OperateTractorBeam();

            return spaceship.CurrentBeam.Name;
        }

        public void Update(GameTime gameTime)
        {
            spaceshipInput.Update();
            spaceship.Update();
            menuInput.Update();
            gameObjectsManager.Update(gameTime);

            if (spaceship.BeamOn)
                DetermineBeam();

            if (spaceship.X + spaceship.Width >= spaceship.RightBoundary)
            {
                background.LoadNextBackground();
                spaceship.X = 0;
            }
            else if (spaceship.X < spaceship.LeftBoundary)
            {
                background.LoadPreviousBackground();
                spaceship.X = spaceship.RightBoundary - spaceship.Width - 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            spaceship.Draw(spriteBatch);
            gameObjectsManager.Draw(spriteBatch);
        }
    }
}
