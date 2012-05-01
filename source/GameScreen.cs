namespace ProjectCow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;

    /// <summary>
    /// Screen for displaying the main game.
    /// </summary>
    public class GameScreen : IScreen
    {
        private Background background;
        private InputManager input;
        private SpaceshipController controller;
        private GameObjectsManager gameObjectsManager;
        private GameInfo hud;
        // Have one random to pass on to all objects
        // or else each random call will be same value
        private Random random;
        // TimeSpan to generate random object every second
        private TimeSpan generateObjectTimer;
        private Song song;

        public ScreenManager Manager { get; set; }

        public GameScreen()
        {
            background = new Background();
            Spaceship spaceship = new Spaceship();
            input = new InputManager();
            controller = new SpaceshipController(spaceship, input);
            random = new Random();
            gameObjectsManager = new GameObjectsManager(spaceship, random);
            hud = new GameInfo();
            gameObjectsManager.AddStartingCows();
        }

        public void LoadContent(ContentManager content)
        {
            if (content != null)
            {
                background.LoadContent(content);
                gameObjectsManager.LoadContent(content);
                hud.LoadContent(content);
                song = content.Load<Song>("gamesong2");
                MediaPlayer.Play(song);
            }
        }

        // Operate the tractor beam
        private void OperateTractorBeam()
        {
            LiftObject liftObject = gameObjectsManager.LiftObjects(gameObjectsManager.Spaceship);
            if (liftObject != null)
            {
                hud.Score += liftObject.Points;
                hud.Health += liftObject.HealthModifier;
            }
        }

        // Determine which beam is being used and call the respective function
        private String DetermineBeam()
        {
            if (gameObjectsManager.Spaceship.CurrentBeam.Name == "Tractor Beam")
                OperateTractorBeam();

            return gameObjectsManager.Spaceship.CurrentBeam.Name;
        }

        private void GenerateRandomObjects()
        {
            // Create cow every second
            gameObjectsManager.AddRandomCow();

            // 50% chance to create bomb cow every second when score >= 10
            if (hud.Score >= 15 && random.Next(100) < 30)
                gameObjectsManager.AddRandomCowBomb();

            // 30% chance to create tank every second when score >= 30
            if (hud.Score >= 30 && random.Next(100) < 30)
                gameObjectsManager.AddRandomTank();
        }

        public void Update(GameTime gameTime)
        {
            Spaceship spaceship = gameObjectsManager.Spaceship;

            generateObjectTimer += gameTime.ElapsedGameTime;
            if (generateObjectTimer.Seconds >= 1)
            {
                GenerateRandomObjects();
                generateObjectTimer -= TimeSpan.FromSeconds(1);
            }

            input.Update();
            controller.Update();
            gameObjectsManager.Update(gameTime);

            hud.Health += gameObjectsManager.CheckCollisions();

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

            if (hud.Health <= 0)
            {
                spaceship.IsAlive = false;
                if (spaceship.Y > 480 - spaceship.Height)
                {
                    Manager.PopScreen();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(((StartScreen)Manager.Screens.Last()).StartSong);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            gameObjectsManager.Draw(spriteBatch);
        }
    }
}
