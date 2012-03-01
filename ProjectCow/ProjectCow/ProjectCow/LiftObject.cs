using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ProjectCow
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

        public void lift(bool movingDown, Vector2 pos)
        {
            position.Y--;
        }

        public void drop()
        {
            if (position.Y < 380)
                position.Y += 1;
        }

        public bool tractorRange(Vector2 pos, int ufoWidth, int ufoHeight) //ufowidth not working properly(maybe)...
         {
            float leftEdge = this.position.X - 80;
            float rightEdge = this.position.X + this.getWidth() + 80;
            float topEdge = this.position.Y;

            float ufoBottomEdge = pos.Y + ufoHeight;
            float ufoLeftEdge = pos.X;
            float ufoRightEdge = pos.X + ufoWidth;

            if(leftEdge <= ufoLeftEdge && rightEdge >= ufoRightEdge && ufoBottomEdge <= topEdge)
                return true;
            else
                return false;
        }

        private void UpdateMoveIAO(KeyboardState curkeystate)
        {
                
        }








    }
}
