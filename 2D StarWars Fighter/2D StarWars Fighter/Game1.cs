using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // State Enum
        public enum State
        {
            Menu, Level1, Gameover, Settings, Level1EndScene, Level2, Level2EndScene, Level3
        }

        #region variables

        public int enemyCount = 19;
        public int platformBossCounter = 200;
        static public Texture2D platform1_texture;
        static public Texture2D platform1_texture2;
        static public Texture2D platform1_texture3;
        List<Platform> platformList = new List<Platform>();

        public bool isPaused = false;
        public KeyboardState pauseKeyState = Keyboard.GetState();
        public KeyboardState tempPauseKeyState =  Keyboard.GetState();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        public int enemyBulletDamage;

        // Enemy List
        List<Enemy1> enemyList = new List<Enemy1>();
        Texture2D[] enemyRunArr = new Texture2D[4];

        // Explosion List
        List<ExplosionEnemy1> explosionEnemyList = new List<ExplosionEnemy1>();

        // Instantiang our Player and Starfield objects
        Player p = new Player();
        Starfield sf = new Starfield();

        // Boss 1 Level
        _1LevelBoss boss_level1;

        // Camera
        Camera camera;

        // HUD
        HUD hud = new HUD();

        // Sound
        SoundManager sm = new SoundManager();

        // Sound first time. "He Is tthere", blast him
        private bool isTherePlayerSound = true;
        private bool isStopThatSound = true;

        // set first State
        State gameState = State.Menu;
        static public string menuCommand = "Menu";

        // # Menu 
        Menu menu = new Menu();

        // # Settings
        Settings settings = new Settings();

        #endregion
        EndScene_1level endscene_object = new EndScene_1level();
        Scene_2level endscene_level2 = new Scene_2level();
        Level2 level2 = new Level2();
        Level3 level3 = new Level3();

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            this.Window.Title = "XNA = 2D StarWars Fighter";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
            SoundManager.musicVolume = 1.0f;
            SoundManager.effectsVolume = 1.0f;
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            boss_level1 = new _1LevelBoss(p);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sm.LoadContent(Content); // Loads Sound

            // Sound
            MediaPlayer.Play(sm.bgMusic); // background song is playing
                #region Level1

                        p.LoadContent(Content);
                        sf.LoadContent(Content);
                        enemyRunArr[0] = Content.Load<Texture2D>("enemy1/run1");
                        enemyRunArr[1] = Content.Load<Texture2D>("enemy1/run2");
                        enemyRunArr[2] = Content.Load<Texture2D>("enemy1/run3");
                        enemyRunArr[3] = Content.Load<Texture2D>("enemy1/run4");
                        hud.LoadContent(Content, p);

                        // Platform
                        platform1_texture = Content.Load<Texture2D>("onebox2");
                        platform1_texture2 = Content.Load<Texture2D>("onebox2empty");
                        platform1_texture3 = Content.Load<Texture2D>("platform2");
                        // Boss
                        boss_level1.LoadContent(Content);

                        // ## End Scene. 1 Level
                        endscene_object.LoadContent(Content);
                        // ## End Scene 2 Level
                        endscene_level2.LoadContent(Content);
                        

                #endregion

                #region Menu

                        menu.LoadContent(Content);
  

                #endregion

                // Level 2
                level2.LoadContent(Content);

                #region Gameover



                #endregion

                #region Settings

            settings.LoadContent(Content);

            #endregion

                // Level 3
            level3.LoadContent(Content);


        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
           // if (gameState != State.Level2 && gameState != State.Level2EndScene) // delete it later
            if(gameState != State.Level3)
                gameState = State.Level3; // delete it later
            #region 1 Level Sounds
            if (HUD.playerScore >= 35 && isTherePlayerSound && gameState == State.Level1)
            {
                SoundManager.there_is_one.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                isTherePlayerSound = false;
            }
            if (HUD.playerScore >= 600 && isStopThatSound && gameState == State.Level1)
            {
                SoundManager.stop_that_blasthim.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                isStopThatSound = false;
            }
            #endregion
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            pauseKeyState = Keyboard.GetState();
            Pause();

            if (!isPaused)
            {
                switch (gameState)
                {
                    #region Level1
                    case State.Level1:
                        {
                            camera.isLevel3 = false;
                            // Переход на сцену 2го уровня
                            if(menuCommand == "Level1EndScene")
                            {
                                gameState = State.Level1EndScene;
                                menuCommand = "";
                                MediaPlayer.Play(SoundManager.mainthemeMusic);
                            }

                            // Check player collision with platform
                            foreach (Platform platform1 in platformList)
                            {
                                if (p.position.Y >= (platform1.position.Y - 103) && p.boundingBox.Intersects(platform1.boundingBox) && p.isJump)
                                {
                                    KeyboardState keybState = Keyboard.GetState();
                                    p.position.Y = platform1.position.Y - 104;
                                    if (tempPauseKeyState.IsKeyUp(Keys.W))
                                    {
                                        if (keybState.IsKeyDown(Keys.W))
                                        {
                                            p.force = 20;
                                        }
                                    }
                                }
                            }

                            // Updating Enemy's and checking collision of enemy to 
                            foreach (Enemy1 e in enemyList)
                            {
                                // Check if enemy is colliding with player
                                if (e.boundingBox.Intersects(p.boundingBox))
                                {
                                    //  p.health -= 20;
                                }

                                // Check enemy bullet collision with player
                                for (int i = 0; i < e.bulletList.Count; i++)
                                {
                                    if (p.boundingBox.Intersects(e.bulletList[i].boundingBox))
                                    {

                                        // if player is not defending OR if player look left and bullet is going from right side, he gets the damage or if player look right and bullet is in going from left side, he gets the damage

                                        if (!p.isDefending || (p.spriteEffect == SpriteEffects.FlipHorizontally && e.bulletList[i].boundingBox.X >= p.boundingBox.X) || (p.spriteEffect == SpriteEffects.None && e.bulletList[i].boundingBox.X <= p.boundingBox.X))
                                        {
                                            p.health -= enemyBulletDamage;
                                        }

                                        e.bulletList[i].isVisible = false;
                                    }
                                }

                                // Check player attack collision to enemy
                                if (p.isAttacking == true && p.boundingBox.Intersects(e.boundingBox))
                                {
                                    sm.explodeSound.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                                    e.isVisible = false;
                                    explosionEnemyList.Add(new ExplosionEnemy1(new Vector2(e.position.X, e.position.Y), e.spriteEffect));
                                    foreach (ExplosionEnemy1 explosionEnemy in explosionEnemyList)
                                    {
                                        explosionEnemy.LoadContent(Content);
                                    }
                                    HUD.playerScore += 35;

                                }

                                // Check if enemy objects intersects with other enemy
                                for (int i = 0; i < enemyList.Count; i++)
                                {
                                    for (int j = 0; j < enemyList.Count; j++)
                                    {
                                        if (enemyList[i].boundingBox.Intersects(enemyList[j].boundingBox) && i != j && enemyList[i].position.X < enemyList[j].position.X)
                                        {
                                            if (enemyList[j].spriteEffect == SpriteEffects.None)
                                            {
                                                enemyList[i].position.X = enemyList[j].position.X - enemyList[j].texture.Width;
                                                enemyList[i].isStandingLeft = true;
                                            }
                                            break;
                                        }
                                        else if (enemyList[i].boundingBox.Intersects(enemyList[j].boundingBox) && i != j && enemyList[i].position.X > enemyList[j].position.X)
                                        {
                                            if (enemyList[j].spriteEffect == SpriteEffects.FlipHorizontally)
                                            {
                                                enemyList[i].position.X = enemyList[j].position.X + enemyList[j].texture.Width;
                                                enemyList[i].isStandingRight = true;
                                            }
                                            break;
                                        }
                                    }
                                }

                                e.Update(gameTime);

                            }



                            // TODO: Add your update logic here
                            foreach (ExplosionEnemy1 explosionEnemy1 in explosionEnemyList)
                            {
                                explosionEnemy1.Update(gameTime);
                            }
                            ManageExplosionsEnemy1();

                            hud.Update(gameTime, p);
                            p.Update(gameTime);
                            //  sf.Update(gameTime);
                            camera.Update(gameTime, p);
                            LoadEnemy1();
                            boss_level1.Update(gameTime);

                            if (p.health <= 0)
                            {
                                gameState = State.Gameover;
                                if (boss_level1.isDeath == true || boss_level1.isVisible)
                                {
                                    boss_level1.SetDefault();
                                    boss_level1.isDeath = false;
                                    boss_level1.isVisible = false;
                                }
                                if (p.position.Y <= 720)
                                    p.isJump = true;
                            }

                            LoadPlatform();

                            break;
                        }
                    #endregion

                    #region Menu
                    // Updating Menu State
                    case State.Menu:
                        {
                            menu.Update(gameTime);

                            // getting command from menu class while we're in menu
                            if (menuCommand == "start")
                            {
                                gameState = State.Level1;
                                menuCommand = "";
                            }
                            if (menuCommand == "settings")
                            {
                                gameState = State.Settings;
                                menuCommand = "";
                            }
                            if (menuCommand == "exit")
                            {
                                this.Exit();
                            }

                            break;
                        }
                    #endregion

                    #region Gameover
                    // Updating gameover state
                    case State.Gameover:
                        {
                           // SoundManager.musicVolume = 0.0f;
                           // MediaPlayer.Volume = 0.0f;
                            MediaPlayer.Stop();
                            if (tempPauseKeyState.IsKeyUp(Keys.Enter))
                            {
                                if (pauseKeyState.IsKeyDown(Keys.Enter))
                                {
                                    p.isHealthBonusUsed = false;
                                    p.health = 200;
                                    HUD.playerScore = 0;
                                    enemyCount = 19;
                                    p.position = new Vector2(300, 720);
                                    p.isEndPosition = false;
                                    foreach (var item in enemyList)
                                    {
                                        item.isVisible = false;
                                    }
                                    gameState = State.Level1;
                                    MediaPlayer.Play(sm.bgMusic);
                                //    SoundManager.musicVolume = 1.0f;
                                //    MediaPlayer.Volume = SoundManager.musicVolume;
                                }
                            }

                            if (boss_level1.isDeath == true || boss_level1.isVisible)
                            {
                                boss_level1.SetDefault();
                                boss_level1.isDeath = false;
                                boss_level1.isVisible = false;
                            }
                            if (p.position.Y <= 720)
                                p.isJump = true;
                            break;
                        }
                    #endregion

                    #region Settings

                    case State.Settings:
                        {
                            settings.Update(gameTime);

                            if (menuCommand == "menu")
                                gameState = State.Menu;
                            break;
                        }

                    #endregion

                    #region Level1EndScene

                    case State.Level1EndScene:
                        {
                            endscene_object.Update(gameTime);
                            if (menuCommand == "2level")
                            {
                                menuCommand = "";
                                gameState = State.Level2;
                            }
                            break;
                        }

                    #endregion

                    #region Level2

                    case State.Level2:
                            {
                                level2.Update(gameTime);

                                if (menuCommand == "level2scene")
                                {
                                    menuCommand = "";
                                    gameState = State.Level2EndScene;
                                }
                                break;
                            }

                    #endregion

                    #region Level2EndScene

                    case State.Level2EndScene:
                            {
                                endscene_level2.Update(gameTime);
                                if (menuCommand == "3level")
                                {
                                    menuCommand = "";
                                    gameState = State.Level3;
                                }
                                break;
                            }

                    #endregion

                    #region Level3
                    case State.Level3:
                            {
                                camera.isLevel3 = true;
                                level3.Update(gameTime);
                                camera.Update(gameTime, level3.GetPlayer());

                           //     if (menuCommand == "level2scene")
                         //       {
                           //         menuCommand = "";
                          //          gameState = State.Level2EndScene;
                         //       }
                                break;
                            }
                    #endregion

                }

                if (menuCommand == "GameOver")
                {
                    gameState = State.Gameover;
                    menuCommand = "";
                }
            }

            // Sound Settings
            // # music
            MediaPlayer.Volume = SoundManager.musicVolume;
            // # effects

            tempPauseKeyState = pauseKeyState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            switch (gameState)
            {
                #region Level 1

                case State.Level1:
                    {
                        // TODO: Add your drawing code here

                        // If player in beginning, then camera will be off
                        //  if(p.position.X >= 0 && p.position.X <= 900)
                        //    spriteBatch.Begin();
                        //  If player is little bit far from the beginning, then camera is ON
                        //   if(p.position.X >= 901)
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                        sf.Draw(spriteBatch);
                        foreach (Platform platform in platformList)
                        {
                            platform.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);
                        hud.Draw(spriteBatch);
                        boss_level1.Draw(spriteBatch);
                        foreach (Enemy1 e in enemyList)
                        {
                            e.Draw(spriteBatch);
                        }

                        foreach (ExplosionEnemy1 explosionEnemy in explosionEnemyList)
                        {
                            explosionEnemy.Draw(spriteBatch);
                        }

                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Menu

                case State.Menu:
                    {
                        spriteBatch.Begin();
                        menu.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Gameover
                case State.Gameover:
                    {
                        spriteBatch.Begin();
                        spriteBatch.DrawString(menu.menuFont, "Game over", new Vector2(300, 170), Color.Yellow);
                        spriteBatch.DrawString(menu.menuFont, "Your score is " + HUD.playerScore, new Vector2(300, 370), Color.Yellow);
                        spriteBatch.End();
                        break;
                    }
                #endregion

                #region Settings

                case State.Settings:
                    {
                        spriteBatch.Begin();
                        settings.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Level1EndScene

                case State.Level1EndScene:
                    {
                        spriteBatch.Begin();
                        endscene_object.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Level2

                case State.Level2:
                    {
                        spriteBatch.Begin();
                        level2.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Level2EndScene

                case State.Level2EndScene:
                    {
                        spriteBatch.Begin();
                        endscene_level2.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }

                #endregion

                #region Level3
                case State.Level3:
                    {
                        //  spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
                        level3.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }
                #endregion

            }

            if (isPaused)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(menu.menuFont, "Paused", new Vector2((menu.screenWidth / 3) + 40, (menu.screenHeight / 3) + 40), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #region Level 1 Methods
        // # Methods for 1 Level

        // Load Enemies

        public void LoadEnemy1()
        {
            // # ENEMY SPAWN BEGIN

            int X, x1, x2;
            int Y = 720 - 87;

            x1 = random.Next((int)p.position.X - 2000, (int)p.position.X - 900);
            x2 = random.Next((int)p.position.X + 900,(int)p.position.X + 7000);

            if (random.Next(0, 120) <= 14)
                X = x1;
            else
                X = x2;

            // Creating Y position and random X position

            // # ENEMY SPAWN END
            #region уменьшаем колво врагов

            if (HUD.playerScore >= 100 && HUD.playerScore < 199)
                enemyCount = 18;
            if (HUD.playerScore >= 200 && HUD.playerScore < 299)
                enemyCount = 17;
            if (HUD.playerScore >= 300 && HUD.playerScore < 399)
                enemyCount = 16;
            if (HUD.playerScore >= 400 && HUD.playerScore < 499)
                enemyCount = 15;
            if (HUD.playerScore >= 500 && HUD.playerScore < 599)
                enemyCount = 14;
            if (HUD.playerScore >= 600 && HUD.playerScore < 699)
                enemyCount = 13;
            if (HUD.playerScore >= 700 && HUD.playerScore < 799)
                enemyCount = 12;
            if (HUD.playerScore >= 800 && HUD.playerScore < 899)
                enemyCount = 11;
            if (HUD.playerScore >= 900 && HUD.playerScore < 999)
                enemyCount = 10;
            if (HUD.playerScore >= 1000 && HUD.playerScore < 1099)
                enemyCount = 9;
            if (HUD.playerScore >= 1100 && HUD.playerScore < 1199)
                enemyCount = 8;
            if (HUD.playerScore >= 1200 && HUD.playerScore < 1299)
                enemyCount = 7;
            if (HUD.playerScore >= 1300 && HUD.playerScore < 1399)
                enemyCount = 6;
            if (HUD.playerScore >= 1400 && HUD.playerScore < 1499)
                enemyCount = 5;
            if (HUD.playerScore >= 1500 && HUD.playerScore < 1599)
                enemyCount = 4;
            if (HUD.playerScore >= 1600 && HUD.playerScore < 1699)
                enemyCount = 3;
            if (HUD.playerScore >= 1700 && HUD.playerScore < 1799)
                enemyCount = 2;

            #endregion
            // If there are less than n enemies on the screen, then create more until there is n again
            if (enemyList.Count() < enemyCount && !p.isEndPosition)
            {
                enemyList.Add(new Enemy1(Content.Load<Texture2D>("enemy1/hurt17"), new Vector2(X, Y), Content.Load<Texture2D>("enemy1/blasterbolt"), p, SpriteEffects.None, enemyRunArr, sm));
            }

            // if anyone has been destroyed
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
                
        }

        public void ManageExplosionsEnemy1()
        {
            for (int i = 0; i < explosionEnemyList.Count; i++)
            {
                if (explosionEnemyList[i].isVisible == false)
                {
                    explosionEnemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        // Platforms

        public void LoadPlatform()
        {
            if(platformList.Count == 0)
                platformList.Add(new Platform(platform1_texture, new Vector2(1800, 570)));
            if (platformList.Count == 1)
                platformList.Add(new Platform(platform1_texture2, new Vector2(1955, 570)));
            if (platformList.Count == 2)
                platformList.Add(new Platform(platform1_texture, new Vector2(2110, 570)));
            if (platformList.Count == 3)
                platformList.Add(new Platform(platform1_texture3, new Vector2(9350, 570)));
            if (platformList.Count == 4)
                platformList.Add(new Platform(platform1_texture3, new Vector2(10520, 570)));

            // # Boss level. Platform animation
            if (platformList.Count >= 4)
            {
                platformBossCounter--;

                if (platformBossCounter >= 100)
                {
                    platformList[3].position.Y = 670;
                    platformList[4].position.Y = 670;

                    platformList[3].boundingBox = new Rectangle((int)platformList[3].position.X, (int)platformList[3].position.Y, platformList[3].texture.Width, platformList[3].texture.Height);
                    platformList[4].boundingBox = new Rectangle((int)platformList[4].position.X, (int)platformList[4].position.Y, platformList[4].texture.Width, platformList[4].texture.Height);
                }
                if (platformBossCounter <= 100)
                {
                    platformList[3].position.Y = 570;
                    platformList[4].position.Y = 570;

                    platformList[3].boundingBox = new Rectangle((int)platformList[3].position.X, (int)platformList[3].position.Y, platformList[3].texture.Width, platformList[3].texture.Height);
                    platformList[4].boundingBox = new Rectangle((int)platformList[4].position.X, (int)platformList[4].position.Y, platformList[4].texture.Width, platformList[4].texture.Height);
                }
                if (platformBossCounter <= 0)
                {
                    platformBossCounter = 200;
                }
            }
        }

        #endregion

        public void Pause()
        {
            if (tempPauseKeyState.IsKeyUp(Keys.Escape))
            {
                if (pauseKeyState.IsKeyDown(Keys.Escape))
                {
                    if (isPaused == true)
                        isPaused = false;
                    else isPaused = true;
                }
            }
        }


    }
}
