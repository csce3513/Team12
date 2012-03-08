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
    /// Manage the various screens.
    /// </summary>
    public class ScreenManager
    {
        public Stack<IScreen> Screens { get; private set; }
        public ContentManager Content { get; private set; }
        public bool Exit { get; set; }

        public ScreenManager()
        {
            Screens = new Stack<IScreen>();
        }

        // Add a screen to the top of the stack.
        public void PushScreen(IScreen screen)
        {
            screen.Manager = this;
            screen.LoadContent(Content);
            Screens.Push(screen);
        }

        // Remove the screen at the top of the stack.
        public void PopScreen()
        {
            if (Screens.Count > 0)
                Screens.Pop();
        }

        // Set the content manager to pass to screens.
        public void Load(ContentManager content)
        {
            this.Content = content;
        }

        // Update the top screen.
        public void Update(GameTime gameTime)
        {
            Screens.Peek().Update(gameTime);
        }

        // Draw the top screen
        public void Draw(SpriteBatch spriteBatch)
        {
            Screens.Peek().Draw(spriteBatch);
        }
    }
}
