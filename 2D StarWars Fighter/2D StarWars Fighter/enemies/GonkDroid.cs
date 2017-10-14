using System;
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
        // refs
        public Player playerRef;
        public List<Hit> hitExplosions;
        public Texture2D[] hitTextures;

        public GonkDroid(Texture2D newCostume1, Texture2D newCostume2, Texture2D bullet, Vector2 newPosition, Player p, List<Hit> hitList, Texture2D[] newhitTexture)
        {
            hitExplosions = hitList;
            hitTextures = newhitTexture;
            playerRef = p;
            costume1 = newCostume1;
            costume2 = newCostume2;
            texture = costume1;
            bulletTexture = bullet;
            position = newPosition;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //
            bulletDelay = 35;
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if ((position.X - playerRef.position.X) <= 2000)
            {
                Shoot();
            }
            ManageBullets();
            Collision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.White);
        }

        private void Shoot()
        {
            if (bulletDelay > 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                if (bulletList.Count <= 40)
                {
                    Bullet b = new Bullet(bulletTexture) { isVisible = true };
                    b.position = position;
                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
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
            foreach(Bullet b in bulletList)
            {
                b.position.X -= b.speed;
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                if (b.position.X <= 0)
                {
                    b.isVisible = false;
                }
                BulletCollision(b);
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

        private void BulletCollision(Bullet bullet)
        {
            if (playerRef.boundingBox.Intersects(bullet.boundingBox))
            {
                if (playerRef.isDefending != true)
                {
                    if (playerRef.spriteEffect == SpriteEffects.None)
                    {
                        playerRef.health -= 30;
                        bullet.isVisible = false;
                    }
                }
                else if(playerRef.isDefending == true)
                {
                    bullet.isVisible = false;
                }

                Hit hit = new Hit(hitTextures, new Vector2( playerRef.position.X + 30,  playerRef.position.Y + 30));
                hit.isVisible = true;
                hitExplosions.Add(hit);
            }

        }

        private void Collision()
        {

            if (playerRef.boundingBox.Intersects(boundingBox) && playerRef.isAttacking)
            {
                IsVisible = false;
                HUD.playerScore += 50;
            }

        }

    }
}
