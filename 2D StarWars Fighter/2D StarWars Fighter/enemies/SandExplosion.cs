using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace _2D_StarWars_Fighter.enemies
{
    class SandExplosion
    {
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        private Texture2D[] textures = new Texture2D[8];
        private int counter;
        private int currentFrame;

        public SandExplosion(Vector2 newPosition, Texture2D[] texturesArray)
        {
            currentFrame = 0;
            counter = 10;
            position = newPosition;
            textures = texturesArray;
            texture = texturesArray[0];
        }

        public void Update(GameTime gameTime)
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                texture = textures[currentFrame];
                currentFrame++;
            }
            if (currentFrame >= 8)
            {
                currentFrame = 7;
                isVisible = false;
            }

            if (counter <= 0)
                counter = 10;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }



    }
}
