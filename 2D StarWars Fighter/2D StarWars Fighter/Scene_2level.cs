using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter
{
    class Scene_2level
    {
         public Texture2D background_texture;
        public Vector2 bg1pos, bg2pos;
        public SpriteFont font, bigfont;
        public int counter;
        public bool isCounting;

        public Scene_2level()
        {
            isCounting = false;
            counter = 200;
            background_texture = null;
            bg1pos = new Vector2(0, 0);
            bg2pos = new Vector2(0, -720);
            font = null;
        }

        public void LoadContent(ContentManager Content)
        {
            background_texture = Content.Load<Texture2D>("space");
            font = Content.Load<SpriteFont>("myFont");
            bigfont = Content.Load<SpriteFont>("menuFont");
        }

        public void Update(GameTime gameTime)
        {
            ScrollingBackground();
            MoveOnNextLevel();
            if (isCounting == true)
            {
                counter--;
                if (counter <= 0)
                {
                    Game1.menuCommand = "3level";
                    MediaPlayer.Play(SoundManager.level2music);
                    isCounting = false;
                    counter = 200;
                    bg1pos = new Vector2(0, 0);
                    bg2pos = new Vector2(0, -720);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isCounting == false)
            {
                spriteBatch.Draw(background_texture, bg1pos, Color.White);
                spriteBatch.Draw(background_texture, bg2pos, Color.White);
                spriteBatch.DrawString(bigfont, "CHAPTER iii", new Vector2(400, 120), Color.Yellow);
                spriteBatch.DrawString(font, "On the red planet a real carnage flared up", new Vector2(300, 300), Color.Yellow);
                spriteBatch.DrawString(font, "What is waiting for our Jedi?", new Vector2(300, 400), Color.Yellow);
                spriteBatch.DrawString(font, "Only the Force could help him now", new Vector2(300, 500), Color.Yellow);
                spriteBatch.DrawString(font, "Press Enter to Continue", new Vector2(400, 650), Color.White);
            }
            if(isCounting == true)
            spriteBatch.DrawString(font, "Loading", new Vector2(450, 350), Color.White);
        }

        private void ScrollingBackground()
        {
            bg1pos.Y++;
            bg2pos.Y++;

            if (bg2pos.Y >= 0)
            {
                bg1pos = new Vector2(0, 0);
                bg2pos = new Vector2(0, -720);
            }
        }

        private void MoveOnNextLevel()
        {
            KeyboardState keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(Keys.Enter))
            {
                MediaPlayer.Stop();
                isCounting = true;
               // SoundManager.endscene1.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
            }
        }
    }
}
