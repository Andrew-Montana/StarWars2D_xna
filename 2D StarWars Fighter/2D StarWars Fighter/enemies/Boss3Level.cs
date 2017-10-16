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
        //
        public Texture2D[] costume = new Texture2D[6];
        public Texture2D[] force = new Texture2D[8];
        public Texture2D[] lighsaber = new Texture2D[6];
        //
        public bool isEntered;

        public Boss3Level(Texture2D[] costume, Texture2D[] force, Texture2D[] lighsaber)
        {
            position = new Vector2(14780, 640);
            isEntered = false;
            this.costume = costume;
            this.force = force;
            this.lighsaber = lighsaber;
        }

        public void Update(GameTime gameTime)
        {
            EnterArea();

            Death();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

        // Lightsaber

        private void StickOut()
        {

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

        // Death

        private void Death()
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
        }


    }
}
