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
        private SpaceshipInputManager spaceshipInput;
        private LiftObject enemy;

        public ScreenManager Manager { get; set; }

        public GameScreen()
        {
            background = new Background();
            spaceship = new Spaceship();
            spaceshipInput = new SpaceshipInputManager(spaceship);
            enemy = new LiftObject();
        }

        public void LoadContent(ContentManager content)
        {
            if (content != null)
            {
                enemy.LoadIAO(content, "cartooncow2");
                background.LoadContent(content);
                spaceship.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {

            if (spaceship.TractorBeam && enemy.tractorRange(spaceship.getPosition(), spaceship.Width, spaceship.Height) == true)
            {
                enemy.lift(spaceship.movingDown, spaceship.getPosition());
            }

            else
            {
                enemy.drop();
            }
            enemy.UpdateIAO(gameTime);
            spaceshipInput.Update();
            spaceship.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            background.Draw(spriteBatch);
            spaceship.Draw(spriteBatch);
            enemy.DrawIAO(spriteBatch);
        }
    }
}
