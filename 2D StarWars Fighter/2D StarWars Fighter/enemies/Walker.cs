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
   public class Walker
    {
        public Texture2D texture;
        public Vector2 position;
        public Texture2D[] walkTextures = new Texture2D[7];
        public Texture2D stand;
        public Rectangle boundingBox;

        public bool isVisible;
        private Player playerRef;
        public SpriteEffects spriteEffect;
        public int speed;

        public bool isAttackingAnimation;
        public bool isMoving;

        public int movementCounter;
        public int movementFrame;

       // sound 
        public int walk_counter;


        public Walker(Texture2D[] textures, Texture2D standTexture, Vector2 newPosition, Player player )
        {
            walk_counter = 0;
            movementFrame = 0;
            movementCounter = 7;
            isAttackingAnimation = false;
            isMoving = true;
            spriteEffect = SpriteEffects.None;
            walkTextures = textures;
            stand = standTexture;
            position = newPosition;
            texture = textures[0];
            playerRef = player;
            speed = 4;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            CheckDistanse();
            MovementAnimation();
            AttackAnimation();
            Movement();
            Rotate();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

        private void Movement()
        {
            if (isMoving == true)
            {
                if (playerRef.position.X > position.X)
                {
                    if ((playerRef.position.X - position.X) < 900)
                    {
                        if (walk_counter == 0)
                        {
                            SoundManager.walker_walk.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                            walk_counter++;
                        }
                    }
                }

                if (playerRef.position.X < position.X)
                {
                    if ((position.X - playerRef.position.X) < 1200)
                    {
                        if (walk_counter == 0)
                        {
                            SoundManager.walker_walk.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                            walk_counter++;
                        }
                    }
                }


                // If player stands righter of scorpion, scorpion going right
                if (playerRef.position.X > position.X)
                {
                    // SpriteEffect
                    spriteEffect = SpriteEffects.None;
                    // movement
                    position.X = position.X + speed;
                }
                // If player stands lefter of scorpion, scorpion is going left
                if (playerRef.position.X < position.X)
                {
                    // SpriteEffect
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    // movement
                    position.X = position.X - speed;
                }

                if(walk_counter > 0)
                walk_counter++;

                if (walk_counter >= 110)
                    walk_counter = 0;

            }
        }

        private void CheckDistanse()
        {
            // Distanse between player and scorpion ( for attack ). When player stands right
            if (playerRef.position.X > position.X)
            {
                if ((playerRef.position.X - position.X) <= 450)
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
                if ((position.X - playerRef.position.X) <= 450)
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

                if (movementFrame >= 7)
                    movementFrame = 0;

                // reset counter
                if (movementCounter <= 0)
                    movementCounter = 7;

            }
        }

        private void AttackAnimation()
        {
            if (isAttackingAnimation == true)
            {
                texture = stand;
            }
        }

        private void Rotate()
        {
            if (playerRef.position.X > position.X)
            {
                spriteEffect = SpriteEffects.None;
            }
            if (playerRef.position.X < position.X)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
        }

    }
}
