using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using _2D_StarWars_Fighter.enemies;

namespace _2D_StarWars_Fighter
{
    public class Bullet
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public bool isVisible;
        public float speed;
        public bool isLeft;
        public bool isRight;

        // walker
        public int step { get; set; }
        public Walker walkerRef;
        // Constructor
        public Bullet(Texture2D newTexture)
        {
            step = 0;
            texture = newTexture;
            speed = 10;
            isVisible = false;
            isLeft = false;
            isRight = false;
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
