﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using _2D_StarWars_Fighter.enemies;

namespace _2D_StarWars_Fighter.enemies
{
    class GonkDroid
    {
        public Texture2D texture, costume1, costume2, bulletTexture; // 1 - stay, 2 - shoot.     stay - shoot - stay - shoot ...
        public Vector2 position;
        public bool IsVisible;
        public Rectangle boundingBox;
        public List<Bullet> bulletList = new List<Bullet>();
        public int bulletDelay;

        public GonkDroid(Texture2D newCostume1, Texture2D newCostume2, Texture2D bullet, Vector2 newPosition)
        {
            costume1 = newCostume1;
            costume2 = newCostume2;
            texture = costume1;
            bulletTexture = bullet;
            position = newPosition;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //
            bulletDelay = 35;
        }

        public void Update(ContentManager Content)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            Shoot();
            ManageBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        private void Shoot()
        {
            if (bulletDelay > 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                if (bulletList.Count <= 20)
                {
                    Bullet b = new Bullet(bulletTexture) { isLeft = true, isVisible = true };
                    b.position = position;
                    bulletList.Add(b);
                    texture = costume2;
                }
            }

            // reset
            if (bulletDelay <= 0)
            {
                bulletDelay = 35;
                texture = costume1;
            }
        }

        private void ManageBullets()
        {

        }

    }
}
