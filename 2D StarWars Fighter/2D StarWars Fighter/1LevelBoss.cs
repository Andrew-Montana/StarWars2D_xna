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
   public class _1LevelBoss
    {
       // Score Bonus
       private bool isGivedScore;

       // Explosion for Death
       public Texture2D explosion_texture;
       public bool isExplosionVisible;
       public Rectangle explosionRectangle;
       public Vector2 explosionOrigin;
       public int currentFrame, spriteWidth, spriteHeight;
       public int frameCounter;

       // Main
        public Texture2D texture;
        public Vector2 position;
        public Texture2D hover, open1, open2, storage, zap1, zap2, zap3, zap4;
        public int bulletDamage, health, collisionDamage, moveCounter, speed;
        public Rectangle boundingBox, boundingRayBox;
        Player playerRefference;
        public Random rand;
        public bool isVisible, isFall, isAttacking, isUp, isDeath, isBoom;
        public bool endFlag;
        public Color color;

       // counters
        public int fallCounter, goingUpCounter, attacking_animation_counter;

       // sound
        private bool isFirePowerSound;

        public _1LevelBoss(Player playerRef)
        {
            isGivedScore = false;
            // explosion
            frameCounter = 13;
            spriteWidth = 134;
            spriteHeight = 134;
            explosion_texture = null;
            isExplosionVisible = false;
            currentFrame = 1;
            explosionRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            explosionOrigin = new Vector2(spriteWidth / 2, spriteHeight / 2);

            // main
            playerRefference = playerRef;
            texture = null;
            hover = null; open1 = null; open2 = null; storage = null; zap1 = null; zap2 = null; zap3 = null; zap4 = null;
            bulletDamage = 40;
            health = 2500;
            collisionDamage = 25;
            rand = new Random();
            moveCounter = 400;
            isVisible = false; isFall = true; isAttacking = false; isUp = false; endFlag = false;
            speed = 1;
            color = Color.White;

            // counters
            fallCounter = 90;
            goingUpCounter = 200;
            attacking_animation_counter = 30;

            // sound
            isFirePowerSound = true;
            isDeath = false;
            isBoom = false;
        }

        public void LoadContent(ContentManager Content)
        {
            //explosion
            explosion_texture = Content.Load<Texture2D>("explosion_sheet");
            // main
            hover = Content.Load<Texture2D>("boss1level/hover");
            open1 = Content.Load<Texture2D>("boss1level/open1");
            open2 = Content.Load<Texture2D>("boss1level/open2");
            storage = Content.Load<Texture2D>("boss1level/storage1");
            zap1 = Content.Load<Texture2D>("boss1level/zap1");
            zap2 = Content.Load<Texture2D>("boss1level/zap2");
            zap3 = Content.Load<Texture2D>("boss1level/zap3");
            zap4 = Content.Load<Texture2D>("boss1level/zap4");

            texture = hover;

            //
            position = new Vector2(10400, 740 - texture.Height);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            boundingRayBox = new Rectangle((int)position.X + texture.Width/2, (int)position.Y, texture.Width / 6, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            // Если игрок дошел до конца, то босс становится видимым т.е. создается, endFlag поднимается чтобы в дальнейшем результат условия не был равен true
            if (playerRefference.isEndPosition == true && endFlag == false)
            {
                isVisible = true;
                endFlag = true;
            }
            if (isVisible)
            {
                boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                boundingRayBox = new Rectangle((int)position.X + (texture.Width / 2) - 25, (int)position.Y + 100, texture.Width / 6, texture.Height);

                DeadAnimation();
                //  Collision();

                // Основной код
                if (isVisible)
                {
                    if (health > 525)
                    {
                        color = Color.White;
                    }
                    if (health < 500)
                    {
                        color = Color.Red;
                    }
                    Fall();
                    Up();
                    Attack();
                    Collision();

                    if (health <= 0 && isDeath == false)
                    {
                        Death();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                if (isFirePowerSound)
                {
                    MediaPlayer.Stop();
                    SoundManager.firepower.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    SoundManager.boss_start_level1.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    MediaPlayer.Play(SoundManager.bgMusic2_level1);
                    isFirePowerSound = false;
                }
                spriteBatch.Draw(texture, position, color);
            }

            if (isExplosionVisible)
            {
                spriteBatch.Draw(explosion_texture, new Vector2(position.X + texture.Width/2, position.Y + texture.Width / 3), explosionRectangle, Color.White, 0f, explosionOrigin, 1.0f, SpriteEffects.None, 0);
            }
            
           // spriteBatch.Draw(texture, boundingRayBox, Color.Red);
        }


        #region methods
        // # Methods
        private void Move()
        {
            if ( (playerRefference.position.X) > boundingRayBox.X)
                position.X += speed;
            if ((playerRefference.position.X) < boundingRayBox.X)
                position.X -= speed;
        }

        private void Attack()
        {
            if (isAttacking)
            {
                AttackAnimation();
                Move();
                if (SoundManager.effectsVolume != 0.0f)
                {
                    SoundManager.boss1_attack.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
                }
            }
        }

        private void Fall()
        {
            #region isFall = true
            if (health == 2000)
            {
                health = health - 1;
                isBoom = true;
                //
                isFall = true;
                isUp = false;
                isAttacking = false;
                speed = 2;
            }
            else if (health == 1500)
            {
                health = health - 1;
                isBoom = true;
                //
                isFall = true;
                isUp = false;
                isAttacking = false;
                speed = 3;
            }
            else
                if (health == 1000)
                {
                    health = health - 1;
                    isBoom = true;
                    //
                    isFall = true;
                    isUp = false;
                    isAttacking = false;
                    speed = 4;
                }
                else
                    if (health == 500)
                    {
                        health = health - 1;
                        isBoom = true;
                        //
                        isFall = true;
                        isUp = false;
                        isAttacking = false;
                       // speed = 5;
                    }

            #endregion

            if (isFall)
            {
                FallAnimation();
                fallCounter--;

                if (fallCounter <= 0)
                {
                    fallCounter = 500;
                    isFall = false;
                    isUp = true;
                }

            }

            if (isBoom)
            {
                SoundManager.boss1_boom.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                isBoom = false;
            }
        }

        private void Up()
        {
            if (isUp)
            {
                UpAnimation();
            }
        }


        private void Collision()
        {
            // Ray
            if(boundingRayBox.Intersects(playerRefference.boundingBox) && isAttacking == true)
                playerRefference.health -= 1;
            // Player attacks the Boss
            if (playerRefference.isAttacking && playerRefference.boundingBox.Intersects(boundingBox))
            {
                health -= 1;
                HurtAnimation();
            }
            
        }

        // # Animations

        private void DeadAnimation()
        {
            // EXPLOSION
            if (isExplosionVisible)
            {
                if (frameCounter > 0)
                    frameCounter--;


                if (frameCounter <= 0)
                {
                    frameCounter = 13;
                    currentFrame++;
                }

                if (currentFrame > 12)
                {
                    currentFrame = 12;
                    isExplosionVisible = false;
                }

                    explosionRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                    explosionOrigin = new Vector2(spriteWidth/2, spriteHeight/2);

            }

            // Boss is going down
            if (isDeath)
            {
                position.Y += 1;
                isFall = false;
                isUp = false;
                isAttacking = false;
                if (position.Y > 800)
                {
                    isVisible = false;
                    if (isGivedScore == false)
                    {
                        HUD.playerScore += 650;
                        isGivedScore = true;
                    }
                    // Переход на 2 уровень
                    Game1.menuCommand = "Level1EndScene";

                }
            }
        }

        private void HurtAnimation()
        {
            color = Color.Orange;
        }

        private void UpAnimation()
        {
            goingUpCounter--;
            position.Y -= 1;


            // textures
            if (goingUpCounter >= 60 && goingUpCounter <= 80)
                texture = open1;
            if (goingUpCounter >= 39 && goingUpCounter <= 59)
                texture = open2;

            // Finish animation
            if (goingUpCounter <= 0)
            {
                goingUpCounter = 200;
                isUp = false;
                isAttacking = true;
                texture = zap1;
            }
        }

        private void FallAnimation()
        {
            texture = hover;
            if (fallCounter >= 300 && fallCounter <= 500)
            {
                position.Y += 1;
            }
        }

        private void AttackAnimation()
        {
            attacking_animation_counter--;

            // textures
            if (attacking_animation_counter >= 20 && attacking_animation_counter <= 30)
            {
                texture = zap2;
            }
            else if(attacking_animation_counter >= 10 && attacking_animation_counter <= 19)
            {
                texture = zap3;
            }
            else if (attacking_animation_counter >= 0 && attacking_animation_counter <= 9)
            {
                texture = zap4;
            }
            // restore
            if (attacking_animation_counter <= 0)
            {
                attacking_animation_counter = 30;
            }
        }

        private void Death()
        {
            SoundManager.boss1_death.Play();
            isDeath = true;
            isExplosionVisible = true;
         //   DeadAnimation();
        }

       // after player death, resets boss parameters
        public void SetDefault()
        {
            isGivedScore = false;
            // explosion
            frameCounter = 13;
            spriteWidth = 134;
            spriteHeight = 134;
       //     explosion_texture = null;
            isExplosionVisible = false;
            currentFrame = 1;
            explosionRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            explosionOrigin = new Vector2(spriteWidth / 2, spriteHeight / 2);

            // main
           // playerRefference = playerRef;
        //    texture = null;
        //    hover = null; open1 = null; open2 = null; storage = null; zap1 = null; zap2 = null; zap3 = null; zap4 = null;
            bulletDamage = 40;
            health = 2500;
            collisionDamage = 25;
            rand = new Random();
            moveCounter = 400;
            isVisible = false; isFall = true; isAttacking = false; isUp = false; endFlag = false;
            speed = 1;
            color = Color.White;

            // counters
            fallCounter = 90;
            goingUpCounter = 200;
            attacking_animation_counter = 30;

            // sound
            isFirePowerSound = true;
            isDeath = false;
            isBoom = false;

            // POSITION

            position = new Vector2(10400, 740 - 168);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            boundingRayBox = new Rectangle((int)position.X + texture.Width / 2, (int)position.Y, texture.Width / 6, texture.Height);


        }


        #endregion

    }
}
