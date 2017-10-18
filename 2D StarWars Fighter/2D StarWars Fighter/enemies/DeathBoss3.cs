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
    class DeathBoss3
    {
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        public Texture2D[] deathList = new Texture2D[7];
        public int counter, currentFrame;
        public SpriteEffects spriteEffect;

        public DeathBoss3(Texture2D[] deathList, Vector2 position, SpriteEffects spriteEffect)
        {
            this.deathList = deathList;
            this.position = position;
            texture = deathList[0];
            counter = 25;
            currentFrame = 0;
            this.spriteEffect = spriteEffect;
        }

        public void Update(GameTime gameTime)
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                texture = deathList[currentFrame];
                currentFrame++;
            }

            if (currentFrame >= 7)
            {
                currentFrame = 0;
                isVisible = false;
            }

            if (counter <= 0)
                counter = 11;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

    }
}
