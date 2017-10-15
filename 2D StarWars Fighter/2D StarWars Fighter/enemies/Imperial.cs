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
        public bool isVisible, isAttack, isMove, isPlayerDetected;
        public Rectangle boundingBox;
        public SpriteEffects spriteEffect; // looking left is - None ( custom )
        public int confines; // confines of imperial movement. 8000-10000 on X for example

        public Imperial()
        {

        }

        public void Update(GameTime gameTime)
        {
            PlayerDetection();
            CheckDistance();
            Movement();
            Rotate();
            Attack();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        // #

        private void PlayerDetection()
        {

        }

        private void CheckDistance()
        {

        }

        private void Movement()
        {
            // custom moving
            // ifDetected player
        }

        private void Attack()
        {

        }

        private void Rotate()
        {

        }

    }
}
