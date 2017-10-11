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
    class Level2
    {
        private Rectangle mustafarRect;
        private Vector2 mustafarPos;
        private Texture2D textureMustafar;
        private bool isEnd;
        // background
        private Texture2D backgroundTexture;
        private Vector2 bg1Pos, bg2Pos;
        // player
        PlayerShip playerShip = new PlayerShip();
        // asteroids
        private Texture2D asteroidTexture;
        private List<Asteroid> asteroidList = new List<Asteroid>();
        private int asteroidDelay;
        // enemies
        private Texture2D enemyTexture;
        private Texture2D enemyBullet;
        private List<EnemyStarfighter> enemyList = new List<EnemyStarfighter>();
        private int enemyDelay;
        // animation of explosion
        private Texture2D explosionTexture;
        List<Level2Explosions> explosionsList = new List<Level2Explosions>();
        // Difficulty
        private int difficulty;
        private int destroyCount;
        private int enemyLimit;
        private int asteroidLimit;
        private int DefaultAsteroidDelay;
        private int backgroundSpeed;
        private int surviveCounter;

        public Level2()
        {
            mustafarPos = new Vector2(1400, 0);
            isEnd = false;
            //
            surviveCounter = 47000;
            backgroundSpeed = 1;
            DefaultAsteroidDelay = 50;
            enemyLimit = 10;
            asteroidLimit = 30;
            destroyCount = 0;
            difficulty = 1;
            bg1Pos = new Vector2(0, 0);
            bg2Pos = new Vector2(1280, 0);
            backgroundTexture = null;
            asteroidDelay = 120;
            enemyDelay = 80;
            
        }

        public void LoadContent(ContentManager Content)
        {
            backgroundTexture = Content.Load<Texture2D>("space");
            playerShip.LoadContent(Content);
            asteroidTexture = Content.Load<Texture2D>("level2/asteroid");
            enemyTexture = Content.Load<Texture2D>("level2/enemyfighter");
            enemyBullet = Content.Load<Texture2D>("level2/enemybullet");
            explosionTexture = Content.Load<Texture2D>("level2/Spaceship_Explosion_Spritesheet");
            textureMustafar = Content.Load<Texture2D>("level2/Mustafar");
            mustafarRect = new Rectangle((int)mustafarPos.X, (int)mustafarPos.Y, textureMustafar.Width, textureMustafar.Height);
        }

        public void Update(GameTime gameTime)
        {
            ScrollingBackground();
            playerShip.Update(gameTime);
            LoadAsteroids();
            ManageAsteroids();
            LoadEnemies();
            ManageEnemies();
            foreach (Asteroid a in asteroidList)
            {
                a.Update(gameTime);
                AsteroidCollisions(a);
                SetDifficultyAsteroids(a);
            }
            foreach (EnemyStarfighter enemy in enemyList)
            {
                enemy.Update(gameTime);
                EnemyCollisions(enemy);
                EnemyBulletsCollisions(enemy);
            }
            foreach (Level2Explosions explosion in explosionsList)
            {
                explosion.Update(gameTime);
            }
            ManageExplosions();
            GameOver();
            Victory();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, bg1Pos, Color.White);
            spriteBatch.Draw(backgroundTexture, bg2Pos, Color.White);
            if (isEnd == true)
            {
                spriteBatch.Draw(textureMustafar, mustafarPos, Color.White);
            }
            playerShip.Draw(spriteBatch);
            foreach (Asteroid a in asteroidList)
            {
                a.Draw(spriteBatch);
            }
            foreach (EnemyStarfighter enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Level2Explosions explosion in explosionsList)
            {
                explosion.Draw(spriteBatch);
            }
            if(difficulty != 4)
            spriteBatch.DrawString(playerShip.font, "Kill all starfighters! Enemy destroyed: " + destroyCount.ToString() + "", new Vector2(280, 0), Color.Yellow);
            if(difficulty == 4)
                spriteBatch.DrawString(playerShip.font, "Survive!! Distance to the planet " + surviveCounter.ToString(), new Vector2(280, 0), Color.Yellow);
        }

        // methods
        private void ScrollingBackground()
        {
            bg1Pos.X -= backgroundSpeed;
            bg2Pos.X -= backgroundSpeed;
            if (bg2Pos.X <= 0)
            {
                bg1Pos = new Vector2(0, 0);
                bg2Pos = new Vector2(1280, 0);
            }
        }

        // Collisions 

        private void AsteroidCollisions(Asteroid a)
        {
            // if player intersects with asteroid
            if (a.boundingBox.Intersects(playerShip.boundingBox))
            {
                a.isVisible = false;
                playerShip.health -= 20;
                SoundManager.justBoom.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                Level2Explosions explosionObject = new Level2Explosions(a.position, explosionTexture);
                explosionObject.isVisible = true;
                explosionsList.Add(explosionObject);
            }
            // Player Attack at Asteroid
            foreach (Bullet playerBullet in playerShip.bulletList)
            {
                if (playerBullet.boundingBox.Intersects(a.boundingBox))
                {
                    playerBullet.isVisible = false;
                    a.isVisible = false;
                    HUD.playerScore += 10;
                    SoundManager.justBoom.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    Level2Explosions explosionObject = new Level2Explosions(a.position, explosionTexture);
                    explosionObject.isVisible = true;
                    explosionsList.Add(explosionObject);
                }
            }
        }

        private void EnemyCollisions(EnemyStarfighter e)
        {
            // if player intersects with enemy
            if (e.boundingBox.Intersects(playerShip.boundingBox))
            {
                e.isVisible = false;
                playerShip.health -= 20;
                SoundManager.justBoom.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                Level2Explosions explosionObject = new Level2Explosions(new Vector2(e.position.X + e.enemyTexture.Width / 2, e.position.Y + e.enemyTexture.Height / 2), explosionTexture);
                explosionObject.isVisible = true;
                explosionsList.Add(explosionObject);
                destroyCount++;
            }
            //
            foreach (Bullet playerBullet in playerShip.bulletList)
            {
                if (playerBullet.boundingBox.Intersects(e.boundingBox))
                {
                    destroyCount++;
                    playerBullet.isVisible = false;
                    e.isVisible = false;
                    HUD.playerScore += 30;
                    SoundManager.justBoom.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    Level2Explosions explosionObject = new Level2Explosions(new Vector2(e.position.X + e.enemyTexture.Width / 2, e.position.Y + e.enemyTexture.Height/2), explosionTexture);
                    explosionObject.isVisible = true;
                    explosionsList.Add(explosionObject);
                }
            }
        }

        private void EnemyBulletsCollisions(EnemyStarfighter e)
        {
            foreach (Bullet b in e.bulletList)
            {
                if (b.boundingBox.Intersects(playerShip.boundingBox))
                {
                    b.isVisible = false;
                    playerShip.health -= 10;
                }
            }
        }

        private void GameOver()
        {
            if (playerShip.health <= 0)
            {
                Game1.menuCommand = "GameOver";
            }
        }
        // Asteroids
        private void LoadAsteroids()
        {
            if (asteroidDelay > 0)
                asteroidDelay--;

            if (asteroidDelay <= 0)
            {
                Asteroid newAsteroid = new Asteroid(asteroidTexture);
                newAsteroid.isVisible = true;

                if (asteroidList.Count < asteroidLimit)
                    asteroidList.Add(newAsteroid);
            }

            if (asteroidDelay <= 0)
                asteroidDelay = DefaultAsteroidDelay;
        }

        private void ManageAsteroids()
        {
            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (asteroidList[i].isVisible == false)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }

        }

        // Enemies
        private void LoadEnemies()
        {
            if (enemyDelay > 0)
                enemyDelay--;

            if (enemyDelay <= 0)
            {
                EnemyStarfighter newEnemy = new EnemyStarfighter(enemyTexture,enemyBullet);
                newEnemy.isVisible = true;

                if (enemyList.Count < enemyLimit && enemyLimit != 0)
                    enemyList.Add(newEnemy);
            }

            if (enemyDelay <= 0)
                enemyDelay = 80;
        }

        private void ManageEnemies()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].isVisible == false)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        // Explosions
        private void ManageExplosions()
        {
            for (int i = 0; i < explosionsList.Count; i++)
            {
                if (explosionsList[i].isVisible == false)
                {
                    explosionsList.RemoveAt(i);
                    i--;
                }
            }
        }

        // Difficulty
        private void SetDifficultyAsteroids(Asteroid a)
        {
            if (destroyCount == 15)
                difficulty = 2;
            else
            if (destroyCount == 30)
                difficulty = 3;
            else
                if (destroyCount > 35)
                    difficulty = 4;

            if (difficulty == 2)
            {
                a.speed = 7;
                DefaultAsteroidDelay = 40;
                backgroundSpeed = 4;
            }
            else
                if (difficulty == 3)
                {
                    a.speed = 11;
                    asteroidLimit = 40;
                    enemyLimit = 4;
                    DefaultAsteroidDelay = 30;
                    backgroundSpeed = 8;
                }
                else
                    if (difficulty == 4)
                    {
                        enemyLimit = 0;
                        a.speed = 15;
                        asteroidLimit = 70;
                        DefaultAsteroidDelay = 18;
                        if(surviveCounter > 0)
                        backgroundSpeed = 16;
                      //  surviveCounter++;
                        surviveCounter--;
                        if (enemyList.Count != 0)
                        {
                            for (int i = 0; i < enemyList.Count; i++)
                            {
                                if (enemyList[i].position.X <= 0)
                                {
                                    enemyList.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
        }

        // Win
        private void Victory()
        {
            if (surviveCounter <= 0 && difficulty == 4)
            {
                asteroidLimit = 0;
                enemyLimit = 0;
                backgroundSpeed = 1;
                // Очистка листа врагов

                if (enemyList.Count != 0)
                {
                    for (int i = 0; i < enemyList.Count; i++)
                    {
                        if (enemyList[i].position.X <= 0)
                        {
                            enemyList.RemoveAt(i);
                            i--;
                        }
                    }
                }


                // Очистка листа астеройдов
                if (asteroidList.Count != 0)
                {

                    for (int i = 0; i < asteroidList.Count; i++)
                    {
                        if (asteroidList[i].position.X <= 0)
                        {
                            asteroidList.RemoveAt(i);
                            i--;
                        }
                    }
                }

                //
                isEnd = true;
                MustafarMove();
                // Collision. Go to 3 level
                if (mustafarRect.Intersects(playerShip.boundingBox))
                {
                    Game1.menuCommand = "level2scene";
                    MediaPlayer.Play(SoundManager.mainthemeMusic);
                }
            }

        }

        // planet moving

        private void MustafarMove()
        {
            mustafarPos.X--;
            mustafarRect = new Rectangle((int)mustafarPos.X, (int)mustafarPos.Y, textureMustafar.Width, textureMustafar.Height);
        }

        private void MustagarCollision()
        {
            if(playerShip.boundingBox.Intersects(mustafarRect))
            {
                //
            }
        }

    }
}
