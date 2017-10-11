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
    class PlayerShip
    {
        // Variables
        public Texture2D texture;
        public Vector2 position;
        public int speed, health;
        public Rectangle boundingBox;
        
        // Animation
        public Texture2D animationTexture;
        public Vector2 animPos;
        public int currentFrame, animationCounter, spriteWidth, spriteHeight;
        public Rectangle animRect;
        public Vector2 animOrigin;

        // Bullet. Shooting
        public Texture2D bulletTexture;
        public int bulletDelay;
        public List<Bullet> bulletList = new List<Bullet>();

        // Health, HUD
        public Rectangle healthRectangle;
        public Texture2D healthTexture;
        public Vector2 healthPosition;
        //
        public SpriteFont font;
        public static Vector2 playerScorePos;

        public PlayerShip()
        {
            playerScorePos = new Vector2(996, 50);
            healthPosition = new Vector2(50, 50);
            health = 200;
            texture = null;
            position = new Vector2(100, 400);
            speed = 7;
            //animation
            animPos = new Vector2(5, 400);
            currentFrame = 1;
            animationCounter = 10;
            spriteWidth = 113;
            spriteHeight = 49;
            animRect = new Rectangle(0, currentFrame * spriteHeight, spriteWidth, spriteHeight);
            animOrigin = new Vector2(spriteWidth / 2, spriteHeight / 2);
            //bullet. shooting
            bulletDelay = 0;

        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("level2/playership_little");
            animationTexture = Content.Load<Texture2D>("level2/blue fire");
            bulletTexture = Content.Load<Texture2D>("level2/little_bullet");
            healthTexture = Content.Load<Texture2D>("health");
            healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, healthTexture.Height);
            font = Content.Load<SpriteFont>("myFont");
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); // updating players boundingbox
            healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, healthTexture.Height);

            Movement();
            ShipLightAnimation();
            ScreenBorders();
            Shoot();
            ManageBullets();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationTexture, animPos, animRect, Color.White, 0f, animOrigin, 1.0f, SpriteEffects.None, 0);
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            spriteBatch.DrawString(font, "Score - " + HUD.playerScore, playerScorePos, Color.Yellow);
          
        }

        // # ##Methods## #

        // Player Controls

        private void Movement()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                position.Y += speed;

            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }
        }

        private void ScreenBorders()
        {
            // Keep playership on screen vision
            if (position.X <= 0) position.X = 0;
            if (position.X + texture.Width >= 1280) position.X = 1280 - texture.Width;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y + texture.Height >= 720) position.Y = 720 - texture.Height;

        }

        // # Animation

        private void ShipLightAnimation()
        {
            animationCounter--;

            if (animationCounter <= 0)
            {
                currentFrame++;
                animationCounter = 10;
            }

            if (currentFrame > 5)
                currentFrame = 1;

            animPos = new Vector2(position.X - texture.Width/2, position.Y + (texture.Height/2)-10);
            animRect = new Rectangle(0, currentFrame * spriteHeight, spriteWidth, spriteHeight);

            if (currentFrame == 3)
            {
                animPos = new Vector2(position.X - texture.Width / 2, position.Y + (texture.Height / 2));
                animRect = new Rectangle(0, (currentFrame * spriteHeight) + 10, spriteWidth, spriteHeight);
            }
            if (currentFrame == 4)
            {
                animPos = new Vector2(position.X - texture.Width / 2, position.Y + (texture.Height / 2)-4);
                animRect = new Rectangle(0, (currentFrame * spriteHeight) + 6, spriteWidth, spriteHeight);
            }
            if (currentFrame == 5)
            {
                animPos = new Vector2(position.X - texture.Width / 2, position.Y + (texture.Height / 2)-4);
                animRect = new Rectangle(0, (currentFrame * spriteHeight) + 6, spriteWidth, spriteHeight);
            }

        }

        // # Bullets. Shooting

        private void Shoot()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (bulletDelay >= 0)
                    bulletDelay--;

                if (bulletDelay <= 0)
                {
                    Bullet newBullet = new Bullet(bulletTexture);
                    newBullet.position = new Vector2((int)position.X + (texture.Width / 2) - 10, (int)position.Y + (texture.Height / 2) - 3);
                    newBullet.isVisible = true;
                    newBullet.speed = 12;

                    if (bulletList.Count < 20)
                    {
                        bulletList.Add(newBullet);
                        SoundManager.playerShoot.Play(volume: SoundManager.effectsVolume, pitch: 0.0f, pan: 0.0f);
                    }
                }

                if (bulletDelay <= 0)
                    bulletDelay = 12;
            }
        }

        private void ManageBullets()
        {

            foreach (Bullet bullet in bulletList)
            {
                bullet.position.X += bullet.speed;
                bullet.boundingBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bullet.texture.Width, bullet.texture.Height);

                if (bullet.position.X >= 1281)
                    bullet.isVisible = false;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].isVisible == false)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

        }
    }
}
