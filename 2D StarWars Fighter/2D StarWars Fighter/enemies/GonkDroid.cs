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
        //
        public List<Scorpion> scorpList;
        public List<SandExplosion> sandExplosionList;
        public Texture2D[] sandTextures = new Texture2D[8];
        //
        public bool isRight;
        public SpriteEffects spriteEffect;
        private int work_counter;

        public GonkDroid(Texture2D newCostume1, Texture2D newCostume2, Texture2D bullet, Vector2 newPosition, Player p, List<Hit> hitList, Texture2D[] newhitTexture, List<Scorpion> scorpionsList, List<SandExplosion> mainSandExplosionList, Texture2D[] MainSandTextures)
        {
            work_counter = 0;
            sandTextures = MainSandTextures;
            sandExplosionList = mainSandExplosionList;
            scorpList = scorpionsList;
            spriteEffect = SpriteEffects.None;
            isRight = false;
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

            if ((position.X - playerRef.position.X) <= 1200 && isRight == false)
            {
                if (playerRef.position.X < position.X)
                {
                    Shoot();
                }
            }
            if (isRight == true)
            {
                foreach (Scorpion scorp in scorpList)
                {

                    if ((scorp.position.X - position.X) <= 600 && scorp.position.X >= position.X)
                    {
                        if(work_counter == 0)
                            SoundManager.gonkdroid_work.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                        Shoot();
                        work_counter++;
                        if (work_counter >= 85)
                            work_counter = 0;
                    }
                }
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
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
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
                    if (isRight == true)
                    {
                        b.position.X += 40;
                    }
                    bulletList.Add(b);
                    SoundManager.gonkdroid_shoot.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
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
                if (isRight == false)
                {
                    b.position.X -= b.speed;
                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                    if (b.position.X <= 0)
                    {
                        b.isVisible = false;
                    }
                }
                else if (isRight == true)
                {
                    b.position.X += b.speed;
                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                    if (b.position.X >= 1900)
                    {
                        b.isVisible = false;
                    }
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
                if (isRight == true)
                {
                    if (playerRef.isDefending == false)
                    {
                        playerRef.health -= 25;
                        bullet.isVisible = false;
                    }
                    //
                    if (playerRef.isDefending == true)
                    {
                        if (playerRef.spriteEffect != SpriteEffects.FlipHorizontally)
                        {
                            playerRef.health -= 25;
                            bullet.isVisible = false;
                        }
                    }
                }
                //

                if (isRight == false)
                {
                    if (playerRef.isDefending == false)
                    {
                        playerRef.health -= 25;
                        bullet.isVisible = false;
                    }
                    //
                    if (playerRef.isDefending == true)
                    {
                        if (playerRef.spriteEffect != SpriteEffects.None)
                        {
                            playerRef.health -= 25;
                            bullet.isVisible = false;
                        }
                    }
                }

                Hit hit = new Hit(hitTextures, new Vector2( playerRef.position.X + 30,  playerRef.position.Y + 30));
                hit.isVisible = true;
                hitExplosions.Add(hit);
            }

            foreach (Scorpion s in scorpList)
            {
                if (bullet.boundingBox.Intersects(s.boundingBox))
                {
                    s.isVisible = false;
                    SandExplosion explosion = new SandExplosion(new Vector2(s.position.X, s.position.Y + 20), sandTextures) { isVisible = true };
                    sandExplosionList.Add(explosion);
                    bullet.isVisible = false;
                }
            }

        }

        private void Collision()
        {

            if (playerRef.boundingBox.Intersects(boundingBox) && playerRef.isAttacking)
            {
                IsVisible = false;
                HUD.playerScore += 50;
                SoundManager.gonkdroid_explode.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
            }

        }

    }
}
