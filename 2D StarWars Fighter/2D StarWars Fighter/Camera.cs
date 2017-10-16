using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2D_StarWars_Fighter
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public bool isLevel3 { get; set; }

        public Camera(Viewport newView)
        {
            view = newView;
            isLevel3 = false;
        }

        public void Update(GameTime gameTime, Player player)
        {
            #region level1
            if (isLevel3 == false)
            {
                // Center of the screen is beginning view
                if (player.position.X <= 400 && !player.isEndPosition)
                    centre = new Vector2(0, 0);
                // Center of the screen is the player
                if (player.position.X >= 401 && player.position.X <= 9763 && !player.isEndPosition) // endX
                    centre = new Vector2(player.position.X + (90 / 2) - 440, 0);
                // Center of the screen is end view
                if (player.position.X >= 9764) // endX
                    centre = new Vector2(9363, 0); // endX 2
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
            #endregion
            #region level3
            if (isLevel3 == true)
            {
                // Center of the screen is beginning view
                if (player.position.X <= 400 && !player.isEndPosition)
                    centre = new Vector2(0, 0);
                // Center of the screen is the player
                if (player.position.X >= 401 && player.position.X <= 16000 && !player.isEndPosition)
                    centre = new Vector2(player.position.X + (90 / 2) - 440, 0);
                // Center of the screen is end view
                if (player.position.X >= 13810)
                {
                    player.isEndPosition = true;
                    centre = new Vector2(13400, 0);
                }
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
            #endregion
        }
    }
}
