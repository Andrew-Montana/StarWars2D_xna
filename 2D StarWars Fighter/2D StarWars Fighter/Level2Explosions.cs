using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter
{
    class Level2Explosions
    {
        // animation of explosion
        public Texture2D explosionTexture;
        public Vector2 explosionPosition, explosionOrigin;
        public Rectangle explosionRectangle;
        public int spriteWidth, spriteHeight, currentFrame, counter;
        public bool isVisible;

        public Level2Explosions(Vector2 newPosition, Texture2D newTexture)
        {
            //animation
            explosionTexture = newTexture;
            currentFrame = 0;
            counter = 7;
            spriteHeight = 400;
            spriteWidth = 400;
            explosionPosition = newPosition;
            explosionRectangle = new Rectangle((int)explosionPosition.X, (int)explosionPosition.Y, spriteWidth, spriteHeight);
            explosionOrigin = new Vector2(explosionTexture.Width / 2, explosionTexture.Height / 2);
        }


        public void Update(GameTime gameTime)
        {
            if(counter > 0)
                counter--;

            if (counter <= 0)
                currentFrame++;

            if (currentFrame > 9)
            {
                currentFrame = 1;
            }

            if (currentFrame >= 0 && currentFrame <= 3)
            {
                explosionRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                explosionOrigin = new Vector2(200,200);
            }
            if (currentFrame >= 3 && currentFrame <= 6)
            {
                explosionRectangle = new Rectangle((currentFrame - 3) * spriteWidth, 400, spriteWidth, spriteHeight);
                explosionOrigin = new Vector2(200, 200);
                
            }
            if (currentFrame >= 6 && currentFrame <= 9)
            {
                explosionRectangle = new Rectangle((currentFrame - 6) * spriteWidth, 800, spriteWidth, spriteHeight);
                explosionOrigin = new Vector2(200,200);
            }

            if (counter <= 0)
                counter = 7;

            if (currentFrame == 9)
                isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(explosionTexture, explosionPosition, new Rectangle(400, 0, 400, 400), Color.White, 0f, new Vector2(400/2, 400/2), 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(explosionTexture, explosionPosition, explosionRectangle, Color.White, 0f, explosionOrigin, 1.0f, SpriteEffects.None, 0);
        }

    }
}
