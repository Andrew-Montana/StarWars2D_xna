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
    class Lightsaber
    {
        public Texture2D texture;
        public Vector2 startPos, endPos, position;
        public bool isVisible;
        public Rectangle boundingBox;
        public int speed;
        private bool flag;

        public Lightsaber(Texture2D texture, Vector2 startPosition, Vector2 endPosition)
        {
            flag = false;
            speed = 7;
            this.texture = texture;
            startPos = startPosition;
            position = startPos;
            endPos = endPosition;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {

            // direction. Go Left
            if (startPos.X > endPos.X)
            {
                // Если флаг не поднят, идем налево
                if(flag == false)
                    position.X -= speed;

                // если position.X достигла нужной точки, поднимаем флаг
                if (position.X <= endPos.X)
                    flag = true;

                // если флаг поднят, движемся в первоначальную точку
                if (flag == true)
                    position.X += speed;

                if (flag == true && position.X >= (startPos.X + 40))
                    isVisible = false;

            }
            if (startPos.X < endPos.X)
            {
                if(flag == false)
                    position.X += speed;

                if (position.X >= endPos.X)
                    flag = true;

                if (flag == true)
                    position.X -= speed;

                if (flag == true && position.X <= startPos.X)
                    isVisible = false;
            }

            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
