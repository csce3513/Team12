using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ufogame
{
    class LiftObject : InteractObject
    {
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

      
        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;
        KeyboardState prevkeystate;


        public void UpdateIAO(GameTime gametime)
        {
            KeyboardState curkeystate = Keyboard.GetState();
            UpdateMoveIAO(curkeystate);
            prevkeystate = curkeystate;
            base.UpdateIAO(gametime, speed, direction);
        }

        private void UpdateMoveIAO(KeyboardState curkeystate)
        {
           
                speed = Vector2.Zero;
                direction = Vector2.Zero;

                if (curkeystate.IsKeyDown(Keys.Space) == true)
                {
                    speed.Y = 160;
                    direction.Y = MOVE_UP;
                    scale -= 0.01f;
                }
                else
                {
                    if (position.Y < 350)
                    {
                        speed.Y = 160;
                        direction.Y = MOVE_DOWN;
                        scale += 0.01f;
                    }
                }
                
        }








    }
}
