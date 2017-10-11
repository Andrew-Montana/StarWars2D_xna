using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _2D_StarWars_Fighter
{
   public class ExplosionEnemy1
    {
       public Texture2D texture;
       public Texture2D[] hurtArray;
       public Vector2 position, mainPosition;
       public int timer, currentFrame;
       public bool isVisible;
       public SpriteEffects spriteEffect;

       public ExplosionEnemy1(Vector2 newposition, SpriteEffects newspriteEffect)
       {
           hurtArray = new Texture2D[10];
           texture = null;
           mainPosition = newposition;
           position = newposition;
           timer = 0; currentFrame = 0;
           isVisible = true;
           spriteEffect = newspriteEffect;

       }

       public void LoadContent(ContentManager Content)
       {
           hurtArray[0] = Content.Load<Texture2D>("enemy1/hurt1");
           texture = hurtArray[0];
           hurtArray[1] = Content.Load<Texture2D>("enemy1/hurt2");
           hurtArray[2] = Content.Load<Texture2D>("enemy1/hurt3");
           hurtArray[3] = Content.Load<Texture2D>("enemy1/hurt4");
           hurtArray[4] = Content.Load<Texture2D>("enemy1/hurt5");
           hurtArray[5] = Content.Load<Texture2D>("enemy1/hurt6");
           hurtArray[6] = Content.Load<Texture2D>("enemy1/hurt7");
           hurtArray[7] = Content.Load<Texture2D>("enemy1/hurt8");
           hurtArray[8] = Content.Load<Texture2D>("enemy1/hurt9");
           hurtArray[9] = Content.Load<Texture2D>("enemy1/hurt10");
       }

       public void Draw(SpriteBatch spriteBatch)
       {
           spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
       }

       public void Update(GameTime gameTime)
       {
           timer += gameTime.ElapsedGameTime.Milliseconds;
           if (timer > 100)
           {
               currentFrame++;
               timer = 0;
           }
           if (currentFrame >= 10)
           {
               currentFrame = 0;
               isVisible = false;
           }

           if (currentFrame == 0) position = new Vector2(mainPosition.X, mainPosition.Y);  // 87 height of the sprite
           if (currentFrame == 1) position = new Vector2(mainPosition.X, mainPosition.Y);  // 87 height of the sprite
           if (currentFrame == 2) position = new Vector2(mainPosition.X, mainPosition.Y + 25);  // 112 height of the sprite
           if (currentFrame == 3) position = new Vector2(mainPosition.X, mainPosition.Y + 15);  // 72 height of the sprite
           if (currentFrame == 4) position = new Vector2(mainPosition.X, mainPosition.Y + 50);  //37 ...
           if (currentFrame == 5) position = new Vector2(mainPosition.X, mainPosition.Y + 42); // 45
           if (currentFrame == 6) position = new Vector2(mainPosition.X, mainPosition.Y + 55); // 32
           if (currentFrame == 7) position = new Vector2(mainPosition.X, mainPosition.Y + 62); // 25
           if (currentFrame == 8) position = new Vector2(mainPosition.X, mainPosition.Y + 68); // 19
           if (currentFrame == 9) position = new Vector2(mainPosition.X, mainPosition.Y + 66);  // 21

           texture = hurtArray[currentFrame];

       }


    }
}
