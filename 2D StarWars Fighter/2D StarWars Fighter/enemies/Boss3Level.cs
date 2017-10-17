﻿using System;
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
    class Boss3Level
    {
        public Texture2D texture;
        public Vector2 position;
        public SpriteEffects spriteEffect;
        public Rectangle boundingBox;
        public int health;
        //
        public Texture2D[] costume = new Texture2D[6];
        public Texture2D[] force = new Texture2D[8];
        public Texture2D[] lighsaber = new Texture2D[4];
        //
        public bool isEntered;
        public bool isVisible;
        //
        public int stickout_counter, stickout_currentFrame;
        public bool isStickout, flag1;
        //
     //   public bool isThrow;
        public int throwCounter;
        public int throw_currentFrame;
        public Texture2D throwTexture;
        public List<Lightsaber> lightsaberList = new List<Lightsaber>();
        public bool isAttack;
        //
        public int step;
        //
        public Player player_ref;
        // 
        public int push_counter;
        public int push_frame;

        public Boss3Level(Texture2D[] costume, Texture2D[] force, Texture2D[] lighsaber, Texture2D throwTexture, Player player)
        {
            push_frame = 0;
            push_counter = 30;
            player_ref = player;
            step = 0;
            isAttack = false;
            this.throwTexture = throwTexture;
            throw_currentFrame = 0;
            throwCounter = 13;
        //    isThrow = true;
            flag1 = false;
            stickout_currentFrame = 0;
            isStickout = true;
            stickout_counter = 8;
            health = 1000;
            spriteEffect = SpriteEffects.None; // Custom. Is looking left
            position = new Vector2(14920, 620);
            isEntered = false;
            this.costume = costume;
            this.force = force;
            this.lighsaber = lighsaber;
            texture = costume[0];
        }

        public void Update(GameTime gameTime)
        {
            switch (step)
            {
                case 0:
                    {
                        EnterArea();
                        // Start Fight
                        if (flag1 == true)
                        {
                            if (lightsaberList.Count == 0)
                            Throw();
                        }
                        break;
                    }

                case 1:
                    {
                        if(lightsaberList.Count == 0)
                        Push();
                        break;
                    }

                case 2:
                    {
                        if (lightsaberList.Count == 0)
                        Throw();
                        break;
                    }

                case 3:
                    {
                        Push();
                        break;
                    }

                case 4:
                    {
                        Throw(); // ok
                        break;
                    }
                case 5:
                    {
                        if(lightsaberList.Count == 0)
                        Throw(10);
                        break;
                    }

                case 6:
                    {
                        if (lightsaberList.Count == 0)
                            Throw(10);
                        break;
                    }
                case 7:
                    {
                        Push();
                        break;
                    }
                case 8:
                    {
                        Teleport();
                        step++;
                        break;
                    }
                case 9:
                    {
                        if (lightsaberList.Count == 0)
                        Throw(12);
                        break;
                    }
                case 10:
                    {
                        if (lightsaberList.Count == 0)
                        Throw(14);
                        break;
                    }
                case 11:
                    {
                        if (lightsaberList.Count == 0)
                        Teleport();
                        step++;
                        break;
                    }
                case 12:
                    {
                        if (lightsaberList.Count == 0)
                        Throw(13);
                        break;
                    }
                case 13:
                    {
                        Push();
                        break;
                    }
                case 14:
                    {
                        if (lightsaberList.Count == 0)
                        Throw(16);
                        break;
                    }
                case 15:
                    {
                        Teleport();
                        step = 0;
                        break;
                    }


            }

            //
            foreach (Lightsaber l in lightsaberList)
            {
                l.Update(gameTime);
            }
            ManageLightsaber();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Lightsaber l in lightsaberList)
            {
                l.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);

        }

        // Lightsaber

        private void StickOut()
        {
            if (isStickout)
            {
                if (stickout_counter > 0)
                    stickout_counter--;

                if (stickout_counter <= 0)
                {
                    texture = costume[stickout_currentFrame];
                    if (stickout_currentFrame == 1)
                        position.X -= 40;
                    if (stickout_currentFrame == 3)
                        position.X -= 7;
                    if (stickout_currentFrame == 4)
                        position.X -= 12;
                    if (stickout_currentFrame == 5)
                        position.X += 12;
                    stickout_currentFrame++;
                }

                if (stickout_currentFrame >= 6)
                {
                    stickout_currentFrame = 0;
                    isStickout = false;
                    flag1 = true;
                }

                if (stickout_counter <= 0)
                    stickout_counter = 8;
            }
        }

        private void Hide()
        {

        }

        private void Throw()
        {
           // if (isThrow == true)
          //  {
                isAttack = true;

                if (throwCounter > 0)
                    throwCounter--;

                //# if standing right
                if (spriteEffect == SpriteEffects.None)
                {
                    if (throwCounter <= 0)
                    {
                        texture = lighsaber[throw_currentFrame];
                        if (throw_currentFrame == 0)
                            position.X -= 22;
                        else if (throw_currentFrame == 1)
                            position.X -= 40;
                        else if (throw_currentFrame == 2)
                            position.X -= 29;
                        else if (throw_currentFrame == 3)
                            position.X -= 1;
                        //    else if (throw_currentFrame == 4)
                        //    position.X += 1;
                        throw_currentFrame++;
                    }

                    if (throw_currentFrame >= 4)
                    {
                        throw_currentFrame = 0;
                        texture = force[0];
                        position.X += 115;
                        //    isThrow = false;
                        Lightsaber l = new Lightsaber(throwTexture, new Vector2(14440, 655), new Vector2(13500, 655));
                        l.isVisible = true;
                        lightsaberList.Add(l);
                        isAttack = false;
                        step++;
                    }
                } // #
                else if (spriteEffect == SpriteEffects.FlipHorizontally)
                {
                    if (throwCounter <= 0)
                    {
                        texture = lighsaber[throw_currentFrame];
                        throw_currentFrame++;
                    }

                    if (throw_currentFrame >= 4)
                    {
                        throw_currentFrame = 0;
                        texture = force[0];
                        Lightsaber l = new Lightsaber(throwTexture, new Vector2(13500, 655), new Vector2(14500, 655)); // ????
                        l.isVisible = true;
                        lightsaberList.Add(l);
                        isAttack = false;
                        step++;
                    }
                }
                

                if (throwCounter <= 0)
                    throwCounter = 13;
           // }
        }

        private void Throw(int flyspeed)
        { // speed must be > 8
            // if (isThrow == true)
            //  {
            isAttack = true;

            if (throwCounter > 0)
                throwCounter--;

            //# if standing right
            if (spriteEffect == SpriteEffects.None)
            {
                if (throwCounter <= 0)
                {
                    texture = lighsaber[throw_currentFrame];
                    if (throw_currentFrame == 0)
                        position.X -= 22;
                    else if (throw_currentFrame == 1)
                        position.X -= 40;
                    else if (throw_currentFrame == 2)
                        position.X -= 29;
                    else if (throw_currentFrame == 3)
                        position.X -= 1;
                    //    else if (throw_currentFrame == 4)
                    //    position.X += 1;
                    throw_currentFrame++;
                }

                if (throw_currentFrame >= 4)
                {
                    throw_currentFrame = 0;
                    texture = force[0];
                    position.X += 115;
                    //    isThrow = false;
                    Lightsaber l = new Lightsaber(throwTexture, new Vector2(14440, 655), new Vector2(13500, 655));
                    l.isVisible = true;
                    l.speed = flyspeed;
                    lightsaberList.Add(l);
                    isAttack = false;
                    step++;
                }
            } // #
            else if (spriteEffect == SpriteEffects.FlipHorizontally)
            {
                if (throwCounter <= 0)
                {
                    texture = lighsaber[throw_currentFrame];
                    throw_currentFrame++;
                }

                if (throw_currentFrame >= 4)
                {
                    throw_currentFrame = 0;
                    texture = force[0];
                    Lightsaber l = new Lightsaber(throwTexture, new Vector2(13500, 655), new Vector2(14500, 655)); // ????
                    l.isVisible = true;
                    l.speed = flyspeed;
                    lightsaberList.Add(l);
                    isAttack = false;
                    step++;
                }
            }

            //#

            if (throwCounter <= 0)
                throwCounter = 13;
            // }
        }

        // Force

        private void Push()
        {
            if (push_counter > 0)
                push_counter--;

            if (spriteEffect == SpriteEffects.None)
            {
                if (push_counter <= 0)
                {
                    texture = force[push_frame];
                    //
                    if (push_frame == 1)
                        position.X -= 17;
                    if (push_frame == 3)
                        position.X += 5;
                    if (push_frame == 4)
                        position.X -= 15;
                    if (push_frame == 5)
                        position.X -= 32;
                    if (push_frame == 6)
                        position.X += 43;
                    if (push_frame == 7)
                        position.X += 17;
                    //
                    push_frame++;
                    if (position.X >= 14000)
                        player_ref.position.X -= 38 * push_frame;
                    if (position.X <= 13900)
                        player_ref.position.X += 38 * push_frame;
                }
            }
            else if (spriteEffect == SpriteEffects.FlipHorizontally)
            {
                if (push_counter <= 0)
                {
                    texture = force[push_frame];
                    push_frame++;
                    if (position.X >= 14000)
                        player_ref.position.X -= 38 * push_frame;
                    if (position.X <= 13900)
                        player_ref.position.X += 38 * push_frame;
                }
            }

            if (push_frame >= 8)
            {
                push_frame = 0;
                step++;
            }

            if (push_counter <= 0)
                push_counter = 30;
        }

        private void Teleport()
        {
            if (spriteEffect == SpriteEffects.None)
            {
                position.X = 13400;
                spriteEffect = SpriteEffects.FlipHorizontally;
             //   step = 0;
            }
            else if (spriteEffect == SpriteEffects.FlipHorizontally)
            {
                position = new Vector2(14580, 620);
                spriteEffect = SpriteEffects.None;
             //   step = 0;
            }
        }
        
        // Intro

        private void EnterArea()
        {
            if (isEntered == false)
            {
                position.X--;
                if (position.X <= 14580)
                {
                    isEntered = true;
                }
            }
            else if (isEntered && flag1 == false)
            {
                    StickOut();
            }
        }

        // 
        private void ManageLightsaber()
        {
            if (lightsaberList.Count != 0)
            {
                for (int i = 0; i < lightsaberList.Count; i++)
                {
                    if (lightsaberList[i].isVisible == false)
                    {
                        lightsaberList.RemoveAt(i);
                        i--;
                        texture = costume[5];
                        if(spriteEffect == SpriteEffects.None)
                        position.X -= 23;
                    }
                }
            }
        }


    }
}
