using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _2D_StarWars_Fighter
{
    public class Platform
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position;

        public Platform(Texture2D Newtexture, Vector2 newPosition)
        {
            texture = Newtexture;
            position = newPosition;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);
        }

    }
}
