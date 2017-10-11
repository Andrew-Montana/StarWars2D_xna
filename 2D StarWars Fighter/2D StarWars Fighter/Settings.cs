using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter
{
    // Reset is Apply. Dont want to change names...
    class Settings
    {
        KeyboardState tempKeyState;

        public int screenWidth, screenHeight;
        public SpriteFont settingsFont;
        public Vector2 backPos, ApplyPos, musicPos, effectsPos;
        // Background
        public Texture2D texture;
        public Vector2 background1Pos, background2Pos;
        // Volume
        public Texture2D musicTexture, effectsTexture, yellowTexture, whiteTexture;
        public Rectangle yellowRect1, yellowRect2;
        public Vector2 yellowBoxPos1, yellowBoxPos2;
        public int musicWidth, effectsWidth;

        // Selecting
        public Color backColor, applyColor;
        public int pressingDelay, afterMenuDelay;
        public bool isMusicSelected, isEffectsSelected, isBackSelected, isApplySelected;

        public Settings()
        {
            tempKeyState = Keyboard.GetState();

            screenHeight = 720;
            screenWidth = 1280;
            backPos = new Vector2(100, 520);
            ApplyPos = new Vector2(880, 520);
            musicPos = new Vector2((screenWidth / 3) - 170, 200);
            effectsPos = new Vector2((screenWidth / 3) - 170, 300);

            texture = null;
            background1Pos = new Vector2(0, 0);
            background2Pos = new Vector2(0, -720);

            yellowBoxPos1 = new Vector2(((screenWidth / 3) + 250), 240);
            yellowBoxPos2 = new Vector2(((screenWidth / 3) + 250), 335);

            musicWidth = 450;
            effectsWidth = 450;

            yellowRect1 = new Rectangle((int)yellowBoxPos1.X, (int)yellowBoxPos1.Y, musicWidth, 65);
            yellowRect2 = new Rectangle((int)yellowBoxPos2.X, (int)yellowBoxPos2.Y, effectsWidth, 65);

            backColor = Color.Yellow;
            applyColor = Color.Yellow;
            pressingDelay = 20;

            isMusicSelected = false; isEffectsSelected = false; isBackSelected = true; isApplySelected = false;
            afterMenuDelay = 40;

        }

        public void LoadContent(ContentManager Content)
        {
            settingsFont = Content.Load<SpriteFont>("menuFont");
            texture = Content.Load<Texture2D>("space");
            yellowTexture = Content.Load<Texture2D>("yellowRect");
            whiteTexture = Content.Load<Texture2D>("whiteRect");

            musicTexture = yellowTexture;
            effectsTexture = yellowTexture;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

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

            #region границы звука

            if (effectsWidth < 0)
                effectsWidth = 0;
            if (effectsWidth > 450)
                effectsWidth = 450;
            if (SoundManager.effectsVolume < 0.0f)
                SoundManager.effectsVolume = 0.0f;
            if (SoundManager.musicVolume < 0.0f)
                SoundManager.musicVolume = 0.0f;
            if (SoundManager.musicVolume > 1.0f)
                SoundManager.musicVolume = 1.0f;
            if (SoundManager.effectsVolume > 1.0f)
                SoundManager.effectsVolume = 1.0f;

            #endregion

            #region Selecting

            // # Back button
            if (isBackSelected)
            {
                backColor = Color.White;
                if(pressingDelay == 20)
                {
                    if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                    {
                        pressingDelay -= 1;
                        isBackSelected = false;
                        isApplySelected = true;
                        backColor = Color.Yellow;
                    }
                    if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                    {
                        pressingDelay -= 1;
                        isBackSelected = false;
                        isEffectsSelected = true;
                        backColor = Color.Yellow;
                    }
                }
            }

            // # Apply button
            if (isApplySelected)
            {
                applyColor = Color.White;
                if (pressingDelay == 20)
                {
                    if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                    {
                        pressingDelay -= 1;
                        isBackSelected = true;
                        isApplySelected = false;
                        applyColor = Color.Yellow;
                    }
                    if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                    {
                        pressingDelay -= 1;
                        isApplySelected = false;
                        isEffectsSelected = true;
                        applyColor = Color.Yellow;
                    }
                }
            }
            
            // # Effects Volume
            if (isEffectsSelected)
            {
                effectsTexture = whiteTexture;
                // Up and Down
                if (pressingDelay == 20)
                {
                    if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                    {
                        pressingDelay -= 1;
                        isMusicSelected = true;
                        isEffectsSelected = false;
                        effectsTexture = yellowTexture;
                    }
                    if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                    {
                        pressingDelay -= 1;
                        isBackSelected = true;
                        isEffectsSelected = false;
                        effectsTexture = yellowTexture;
                    }
                }
                // Left and Right
                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                {
                    
                        if (effectsWidth < 0)
                            effectsWidth = 0;
                        if (effectsWidth > 450)
                            effectsWidth = 450;

                    if(effectsWidth != 0)
                            effectsWidth -= 45;
                    if(SoundManager.effectsVolume != 0.0f)
                        SoundManager.effectsVolume -= 0.1f;
                        yellowRect2 = new Rectangle((int)yellowBoxPos2.X, (int)yellowBoxPos2.Y, effectsWidth, 65);

                    
                }
                if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                {

                    if(effectsWidth != 450)
                            effectsWidth += 45;
                    if (SoundManager.effectsVolume != 1.0f)
                    SoundManager.effectsVolume += 0.1f;
                        yellowRect2 = new Rectangle((int)yellowBoxPos2.X, (int)yellowBoxPos2.Y, effectsWidth, 65);
                    
                }
            }

            // # Music Volume
            if (isMusicSelected)
            {
                musicTexture = whiteTexture;
                // Up and Down
                if (pressingDelay == 20)
                {
                    if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                    {
                        pressingDelay -= 1;
                        isMusicSelected = false;
                        isEffectsSelected = true;
                        musicTexture = yellowTexture;
                    }
                }
                // Left and Right
                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                {

                    if (musicWidth != 0)
                        musicWidth -= 45;

                    if(SoundManager.musicVolume != 0.0f)
                    SoundManager.musicVolume -= 0.1f;
                    yellowRect1 = new Rectangle((int)yellowBoxPos1.X, (int)yellowBoxPos1.Y, musicWidth, 65);


                }
                if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                {

                    
                    if (musicWidth < 0)
                        musicWidth = 0;
                    if (musicWidth > 450)
                        musicWidth = 450;

                    if (musicWidth != 450)
                        musicWidth += 45;
                    if (SoundManager.musicVolume != 1.0f)
                    SoundManager.musicVolume += 0.1f;
                    yellowRect1 = new Rectangle((int)yellowBoxPos1.X, (int)yellowBoxPos1.Y, musicWidth, 65);

                }
            }

            // Delay reset
            if (pressingDelay <= 19)
                pressingDelay--;
            if (pressingDelay < 5)
                pressingDelay = 20;

            // Sound
            #endregion
            if (tempKeyState.IsKeyUp(Keys.Enter))
            {
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    if (isBackSelected)
                    {
                        Game1.menuCommand = "menu";
                    }

                    if (isApplySelected)
                    {
                        SoundManager.musicVolume = 1.0f;
                        SoundManager.effectsVolume = 1.0f;
                        musicWidth = 450;
                        effectsWidth = 450;
                        yellowRect1 = new Rectangle((int)yellowBoxPos1.X, (int)yellowBoxPos1.Y, musicWidth, 65);
                        yellowRect2 = new Rectangle((int)yellowBoxPos2.X, (int)yellowBoxPos2.Y, effectsWidth, 65);
                    }
                }
            }

            tempKeyState = keyState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, background1Pos, Color.White);
            spriteBatch.Draw(texture, background2Pos, Color.White);
            spriteBatch.DrawString(settingsFont, "Back", backPos, backColor);
            spriteBatch.DrawString(settingsFont, "Reset", ApplyPos, applyColor);
            spriteBatch.DrawString(settingsFont, "Music", musicPos, Color.Yellow);
            spriteBatch.DrawString(settingsFont, "Effects", effectsPos, Color.Yellow);

            spriteBatch.Draw(musicTexture, yellowRect1, Color.White);
            spriteBatch.Draw(effectsTexture, yellowRect2, Color.White);
        }

    }
}
