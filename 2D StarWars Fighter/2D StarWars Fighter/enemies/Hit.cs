using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

// Попадание по игроку
namespace _2D_StarWars_Fighter.enemies
{
    class Hit
    {
        public Texture2D texture;
        public Vector2 position;
        public Texture2D[] textures = new Texture2D[3];
        public bool isVisible;

        public int counter, currentFrame;

        public Hit(Texture2D[] hitTextures, Vector2 NewPosition)
        {
            counter = 10;
            currentFrame = 0;
            textures = hitTextures;
            texture = hitTextures[0];
            position = NewPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                currentFrame++;
                texture = textures[currentFrame];
            }

            if (currentFrame >= 2)
            {
                currentFrame = 0;
                isVisible = false;
            }

            if (counter <= 0)
                counter = 10;
        }

    }
}
