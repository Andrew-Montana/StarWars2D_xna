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
        public Texture2D[] lighsaber = new Texture2D[6];
        //
        public bool isEntered;
        public bool isVisible;
        //
        public int stickout_counter, stickout_currentFrame;
        public bool isStickout, flag1;

        public Boss3Level(Texture2D[] costume, Texture2D[] force, Texture2D[] lighsaber)
        {
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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


    }
}
