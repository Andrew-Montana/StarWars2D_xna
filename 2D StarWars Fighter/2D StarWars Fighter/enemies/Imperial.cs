using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace _2D_StarWars_Fighter.enemies
{
    class Imperial
    {
        public Texture2D texture, stand1, stand2, kneel1, kneel2;
        public Vector2 position;
        public bool isVisible, isAttack, isMove, isPlayerDetected, isPlayerConfines;
        public Rectangle boundingBox;
        public SpriteEffects spriteEffect; // looking left is - None ( custom )
     //   public int confines1,// confines2; // confines of imperial movement. 8000-10000 on X for example
        public Player playerRef;

        public Imperial(Texture2D stand1, Texture2D stand2, Texture2D kneel1, Texture2D kneel2, Vector2 newPosition, Player player)
        {
            this.stand1 = stand1;
            this.stand2 = stand2;
            this.kneel1 = kneel1;
            this.kneel2 = kneel2;
            texture = this.stand1;
            position = newPosition;
            playerRef = player;
            spriteEffect = SpriteEffects.None;
        //    confines1 = 8000;
          //  confines2 = 10000;

        }

        public void Update(GameTime gameTime)
        {
            PlayerDetection();
            CheckDistance();
            Movement();
          //  Rotate();
            Attack();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0F, new Vector2(0, 0), 1F, spriteEffect, 0);
        }

        // #

        private void PlayerDetection()
        {
            if (playerRef.position.X > 8000)
            {
                // если игрок заходит в границы 
                isPlayerConfines = true;
            }
            else if (playerRef.position.X < 8000)
            {
                // если игрок вне границ
                isPlayerConfines = false;
            }
        }

        private void CheckDistance()
        {
            if (isPlayerConfines)
            {
                // если позиция игрока меньше чем позиция объекта
                if (playerRef.position.X < position.X)
                {
                    int result = (int)position.X - (int)playerRef.position.X;
                    if (result <= 750)
                    {
                        isPlayerDetected = true;
                        spriteEffect = SpriteEffects.None;
                    }
                }// если позиция игрока по X больше, чем у объекта
                else if (playerRef.position.X > position.X)
                {
                    int result = (int)playerRef.position.X - (int)position.X;
                    if (result <= 750)
                    {
                        isPlayerDetected = true;
                        spriteEffect = SpriteEffects.FlipHorizontally;
                    }
                }

            }
            else if (isPlayerConfines == false)
            {
                isPlayerDetected = false;
            }
        }

        private void Movement()
        {
            if (isPlayerDetected)
            {

            }
        }

        private void Attack()
        {

        }


    }
}
