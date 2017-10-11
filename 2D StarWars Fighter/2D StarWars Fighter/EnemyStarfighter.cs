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
    class EnemyStarfighter
    {
        public Texture2D enemyTexture, bulletTexture;
        public Vector2 position;
        public Rectangle boundingBox;
        public int speed, bulletDelay;
        public bool isVisible;
        public Random rand;
        public List<Bullet> bulletList = new List<Bullet>();

        public EnemyStarfighter(Texture2D newEnemyTexture, Texture2D newBulletTexture)
        {
            rand = new Random();
            enemyTexture = newEnemyTexture;
            bulletTexture = newBulletTexture;
            position = new Vector2(1400, rand.Next(0, 695));
            speed = 3;
            bulletDelay = 0;
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, enemyTexture.Width, enemyTexture.Height);
            Movement();
            Shoot();
            ManageBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(enemyTexture, position, Color.White);
        }

        // methods

        private void Movement()
        {
            position.X -= speed;
            if (position.X <= -100)
                position.X = 1400;
        }

        private void Shoot()
        {
            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2((int)position.X + (enemyTexture.Width / 2) + 10, (int)position.Y + (enemyTexture.Height / 2) - 3);
                newBullet.isVisible = true;
                newBullet.speed = 8;

                if (bulletList.Count < 20)
                {
                    bulletList.Add(newBullet);
                    SoundManager.enemyShoot.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                }
            }

            if (bulletDelay <= 0)
                bulletDelay = 60;
        }

        private void ManageBullets()
        {
            foreach (Bullet bullet in bulletList)
            {
                bullet.position.X -= bullet.speed;
                bullet.boundingBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bullet.texture.Width, bullet.texture.Height);

                if (bullet.position.X <= -20)
                    bullet.isVisible = false;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].isVisible == false)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
