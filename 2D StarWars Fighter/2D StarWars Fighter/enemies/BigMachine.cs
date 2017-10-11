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
    class BigMachine
    {
        public Texture2D texture, stand, walk1, walk2;
        public Vector2 position;
        public bool isVisible;
        public SpriteEffects spriteEffect;
        public int speed;
        private int counter;
        private int currentFrame;

        public BigMachine(Texture2D textureStand, Texture2D textureWalk1, Texture2D textureWalk2, Vector2 newPosition )
        {
            currentFrame = 1;
            speed = 1;
            counter = 18;
            texture = textureStand;
            stand = textureStand;
            walk1 = textureWalk1;
            walk2 = textureWalk2;
            spriteEffect = SpriteEffects.FlipHorizontally;
            position = newPosition;
            //
        }

        public void Update(GameTime gameTime)
        {
            if (position.X >= 10000)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (position.X <= -800)
            {
                spriteEffect = SpriteEffects.None;
            }

            if (spriteEffect == SpriteEffects.FlipHorizontally)
            {
                position.X -= speed;
            }
            else
                if (spriteEffect == SpriteEffects.None)
                {
                    position.X += speed;
                }

            MovementAnimation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

        private void MovementAnimation()
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                if (currentFrame == 1)
                    texture = walk1;
                else
                    if (currentFrame == 2)
                    {
                        texture = stand;
                        counter = 12;
                    }
                    else if (currentFrame == 3)
                        texture = walk2;
                currentFrame++;
            }

            if (currentFrame > 3)
                currentFrame = 1;

            if (counter <= 0)
                counter = 18;
        }
    }
}
