using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ProjectCow
{
    public class StartScreen : IScreen
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Position2 = new Vector2(50, 340);
        public Vector2 Position3 = new Vector2(100, 225);

        //The texture object used when drawing the sprite
        private Texture2D background;
        private Texture2D[] titles = new Texture2D[11];
        private Texture2D[] starts = new Texture2D[2];
        private static GameTime gameTime = new GameTime();
        private int count = 0;
        private int wait = 0;
        private double delay = 0;
        private double delay2 = 0;
        private ContentManager manager;
        private InputManager input;

        protected Song song;

        // Name of pictures (menu, instruction)
        string[] start = new string[2];
        string[] title = new string[11];

        public ScreenManager Manager { get; set; }

        public StartScreen()
        {
            input = new InputManager();
            for (int i = 0; i < 11; i++)
            {
                int j = i + 1;
                title[i] = "sstitle" + j.ToString();
            }
            start[0] = "ssstart";
            start[1] = "ssstartw";
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            
            background = theContentManager.Load<Texture2D>("ssbase");
            for (int i = 0; i < 11; i++)
            {
                titles[i] = theContentManager.Load<Texture2D>(title[i]);
            }
            starts[0] = theContentManager.Load<Texture2D>("ssstart");
            starts[1] = theContentManager.Load<Texture2D>("ssstartw");

            song = theContentManager.Load<Song>("tomb");
            MediaPlayer.Play(song);

            // Only set content manager once
            if (manager == null)
                manager = theContentManager;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(background, Position, Color.White);
            theSpriteBatch.Draw(titles[count], Position2, Color.White);
            if(wait == 0)
                count++;
            if (count == 11)
            {
                count = 0;
                wait = 1;
            }


            if (delay > 3000)
            {
                delay -= 3000;
                wait = 0;
            }

            if(delay2 < 1000)
                theSpriteBatch.Draw(starts[0], Position3, Color.White);
            else
               theSpriteBatch.Draw(starts[1], Position3, Color.White);

            if (delay2 > 2000)
                delay2 = 0;

        }

        public void setMainMenu()
        {
            
            LoadContent(manager);
        }

        public void setInstructionMenu()
        {
           
            LoadContent(manager);
        }

        // Restart the game
        public void Start()
        {
            MediaPlayer.Stop();
            Manager.PushScreen(new GameScreen());
        }

        public void ProcessInputs()
        {
            if (input.IsEnterKeyPressed())
                Start();
        }

        public void Update(GameTime gameTime)
        {
            input.Update();
            ProcessInputs();
            
            delay += gameTime.ElapsedGameTime.Milliseconds;
            delay2 += gameTime.ElapsedGameTime.Milliseconds;
        }

    }
}
