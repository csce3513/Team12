/*
 * 
 * File: InteractObject.cs
 * 
 * Author: Ronnie Mathes
 * 
 * Date: 02/13/12
 * 
 * Description: InteractObject is a base class for objects the ufo can interact with. 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace ufogame
{
    public class InteractObject
    {

        //Variables for position on screen, uploaded image, and size.
        public Vector2 position = new Vector2(0, 0);
        private Texture2D image;
        private float size = 0;


        //Getter for uploaded image
        public object IAOimage
        {
            get
            { return image; }

        }

        //Getter for size variable
        public object IAOsize
        {
            get
            { return size; }
        }

        //Method for loading the image
        public void LoadIAOImage(ContentManager IAOContentManager, string imagename)
        {
            image = IAOContentManager.Load<Texture2D>(imagename);
        }

        //Basic drawing method
        public void DrawIAOObject(SpriteBatch IAOSpriteBatch)
        {
            IAOSpriteBatch.Draw(image, position, Color.White);
        }



    }
}