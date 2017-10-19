using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace _2D_StarWars_Fighter.enemies
{
    class Scorpion
    {
        public Texture2D texture;   // main texture
        public Vector2 position;    // object position
        public Texture2D[] walkTextures = new Texture2D[3];
        public Texture2D[] attackTextures = new Texture2D[9];
        public Rectangle boundingBox;
        public bool isVisible;
        private Player playerRef;
        public SpriteEffects spriteEffect;
        public int speed;

        public bool isAttackingAnimation;
        public bool isAttacks;
        public bool isMoving;
        public int animationCounter;
        public int currentFrame;

        public int movementCounter;
        public int movementFrame;

        public Scorpion(Vector2 newPosition, Player p, Texture2D[] walkArray, Texture2D[] attackArray)
        {
            movementFrame = 0;
            movementCounter = 20;
            currentFrame = 0;
            animationCounter = 10;
            speed = 4;
            spriteEffect = SpriteEffects.None;
            position = newPosition;
            isAttackingAnimation = false;
            isAttacks = false;
            isMoving = true;
            playerRef = p;
            //
            walkTextures = walkArray;
            attackTextures = attackArray;

            texture = walkTextures[2];
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

     /*   public void LoadContent(ContentManager Content)
        {
            for (int i = 0; i < walkTextures.Length; i++)
            {
                walkTextures[i] = Content.Load<Texture2D>("level3/scorpion/scorpion_walk" + (i+1).ToString());
            }
            //
            for (int i = 0; i < attackTextures.Length; i++)
            {
                attackTextures[i] = Content.Load<Texture2D>("level3/scorpion/scorpion" + (i+1).ToString());
            }

            texture = walkTextures[2];
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        } */

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            CheckDistanse();
            MovementAnimation();
            AttackAnimation();
            Movement();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

        // Methods

        private void Movement()
        {
            if (isMoving == true)
            {
                // If player stands righter of scorpion, scorpion going right
                if (playerRef.position.X > position.X)
                {
                    // SpriteEffect
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    // movement
                    position.X = position.X + speed;                  
                }
                // If player stands lefter of scorpion, scorpion is going left
                if (playerRef.position.X < position.X)
                {
                    // SpriteEffect
                    spriteEffect = SpriteEffects.None;
                    // movement
                    position.X = position.X - speed;
                }

            }
        }

        private void CheckDistanse()
        {
            // Distanse between player and scorpion ( for attack ). When player stands right
            if (playerRef.position.X > position.X)
            {
                if ((playerRef.position.X - position.X) <= 280)
                {
                    isAttackingAnimation = true;
                    isMoving = false;
                }
                else
                {
                    isAttackingAnimation = false;
                    isMoving = true;
                }
            }

            if (playerRef.position.X < position.X)
            {
                // Distanse between player and scorpion ( for attack ). When player stands left
                if ((position.X - playerRef.position.X) <= 280)
                {
                    isAttackingAnimation = true;
                    isMoving = false;
                }
                else
                {
                    isAttackingAnimation = false;
                    isMoving = true;
                }
            }
        }

        private void MovementAnimation()
        {
            if (isMoving)
            {
                if (movementCounter > 0)
                    movementCounter--;

                if (movementCounter <= 0)
                {
                    texture = walkTextures[movementFrame];
                    movementFrame++;
                }

                if (movementFrame >= 3)
                    movementFrame = 0;

                // reset counter
                if (movementCounter <= 0)
                    movementCounter = 20;

            }
        }

        private void AttackAnimation()
        {
            if (isAttackingAnimation == true)
            {
                if (animationCounter > 0)
                    animationCounter--;

                if (currentFrame >= 6 && currentFrame <= 8)
                {
                    isAttacks = true;
                    if (spriteEffect == SpriteEffects.None)
                        position.X = position.X - 10; 
                    else
                    position.X = position.X + 10;
                }
                else
                {
                    isAttacks = false;
                }

                if (animationCounter <= 0)
                {
                    texture = attackTextures[currentFrame];
                    currentFrame++;
                }

                if(currentFrame == 8)
                    SoundManager.scorp_attack.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);

                if (currentFrame >= 9)
                {
                    currentFrame = 0;
                }

                // reset counter
                if (animationCounter <= 0)
                    animationCounter = 10;

            }
        }

    }
}
