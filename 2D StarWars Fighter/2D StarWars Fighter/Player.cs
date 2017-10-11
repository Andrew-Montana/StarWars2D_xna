using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace _2D_StarWars_Fighter
{
    public class Player
    {
        #region variables

        public int healthEndX { get; set; }
        public int EndX { get; set; }
        public int EndX2 { get; set; }
        public int screenBoundsEnd { get; set; }
        // Bonus
        public bool isHealthBonusUsed;

        // Getting Keyboard State
        KeyboardState keyState;
        // #
        public Texture2D texture, healthTexture;   // main character texture and health texture
        public Vector2 position, healthPosition;
        public int speed, health; // health and speed
        public int force;   // force of jumping
        public bool isJump;
        public int cooldown;


       // #Collision Variables
        public Rectangle boundingBox, healthRectangle;
        public bool isColliding;

       // #Walking
        public Texture2D texture_Walk1;     // stores image Walk1
        public Texture2D texture_Walk2;     // stores image Walk2
        int moveTime;       // Timer for walking animation
        
       // #Rotation
        public SpriteEffects spriteEffect;

       // #Defend
        public bool isDefending;
        public Texture2D textureDefend;

       // #Attack
        public bool isAttacking;
        public Texture2D textureAttack1;
        public Texture2D textureAttack2;
        public Texture2D textureAttack3;
        public Texture2D textureAttack4;
        public Texture2D textureAttack5;

        // Sound for attack
        public SoundEffect sound_attack1;
        public SoundEffect sound_attack2;
        public SoundEffect sound_attack3;
        public SoundEffect sound_attack4;
        public SoundEffect sound_attack5;

        public bool isSound1;
        public bool isSound2;
        public bool isSound3;

        // force
        public int attackForce;

        // KeyBoard temp statement for pressing keys
        KeyboardState tempKeyState;

        // Temp texture for sprite attack
        public Texture2D tempTextureAttack;

        // if player reach the end
        public bool isEndPosition;


        #endregion

        // Constructor

        public Player()
        {
            healthEndX = 9400;
            screenBoundsEnd = 10640;
            EndX = 9764;
            EndX2 = 9364;
            isHealthBonusUsed = false;
            cooldown = 0;
            isJump = false;
            force = 0;
            spriteEffect = SpriteEffects.None;
            texture = null;
            texture_Walk1 = null;
            texture_Walk2 = null;
            position = new Vector2(300, 720); // 300, 720|| 10100, 720
            speed = 6;
            isColliding = false;
            moveTime = 0;
            isDefending = false;
            textureDefend = null;
            isAttacking = false;
            textureAttack1 = null;
            textureAttack2 = null;
            textureAttack3 = null;
            textureAttack4 = null;
            textureAttack5 = null;
            sound_attack1 = null;
            sound_attack2 = null;
            sound_attack3 = null;
            sound_attack4 = null;
            sound_attack5 = null;
            isSound1 = false;
            isSound2 = false;
            isSound3 = false;
            attackForce = 0;
            keyState = Keyboard.GetState();
            tempKeyState = keyState;
            health = 200;
            healthTexture = null;
            healthPosition = new Vector2(position.X, 50);
            isEndPosition = false;
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {
            // Healthbar
            healthTexture = Content.Load<Texture2D>("health");

            // textures
            texture = Content.Load<Texture2D>("ben/walk1"); 
            texture_Walk1 = Content.Load<Texture2D>("ben/walk1");
            texture_Walk2 = Content.Load<Texture2D>("ben/walk2"); 

            textureDefend = Content.Load<Texture2D>("ben/defend");

            textureAttack1 = Content.Load<Texture2D>("ben/attack1");
            textureAttack2 = Content.Load<Texture2D>("ben/attack2");
            textureAttack3 = Content.Load<Texture2D>("ben/attack3");
            textureAttack4 = Content.Load<Texture2D>("ben/attack4");
            textureAttack5 = Content.Load<Texture2D>("ben/attack5");

            tempTextureAttack = textureAttack5;

            // sound
            sound_attack1 = Content.Load<SoundEffect>("sound/player_attack1");
            sound_attack2 = Content.Load<SoundEffect>("sound/player_attack2");
            sound_attack3 = Content.Load<SoundEffect>("sound/player_attack3");
            sound_attack4 = Content.Load<SoundEffect>("sound/player_attack4");
            sound_attack5 = Content.Load<SoundEffect>("sound/player_attack5");
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // BoudingBox for our Players
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            // Jedi Speed Ability


            // Update Health position
            if (position.X <= 400)
                healthPosition = new Vector2(45, 50);
            if (position.X >= 401 && !isEndPosition)
                healthPosition = new Vector2(position.X - 350, 50);
            if (isEndPosition)
                healthPosition = new Vector2(healthEndX, 50);
            // Set Rectangle for Healthbar
            healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, 25);

            #region rotation

            if(Keyboard.GetState().IsKeyDown(Keys.A)) spriteEffect = SpriteEffects.FlipHorizontally;
            if(Keyboard.GetState().IsKeyDown(Keys.D)) spriteEffect = SpriteEffects.None;

            #endregion

            #region Movement + Walking
            // Getting Keyboard State
             keyState = Keyboard.GetState();
            // Player Controls ( Movement ) + Animation

            if (keyState.IsKeyDown(Keys.A) && isDefending == false && isAttacking == false)     // Left
            {
                position.X = position.X - speed;

                if (position.X >= 401 && !isEndPosition) 
                {
                    // Set health position ( fixing bug )
                    healthPosition.X = healthPosition.X - 6;
                    healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, 25);
                    HUD.playerScorePos.X = HUD.playerScorePos.X - 6;
                }

                moveTime++; 
                if (moveTime >= 0 && moveTime <= 10) { texture = texture_Walk2; }
                else if (moveTime >= 11 && moveTime <= 20) { texture = texture_Walk1; }
                if (moveTime > 21) moveTime = 0;
            }
            if (keyState.IsKeyDown(Keys.D) && isDefending == false && isAttacking == false)     // Right
            {
                position.X = position.X + speed; // movement

                if (position.X >= 401 && !isEndPosition)
                {
                    // Set health position ( fixing bug )
                    healthPosition.X = healthPosition.X + 6;
                    healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, 25);
                    HUD.playerScorePos.X = HUD.playerScorePos.X + 6;
                }

                // Animation of Walking
                moveTime++; // timer for animation of walking
                if (moveTime >= 0 && moveTime <= 10) { texture = texture_Walk2; }
                else if (moveTime >= 11 && moveTime <=20) {   texture = texture_Walk1; }
                if (moveTime > 21) moveTime = 0;
            }

            #endregion

            // Custom player texture when is no pressed keys
            if (keyState.GetPressedKeys().Length == 0)
                texture = texture_Walk2;


            #region jumping
            if (cooldown >= 130)
            {
                if (keyState.IsKeyDown(Keys.W) && isJump == false)
                {
                    force = 20;
                    isJump = true;
                    cooldown = 0;
                }
            }

            if (isJump && force > 0)
            {
                position.Y -= force;    // force of jumping
                force -= 1;
            }

            // if force == 0 && isJust == true
            if (force == 0)
            {
                position.Y += 12;       // speed of failing    
            }

            if (position.Y >= 720 - texture.Height)
            {
                isJump = false;
                cooldown += 10;
            }


            #endregion

            #region Defend

            if (keyState.IsKeyDown(Keys.E))
            {
                texture = textureDefend;
                isDefending = true;
            }
            if (keyState.IsKeyUp(Keys.E))
            {
                isDefending = false;
            }


            #endregion

            #region Attack
            if (tempKeyState.IsKeyUp(Keys.Space))
            {
                if (keyState.IsKeyDown(Keys.Space) && isAttacking == false)
                {
                    isAttacking = true;
                    attackForce = 10;

                    // Different Sprites and Sounds
                    if (tempTextureAttack.Bounds == textureAttack5.Bounds)
                    {
                        texture = textureAttack1;
                        tempTextureAttack = texture;
                        Play_Attack1();
                    }
                    else
                        if (tempTextureAttack.GetHashCode() == textureAttack1.GetHashCode())
                        {
                            texture = textureAttack2;
                            tempTextureAttack = texture;
                            Play_Attack2();
                        }

                        else if (tempTextureAttack.GetHashCode() == textureAttack2.GetHashCode())
                        {
                            texture = textureAttack3;
                            tempTextureAttack = texture;
                            Play_Attack3();
                        }
                        else if (tempTextureAttack.GetHashCode() == textureAttack3.GetHashCode())
                        {
                            texture = textureAttack4;
                            tempTextureAttack = texture;
                            Play_Attack4();
                        }
                        else
                        {
                            texture = textureAttack5;
                            tempTextureAttack = texture;
                            Play_Attack5();
                        }

                }
            }

            if (isAttacking)
            {
                attackForce--;
            }

            if (attackForce <= 0 && isAttacking == true)
            {
                isAttacking = false;
                texture = texture_Walk1;
            }

            tempKeyState = keyState;
            #endregion

            // Keep Player In Screen Bounds
            if (position.X <= 0) position.X = 0;
            if (position.Y <= 0) position.Y = 0;
            if (position.X >= screenBoundsEnd - texture.Width) position.X = screenBoundsEnd - texture.Width;
            if (position.Y >= 720 - texture.Height) position.Y = 720 - texture.Height;

            // if player reached  the end
            if (position.X >= EndX)
            {
                isEndPosition = true;
                // giving more health
                if (!isHealthBonusUsed)
                {
                    health += 260;
                    isHealthBonusUsed = true;
                }
            }
            if (isEndPosition && position.X <= (EndX2-4)) position.X = EndX2;


            // #UPDATE END

        }

        // Sound Methods
        #region Sound

        public void Play_Attack1()
        {
            sound_attack1.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
        }

        public void Play_Attack2()
        {
            sound_attack2.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
        }

        public void Play_Attack3()
        {
            sound_attack3.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
        }

        public void Play_Attack4()
        {
            sound_attack4.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
        }

        public void Play_Attack5()
        {
            sound_attack5.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
        }

        #endregion




    }
}
