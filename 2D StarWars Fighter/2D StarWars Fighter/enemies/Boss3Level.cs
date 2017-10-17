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
        public bool isThrow;
        public int throwCounter;
        public int throw_currentFrame;
        public Texture2D throwTexture;
        public List<Lightsaber> lightsaberList = new List<Lightsaber>();

        public Boss3Level(Texture2D[] costume, Texture2D[] force, Texture2D[] lighsaber, Texture2D throwTexture)
        {
            this.throwTexture = throwTexture;
            throw_currentFrame = 0;
            throwCounter = 13;
            isThrow = true;
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
            EnterArea();
            // Start Fight
            if (flag1 == true)
            {
                Throw();
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
            if (isThrow == true)
            {
                if (throwCounter > 0)
                    throwCounter--;

                //# if standing right
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
                    isThrow = false;
                    Lightsaber l = new Lightsaber(throwTexture, new Vector2(14440, 655), new Vector2(13500, 655));
                    l.isVisible = true;
                    lightsaberList.Add(l);
                }

                //#

                if (throwCounter <= 0)
                    throwCounter = 13;
            }
        }

        // Force

        private void Push()
        {

        }

        private void Teleport()
        {

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
                        position.X -= 23;
                    }
                }
            }
        }


    }
}
