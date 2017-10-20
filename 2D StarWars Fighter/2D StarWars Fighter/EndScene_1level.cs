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
    class EndScene_1level
    {
        public Texture2D background_texture;
        public Vector2 bg1pos, bg2pos;
        public SpriteFont font, bigfont;
        public int counter;
        public bool isCounting;

        public EndScene_1level()
        {
            isCounting = false;
            counter = 1900;
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
                    Game1.menuCommand = "2level";
                    MediaPlayer.Play(SoundManager.level2music);
                    //
                    // reset states
                    isCounting = false;
                    counter = 1900;
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
                spriteBatch.DrawString(bigfont, "CHAPTER ii", new Vector2(400, 120), Color.Yellow);
                spriteBatch.DrawString(font, "After our hero has crashed the Empire main base", new Vector2(200, 300), Color.Yellow);
                spriteBatch.DrawString(font, "Thousands of star fighters began to pursue our hero.", new Vector2(200, 400), Color.Yellow);
                spriteBatch.DrawString(font, "Now, his new goal is to arrive on the red planet", new Vector2(200, 500), Color.Yellow);
                spriteBatch.DrawString(font, "Press Enter to Continue", new Vector2(400, 650), Color.White);
            }
            if(isCounting == true)
            spriteBatch.DrawString(font, "Radio transmission", new Vector2(450, 350), Color.White);
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
                SoundManager.endscene1.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
            }
        }

    }
}
