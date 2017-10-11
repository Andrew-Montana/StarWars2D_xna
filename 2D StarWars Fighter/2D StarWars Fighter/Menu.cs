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
    class Menu
    {
        KeyboardState tempKeyState;

        public int screenWidth, screenHeight, pressTimer;
        // Text and Font
        public SpriteFont menuFont;
        public Vector2 startPos, settingsPos, exitPos;
        // Background
        public Texture2D texture;
        public Vector2 background1Pos, background2Pos;
        // Selected Things
        public bool isStartSelected, isSettingsSelected, isExitSelected;
        public Color startColor, settingsColor, exitColor;

        public Menu()
        {
            tempKeyState = Keyboard.GetState();

            screenHeight = 720;
            screenWidth = 1280;
            startPos = new Vector2((screenWidth / 2) - 170, 200);
            settingsPos = new Vector2((screenWidth / 2) - 170, 300);
            exitPos = new Vector2((screenWidth / 2) - 170, 400);

            texture = null;
            background1Pos = new Vector2(0, 0);
            background2Pos = new Vector2(0, -720);

            isSettingsSelected = false;
            isStartSelected = true;
            isSettingsSelected = false;
            startColor = Color.Yellow;
            settingsColor = Color.Yellow;
            exitColor = Color.Yellow;

            pressTimer = 20;
        }

        public void LoadContent(ContentManager Content)
        {
            menuFont = Content.Load<SpriteFont>("menuFont");
            texture = Content.Load<Texture2D>("space");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (tempKeyState.IsKeyUp(Keys.Enter))
            {
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    if (isStartSelected)
                        Game1.menuCommand = "start";
                    if (isExitSelected)
                        Game1.menuCommand = "exit";
                    if (isSettingsSelected)
                        Game1.menuCommand = "settings";
                }
            }

            tempKeyState = keyState;

            #region Background Scroll

            // Background scrolling down
            background1Pos.Y = background1Pos.Y + 2;
            background2Pos.Y = background2Pos.Y + 2;
            if (background2Pos.Y >= 0)
            {
                background1Pos = new Vector2(0, 0);
                background2Pos = new Vector2(0, -720);
            }

            #endregion
            // Selection magic

            #region Selection
            if (keyState.IsKeyDown(Keys.Up) && pressTimer == 20 || keyState.IsKeyDown(Keys.W) && pressTimer == 20)
            {
                pressTimer = pressTimer - 1;

                if (isStartSelected)
                {
                    isStartSelected = false;
                    isExitSelected = true;
                }
                else
                if (isExitSelected)
                {
                    isSettingsSelected = true;
                    isExitSelected = false;
                }
                else
                if (isSettingsSelected)
                {
                    isSettingsSelected = false;
                    isStartSelected = true;
                }
            }
            if (keyState.IsKeyDown(Keys.Down) && pressTimer == 20 || keyState.IsKeyDown(Keys.S) && pressTimer == 20)
            {
                pressTimer = pressTimer - 1;

                if (isStartSelected)
                {
                    isStartSelected = false;
                    isSettingsSelected = true;
                }
                else
                if (isExitSelected)
                {
                    isExitSelected = false;
                    isStartSelected = true;
                }
                else
                if (isSettingsSelected)
                {
                    isSettingsSelected = false;
                    isExitSelected = true;
                }
            }
            //
            if (isStartSelected)
            {
                startColor = Color.White;
                settingsColor = Color.Yellow;
                exitColor = Color.Yellow;
            }
            else
                if (isSettingsSelected)
                {
                    startColor = Color.Yellow;
                    settingsColor = Color.White;
                    exitColor = Color.Yellow;
                }
                else if (isExitSelected)
                {
                    startColor = Color.Yellow;
                    settingsColor = Color.Yellow;
                    exitColor = Color.White;
                }

            if (pressTimer <= 19)
                pressTimer--;
            if (pressTimer < 5)
                pressTimer = 20;

            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, background1Pos, Color.White);
            spriteBatch.Draw(texture, background2Pos, Color.White);
            spriteBatch.DrawString(menuFont, "Start", startPos, startColor);
            spriteBatch.DrawString(menuFont, "Settings", settingsPos, settingsColor);
            spriteBatch.DrawString(menuFont, "Exit", exitPos, exitColor);
        }

    }
}
