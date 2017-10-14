using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter.enemies
{
    class DroidDesAnimation
    {
        public Texture2D texture;
        public Texture2D[] sprites = new Texture2D[5];
        public Vector2 position;
        public bool isVisible;
        public int counter;
        public int currentFrame;

        public DroidDesAnimation(Texture2D[] droidDestroySpriteList, Vector2 newPosition)
        {
            currentFrame = 0;
            counter = 8;
            sprites = droidDestroySpriteList;
            position = newPosition;
            texture = sprites[0];
        }

        public void Update(GameTime gameTime)
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                texture = sprites[currentFrame];
                currentFrame++;
            }

            if (currentFrame >= 5)
            {
                currentFrame = 0;
                isVisible = false;
            }

            if (counter <= 0)
                counter = 11 + currentFrame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
