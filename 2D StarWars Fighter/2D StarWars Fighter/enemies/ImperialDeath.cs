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
    class ImperialDeath
    {
        public Texture2D texture, die1, die2, die3;
        public Vector2 position;
        public bool isVisible;
        public int counter;
        public SpriteEffects spriteEffect;

        public ImperialDeath(Texture2D die1, Texture2D die2, Texture2D die3, Vector2 position, SpriteEffects spriteEffect)
        {
            this.die1 = die1;
            this.die2 = die2;
            this.die3 = die3;
            texture = die1;
            this.spriteEffect = spriteEffect;
            this.position = position;
            counter = 15;
        }

        public void Update(GameTime gameTime)
        {
            if (counter > 0)
                counter--;

            if (counter <= 0)
            {
                if (texture == die1)
                    texture = die2;
                else if (texture == die2)
                    texture = die3;
                else if (texture == die3)
                    isVisible = false;
            }

            if (counter <= 0)
            {
                counter = 15;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

    }
}
