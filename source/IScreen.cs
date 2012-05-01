namespace ProjectCow
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Interface for the different game screens. (Menu, pause, game, etc)
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Instance of screen manager to switch screens.
        /// </summary>
        ScreenManager Manager { get; set; }

        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
