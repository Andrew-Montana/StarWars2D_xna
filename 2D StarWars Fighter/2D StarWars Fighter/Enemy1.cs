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
    public class Enemy1
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture, stand, run1, run2, run3, run4;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficultyLevel;
        public bool isVisible, isStandingLeft, isStandingRight;
        public List<Bullet> bulletList;
        public Player playerRef;
        public SpriteEffects spriteEffect;
        SoundManager sm = new SoundManager();

        // run
        public int animationCounter;

        // Constructor
        public Enemy1(Texture2D newTexture, Vector2 newPosition, Texture2D newbulletTexture, Player playerRefference, SpriteEffects newspriteEffect, Texture2D[] enemyRunArr, SoundManager soundManager)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newbulletTexture;
            position = newPosition;
            health = 5;
            currentDifficultyLevel = 1;
            bulletDelay = 30;
            isVisible = true;
            playerRef = playerRefference;
            spriteEffect = newspriteEffect;
            speed = 3;
            //
            run1 = enemyRunArr[0];
            run2 = enemyRunArr[1];
            run3 = enemyRunArr[2];
            run4 = enemyRunArr[3];
            stand = newTexture;
            isStandingLeft = false;
            isStandingRight = false;
            animationCounter = 80;
            sm = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            // Update Collision Rectangle
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            #region Movement and Rotation
            // Update Enemy Movement

            // If left enemy is too close to our player, then he must stop ( When Enemy in Left side)
            if (playerRef.position.X >= position.X && (playerRef.position.X - position.X) <= 150)
            {
                texture = stand;
                isStandingLeft = true;
            }

            // If enemy if too far from the player, then he will go ( When Enemy is left )
            if (playerRef.position.X >= position.X && (playerRef.position.X - position.X) >= 150)
            {
                isStandingLeft = false;
            }

            // If left enemy is too close to our player, then he must stop ( When Enemy in Right side)
            if (playerRef.position.X <= position.X && (playerRef.position.X - position.X) >= -150)
            {
                texture = stand;
                isStandingRight = true;
            }

            // If enemy if too far from the player, then he will go ( When Enemy is Right )
            if (playerRef.position.X <= position.X && (playerRef.position.X - position.X) <= -150)
            {
                isStandingRight = false;
            }

            // if player is righer than enemy, then enemy is going right
            if (playerRef.position.X >= position.X)
            {
                if (!isStandingLeft)
                {
                    position.X = position.X + speed;
                    EnemyRun();
                }
                spriteEffect = SpriteEffects.None;
            }
            // if player if lefter than enemy, then enemy is going left
            if (playerRef.position.X <= position.X)
            {
                if (!isStandingRight)
                {
                    position.X = position.X - speed;
                    EnemyRun();
                }
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            #endregion
            // if enemy is on the screen area, then he able to shoot
            if (position.X >= 50 || position.X <= 1230)
            {
                EnemyShoot();
            }
            UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Enemy Body
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
            // Draw Enemy Bullets
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public void UpdateBullets()
        {
            // For each bullet in our bulletList: update the movement and if the bullet hits the end of the screen remove it from the list
            foreach (Bullet b in bulletList)
            {
                // BoundingBox for our every bullet in our bulletList
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                // Set movement for bullet
                if(b.isRight)
                b.position.X = b.position.X + b.speed;
                if (b.isLeft)
                    b.position.X = b.position.X - b.speed;

                // if bullet hits the end of the screen, then make visible false
                if (b.position.X <= -20 || b.position.X >= 10750)
                    b.isVisible = false;
            }

            // Iterate througth bulletList and see if any of the bullets are not visible, if they arent then remove that bullet from the list

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

        }

        // Enemy Shoot Function
        public void EnemyShoot()
        {
            if ((playerRef.position.X - position.X) >= 0 && (playerRef.position.X - position.X) <= 400 || (position.X - playerRef.position.X) >= 0 && (position.X - playerRef.position.X) <= 800 )
            {



                // Shoot only if bulletDelay resets
                if (bulletDelay >= 0)
                    bulletDelay--;

                if (bulletDelay <= 0)
                {
                    sm.enemyShootSound.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    Bullet newBullet = new Bullet(bulletTexture);

                    newBullet.isVisible = true;

                    // Finding the side, where the bullet is gonna fly
                    if (spriteEffect == SpriteEffects.None)
                    {
                        newBullet.isRight = true;
                        // position
                        newBullet.position = new Vector2(position.X + 46, (position.Y + texture.Height / 2 - newBullet.texture.Height / 2) - 15);
                    }
                    if (spriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        newBullet.isLeft = true;
                        // position
                        newBullet.position = new Vector2(position.X, (position.Y + texture.Height / 2 - newBullet.texture.Height / 2) - 15);
                    }



                    // adding bullet in list if it is less than 20 objects
                    if (bulletList.Count() < 20)
                        bulletList.Add(newBullet);
                }

                // reset bullet delay
                if (bulletDelay == 0)
                {
                    bulletDelay = 90;
                }
            }

        }

        // Enemy Run Function
        public void EnemyRun()
        {
            // If counter is bigger than 0. He is going lower
            if (animationCounter >= 0)
                animationCounter--;

            // Changing of texture, depends on inverval
            if (animationCounter >= 61 && animationCounter <= 80)
                texture = run1;
            else
            if (animationCounter >= 41 && animationCounter <= 60)
                texture = run2;
            else
            if (animationCounter >= 21 && animationCounter <= 40)
                texture = run3;
            else
            if (animationCounter >= 0 && animationCounter <= 20)
                texture = run4;
            
            // Reset counter
            if (animationCounter <= 0)
                animationCounter = 80;
        }
    }
}
