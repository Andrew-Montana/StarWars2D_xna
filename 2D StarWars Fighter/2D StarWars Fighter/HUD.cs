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
    public class HUD
    {
        static public int playerScore, screenWidth, screenHeight;
        public SpriteFont playerScoreFont;
        public static Vector2 playerScorePos;   // for 1 player because its static
        public bool showHud;

        // Constructor
        public HUD()
        {
            playerScore = 0;
            showHud = true;
            screenHeight = 720;
            screenWidth = 1280;
            playerScoreFont = null;
          //  playerScorePos = new Vector2((screenWidth-200), 50);
        }

        // Load Content
        public void LoadContent(ContentManager Content, Player p)
        {
            playerScoreFont = Content.Load<SpriteFont>("myFont");
            playerScorePos = new Vector2(p.position.X + 800, 50);
        }

        // Update
        public void Update(GameTime gameTime, Player p)
        {
            // Hold HUD position in beginning fixed
            if (p.position.X <= 400)
                playerScorePos = new Vector2(996, 50);
            // Bind HUD position to player position
            if (p.position.X >= 401 && p.position.X <= 9463)
            playerScorePos = new Vector2(p.position.X + 596, 50);

            // Hold HUD position at the End fixed
            if (p.isEndPosition) 
                playerScorePos = new Vector2(10352, 50);
            
        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            // If we are showing our HUD ( if showHud == true ) then display the HUD
            if (showHud)
                spriteBatch.DrawString(playerScoreFont, "Score - " + playerScore, playerScorePos, Color.Yellow);
        }


    }
}
