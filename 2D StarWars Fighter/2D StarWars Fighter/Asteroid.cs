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
    class Asteroid
    {
        public Texture2D texture;
        public int speed;
        public float rotationAngle;
        public Vector2 position, origin;
        public bool isVisible;
        public Rectangle boundingBox;
        private Random rand;

        public Asteroid(Texture2D newTexture)
        {
            rand = new Random();
            position = new Vector2(1400, rand.Next(46, 654));
            speed = 4;
            texture = newTexture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }


        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Movement();
            Rotation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
        }

        private void Movement()
        {
            position.X -= speed;
            if (position.X <= -100)
                position.X = 1400;
        }

        private void Rotation(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }


    }
}
