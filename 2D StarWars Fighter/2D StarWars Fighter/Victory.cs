using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_StarWars_Fighter
{
    class Victory
    {
        public Texture2D background_texture;
        public Vector2 bg1pos, bg2pos;
        public SpriteFont font, bigfont;
        public int counter;
        public bool isCounting;

        public Victory()
        {
            isCounting = false;
            counter = 10;
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
                    Game1.menuCommand = "menu";
                    MediaPlayer.Play(SoundManager.bgMusic2_level1);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isCounting == false)
            {
                spriteBatch.Draw(background_texture, bg1pos, Color.White);
                spriteBatch.Draw(background_texture, bg2pos, Color.White);
                spriteBatch.DrawString(bigfont, "epilogue", new Vector2(400, 120), Color.Yellow);
                spriteBatch.DrawString(font, "Congratulations! You've saved the Galaxy", new Vector2(300, 300), Color.Yellow);
                spriteBatch.DrawString(font, "Imperial troops are defeated", new Vector2(300, 400), Color.Yellow);
                spriteBatch.DrawString(font, "Now everyone will live in the Peace", new Vector2(300, 500), Color.Yellow);
                spriteBatch.DrawString(font, "Press Enter to Exit", new Vector2(400, 650), Color.White);
            }
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
            if (keyState.IsKeyDown(Keys.Enter))
            {
                MediaPlayer.Stop();
                isCounting = true;
                // SoundManager.endscene1.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
            }
        }

    }
}
