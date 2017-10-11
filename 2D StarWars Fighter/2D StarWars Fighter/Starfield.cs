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
    public class Starfield
    {
        /* @@ OLD
        public Texture2D texture;
        public Vector2 bgPos1, bgPos2; 
        public int speed;

        // Поверхность
        public Texture2D floorTexture;
        public Vector2 flPos1, flPos2;
        */
        // New
        public Texture2D image1, image2, image3, image4, image5, image6, image7, image8, image9;
        public Vector2 pos1, pos2, pos3, pos4, pos5, pos6, pos7, pos8, pos9;


        // Constructor
        public Starfield()
        {
            /* @@ Old
            texture = null;
            bgPos1 = new Vector2(0, 0);
            bgPos2 = new Vector2(1280, 0);
            speed = 2;

            floorTexture = null;
            flPos1 = new Vector2(0, 700);
            flPos2 = new Vector2(1280, 700);
            */

            image1 = null;
            image2 = null;
            image3 = null;
            image4 = null;
            image5 = null;
            image6 = null;
            image7 = null;
            image8 = null;
            image9 = null;

            pos1 = new Vector2(0,0);
            pos2 = new Vector2(1183, 0);
            pos3 = new Vector2(2366, 0);
            pos4 = new Vector2(3549, 0);
            pos5 = new Vector2(4732, 0);
            pos6 = new Vector2(5914, 0);
            pos7 = new Vector2(7097, 0);
            pos8 = new Vector2(8280, 0);
            pos9 = new Vector2(9463, 0);
            
        }

        public void LoadContent(ContentManager Content)
        {
            /* @@@ OLD
            floorTexture = Content.Load<Texture2D>("floor1");
            texture = Content.Load<Texture2D>("space");
             * */
            image1 = Content.Load<Texture2D>("background/image_part_001");
            image2 = Content.Load<Texture2D>("background/image_part_002");
            image3 = Content.Load<Texture2D>("background/image_part_003");
            image4 = Content.Load<Texture2D>("background/image_part_004");
            image5 = Content.Load<Texture2D>("background/image_part_005");
            image6 = Content.Load<Texture2D>("background/image_part_006");
            image7 = Content.Load<Texture2D>("background/image_part_007");
            image8 = Content.Load<Texture2D>("background/image_part_008");
            image9 = Content.Load<Texture2D>("background/image_part_009");
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            /* @@@ OLD
            spriteBatch.Draw(texture, bgPos1, Color.White); // space
            spriteBatch.Draw(texture, bgPos2, Color.White); // space
            spriteBatch.Draw(floorTexture, flPos1, Color.Gray); // surface
            spriteBatch.Draw(floorTexture, flPos2, Color.Gray); // surface
             * */
            spriteBatch.Draw(image1, pos1, Color.White);
            spriteBatch.Draw(image2, pos2, Color.White);
            spriteBatch.Draw(image3, pos3, Color.White);
            spriteBatch.Draw(image4, pos4, Color.White);
            spriteBatch.Draw(image5, pos5, Color.White);
            spriteBatch.Draw(image6, pos6, Color.White);
            spriteBatch.Draw(image7, pos7, Color.White);
            spriteBatch.Draw(image8, pos8, Color.White);
            spriteBatch.Draw(image9, pos9, Color.White);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            /* @@@ OLD

            // Setting speed for background scrolling

            bgPos1.X = bgPos1.X - speed;
            bgPos2.X = bgPos2.X - speed;

            // Scrolling background (Repeating)
            if (bgPos1.X <= -1280)
            {
                bgPos1.X = 0;
                bgPos2.X = 1280;
            }
             * 
             * */


        }


    }
}
