﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using _2D_StarWars_Fighter.enemies;

namespace _2D_StarWars_Fighter
{
    class Level3
    {
     
        // HUD
        HUD hud = new HUD();
        // background
        private Texture2D[] bgTexture;
        private Vector2 bgPosition;
        private Player player = new Player();
        // ### Enemies
        // scorpion
        private List<Scorpion> scorpionList = new List<Scorpion>();
        public Texture2D[] walkTextures = new Texture2D[3];
        public Texture2D[] attackTextures = new Texture2D[9];
        private Texture2D[] sandTextures = new Texture2D[8];
        private int scorpionsDelay;
        // Explosions Lists
        private List<SandExplosion> sandExplosionList = new List<SandExplosion>();
        // walker
        private List<Walker> walkerList = new List<Walker>();
        private Texture2D[] walkerWalkTextures = new Texture2D[7];
        private Texture2D walkerStandTexture;
        private List<Bullet> walkerBulletList = new List<Bullet>();
        private Texture2D walkerBulletTexture;
        private int bulletDelay;
        private Texture2D[] hitTextures = new Texture2D[3];
        private List<Hit> hitExplosions = new List<Hit>();
        // explosions
        private Texture2D explosionTexture;
        List<Level2Explosions> explosionsList = new List<Level2Explosions>();
        // BigMachine
        private Texture2D stand, walk1, walk2;
        List<BigMachine> bigMachineList = new List<BigMachine>();
        // droid
        private Texture2D costume1, costume2, blaster_bolt;
        private List<GonkDroid> droidList = new List<GonkDroid>();
        private int destroyedDroidsCount;
        //
        private List<DestroyedGonkDroid> destroyedDroidList = new List<DestroyedGonkDroid>();
        private Texture2D costume5; // destoyed void texture
        private Texture2D[] droidDestroySpriteList = new Texture2D[5];
        private List<DroidDesAnimation> spritesGonkDroidList = new List<DroidDesAnimation>();
        // Imperial
        private Texture2D stand1, stand2, kneel1, kneel2, imperial_blaster;
        private List<Imperial> imperialList = new List<Imperial>();
        private List<Bullet> imperialBulletList = new List<Bullet>();
        private int impBulletCounter;


        public Level3()
        {
            impBulletCounter = 35;
            destroyedDroidsCount = 0;
            bulletDelay = 15;
            scorpionsDelay = 20;
            player.healthEndX = 13450;
            player.screenBoundsEnd = 14680;
       //     player.position = new Vector2(13000, 0);
            player.EndX = 13810;
            player.EndX2 = 13400;
            bgPosition = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            // imperial
            kneel1 = Content.Load<Texture2D>("level3/imperial/kneel1");
            kneel2 = Content.Load<Texture2D>("level3/imperial/kneel2");
            stand1 = Content.Load<Texture2D>("level3/imperial/stand1");
            stand2 = Content.Load<Texture2D>("level3/imperial/stand2");
            imperial_blaster = Content.Load<Texture2D>("level3/imperial/costume1");
            //
            for (int i = 0; i < 5; i++)
            {
                Texture2D des = Content.Load<Texture2D>("level3/droidDestSprites/des" + (i+1).ToString());
                droidDestroySpriteList[i] = des;
            }
            costume5 = Content.Load<Texture2D>("level3/droid/costume5");
            // droid
            costume1 = Content.Load<Texture2D>("level3/droid/costume1");
            costume2 = Content.Load<Texture2D>("level3/droid/costume2");
            blaster_bolt = Content.Load<Texture2D>("level3/droid/blaster bolt");
            // big machine
            stand = Content.Load<Texture2D>("bigmachine/stand");
            walk1 = Content.Load<Texture2D>("bigmachine/walk1");
            walk2 = Content.Load<Texture2D>("bigmachine/walk2");
            // explosions
            explosionTexture = Content.Load<Texture2D>("level2/Spaceship_Explosion_Spritesheet");
            // background
            bgTexture = new Texture2D[12];
            for (int i = 0; i < 12; i++)
            {
                bgTexture[i] = Content.Load<Texture2D>("level3/background/image_part_" + (i+1).ToString());
            }
            // Player Content
            player.LoadContent(Content);
            // Scorpion Content
            for (int i = 0; i < walkTextures.Length; i++)
            {
                walkTextures[i] = Content.Load<Texture2D>("level3/scorpion/scorpion_walk" + (i + 1).ToString());
            }
            // Scorpion Content
            for (int i = 0; i < attackTextures.Length; i++)
            {
                attackTextures[i] = Content.Load<Texture2D>("level3/scorpion/scorpion" + (i + 1).ToString());
            }
            // Sand Explosions Content

            for (int i = 0; i < sandTextures.Length; i++)
            {
                sandTextures[i] = Content.Load<Texture2D>("level3/sand explosions/sand" + (i + 1).ToString());
            }

            // # WALKER
            for (int i = 0; i < walkerWalkTextures.Length; i++)
            {
                walkerWalkTextures[i] = Content.Load<Texture2D>("level3/Walker/walk" + (i + 1).ToString());
            }
            walkerStandTexture = Content.Load<Texture2D>("level3/Walker/stand");

            // Walker Bullets
            walkerBulletTexture = Content.Load<Texture2D>("level3/red blast/costume1");
            for (int i = 0; i < hitTextures.Length; i++)
            {
                hitTextures[i] = Content.Load<Texture2D>("level3/red blast/blocked" + (i + 1).ToString());
            }
            // hud
            hud.LoadContent(Content, player);

        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            SpawnScorpions();
            SpawnWalkers();
            SpawnDroids();
            SpawnImperials();
            foreach (Scorpion s in scorpionList)
            {
                s.Update(gameTime);
            }

            // immperial
            foreach (Imperial imp in imperialList)
            {
                imp.Update(gameTime);
                // imperial attack
                if (imp.isAttack)
                {
                    if (impBulletCounter > 0)
                        impBulletCounter--;

                    if (impBulletCounter <= 0)
                    {
                        //
                        Bullet bullet = new Bullet(imperial_blaster);
                        bullet.isVisible = true;
                        if (imp.spriteEffect == SpriteEffects.None)
                        {
                            bullet.isLeft = true;
                            bullet.position = new Vector2(imp.position.X, imp.position.Y + (imp.texture.Height / 2) - 10);
                        }
                        else
                            if (imp.spriteEffect == SpriteEffects.FlipHorizontally)
                            {
                                bullet.isRight = true;
                                bullet.position = new Vector2(imp.position.X + 30, imp.position.Y + (imp.texture.Height / 2) - 10);
                            }
                        imperialBulletList.Add(bullet);
                    }

                    if (impBulletCounter <= 0)
                        impBulletCounter = 35;

                }
            }

            Collisions();

            foreach (SandExplosion sandExp in sandExplosionList)
            {
                sandExp.Update(gameTime);
            }

            foreach (Walker w in walkerList)
            {
                w.Update(gameTime);
                if (w.isAttackingAnimation)
                {
                    WalkerCreateBullets(w);
                }

                // walker dies
                if (w.boundingBox.Intersects(player.boundingBox) && player.isAttacking)  // collision
                {
                    w.isVisible = false;
                    Level2Explosions explosionObject = new Level2Explosions(new Vector2(w.position.X + (w.texture.Width/2), w.position.Y + (w.texture.Height/2)), explosionTexture);
                    explosionObject.isVisible = true;
                    explosionsList.Add(explosionObject);
                    HUD.playerScore += 40;
                }
            }

            foreach (Hit h in hitExplosions)
            {
                h.Update(gameTime);
            }

            // Droids
            foreach(GonkDroid gd in droidList)
            {
                gd.Update(gameTime);
            }

            foreach(DroidDesAnimation dda in spritesGonkDroidList)
            {
                dda.Update(gameTime);
            }

            // ManageWalkers();
            ManageScorpions();
            ManageExplosions();
            WalkerManageBullets();
            HitManage();
            ManageWalkers();
            ManageDroids();
            ManageDroidSprites();
            ManageImperialBullets();

            foreach (Level2Explosions explosion in explosionsList)
            {
                explosion.Update(gameTime);
            }
            ManageExplosionsBoom();
            hud.Update(gameTime, player);
            //
            foreach (BigMachine bm in bigMachineList)
            {
                bm.Update(gameTime);
            }
            //
            BigMachineSpawn();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // background
            spriteBatch.Draw(bgTexture[0], new Vector2(0, 0), Color.White);
            for (int i = 1; i < 12; i++)
            {
                spriteBatch.Draw(bgTexture[i], new Vector2(1333 * i, 0), Color.White);
            }
            // 
            // big machine
            foreach (BigMachine bm in bigMachineList)
            {
                bm.Draw(spriteBatch);
            }
            //
            // explosion
            foreach (SandExplosion sandExp in sandExplosionList)
            {
                sandExp.Draw(spriteBatch);
            }
            // Walker Bullet
            foreach (Bullet b in walkerBulletList)
            {
                b.Draw(spriteBatch);
            }
            // Imperial Bullet
            foreach (Bullet b in imperialBulletList)
            {
                b.Draw(spriteBatch);
            }
            foreach (GonkDroid gd in droidList)
            {
                gd.Draw(spriteBatch);
            }
            foreach (DroidDesAnimation dda in spritesGonkDroidList)
            {
                dda.Draw(spriteBatch);
            }
            foreach (DestroyedGonkDroid dgd in destroyedDroidList)
            {
                dgd.Draw(spriteBatch);
            }
            // player
            player.Draw(spriteBatch);
            //imperials
            foreach (Imperial imp in imperialList)
            {
                imp.Draw(spriteBatch);
            }
            // scorpions
            foreach (Scorpion s in scorpionList)
            {
                s.Draw(spriteBatch);
            }
            // walkers
            foreach (Walker w in walkerList)
            {
                w.Draw(spriteBatch);
            }
            //
            foreach (Hit h in hitExplosions)
            {
                h.Draw(spriteBatch);
            }
            // explosions
            foreach (Level2Explosions explosion in explosionsList)
            {
                explosion.Draw(spriteBatch);
            }
            hud.Draw(spriteBatch);
        }

        public Player GetPlayer()
        {
            return player;
        }

        private void SpawnDroids()
        {
            if (droidList.Count == 0 && destroyedDroidsCount == 0)
            {
              //  GonkDroid droid = new GonkDroid(costume1, costume2, blaster_bolt, new Vector2(7500, 680), player, hitExplosions, hitTextures);
                GonkDroid droid = new GonkDroid(costume1, costume2, blaster_bolt, new Vector2(1000, 680), player, hitExplosions, hitTextures, scorpionList, sandExplosionList, sandTextures);
                droid.isRight = true;
                droid.spriteEffect = SpriteEffects.FlipHorizontally;
                droid.IsVisible = true;
                droidList.Add(droid);

                // second droid
                GonkDroid droid2 = new GonkDroid(costume1, costume2, blaster_bolt, new Vector2(7500, 680), player, hitExplosions, hitTextures, scorpionList, sandExplosionList, sandTextures);
                droid2.IsVisible = true;
                droidList.Add(droid2);
            }
        }

        private void SpawnWalkers()
        {
            if (player.position.X >= 2000 && player.position.X <= 6500)
            {
                Random rand = new Random();
                int x, y;
                x = rand.Next((int)(player.position.X + 1000), (int)(player.position.X + 7000));
                y = 440;
                if (walkerList.Count < 4)
                {
                    Walker walker = new Walker(walkerWalkTextures, walkerStandTexture, new Vector2(x, y), player) { isVisible = true };
                    walkerList.Add(walker);
                }
            }
        }

        private void SpawnScorpions()
        {
            if (scorpionsDelay > 0)
                scorpionsDelay--;

            if (scorpionsDelay <= 0)
            {
                if (player.position.X <= 2000)
                {
                    Random rand = new Random();
                    int x = rand.Next(600, 4900);
                    if (scorpionList.Count < 14 && (rand.Next(0, 1000)) > 300)
                    {
                        if (scorpionList.Count >= 1)
                        {
                            if (x == scorpionList[0].position.X)
                            {
                                x = rand.Next(600, 3000);
                            }
                        }

                        Scorpion scorp = new Scorpion(new Vector2(player.position.X + x, 635), player, walkTextures, attackTextures) { isVisible = true };
                        scorpionList.Add(scorp);
                    }
                    else if (scorpionList.Count < 14 && (rand.Next(0, 1000)) < 450)
                    {
                        if (scorpionList.Count >= 1)
                        {
                            if (x == scorpionList[0].position.X)
                            {
                                x = rand.Next(600, 3000);
                            }
                        }

                        Scorpion scorp = new Scorpion(new Vector2(player.position.X - x, 635), player, walkTextures, attackTextures) { isVisible = true };
                        scorpionList.Add(scorp);
                    }
                }

                if (scorpionsDelay <= 0)
                    scorpionsDelay = 20;
            }
        }

        private void ManageScorpions()
        {
            if (scorpionList.Count != 0)
            {
                for (int i = 0; i < scorpionList.Count; i++)
                {
                    if (scorpionList[i].isVisible == false)
                    {
                        scorpionList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void Collisions()
        {
            // Scorp and Player
            foreach (Scorpion s in scorpionList)
            {
                if (player.isAttacking && player.boundingBox.Intersects(s.boundingBox))
                {
                    s.isVisible = false;
                    HUD.playerScore += 10;
                    SandExplosion explosion = new SandExplosion(new Vector2(s.position.X, s.position.Y+20), sandTextures) { isVisible = true };
                    sandExplosionList.Add(explosion);
                }
                if (s.boundingBox.Intersects(player.boundingBox) && s.isAttacks)
                {
                    if (player.isDefending == false)
                    {
                        player.health -= 1;
                    }
                    else if (player.isDefending && player.spriteEffect != s.spriteEffect)
                    {
                        player.health -= 1;
                    }
                }
            }

        }

        private void ManageExplosions()
        {
            if (sandExplosionList.Count != 0)
            {
                for (int i = 0; i < sandExplosionList.Count; i++)
                {
                    if (sandExplosionList[i].isVisible == false)
                    {
                        sandExplosionList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void ManageDroids()
        {
            if(droidList.Count != 0)
            {
                for (int i = 0; i < droidList.Count; i++)
                {
                    if (droidList[i].IsVisible == false)
                    {
                        // sprites. animation of destroy
                        DroidDesAnimation dda = new DroidDesAnimation(droidDestroySpriteList, droidList[i].position);
                        dda.isVisible = true;
                        spritesGonkDroidList.Add(dda);
                        // ours droid removing and setting a destroyed texture. making ++ for count of droid death to not spawn him second time.
                        DestroyedGonkDroid destroyedDroid = new DestroyedGonkDroid(costume5, droidList[i].position) { isVisible = true };
                        destroyedDroidList.Add(destroyedDroid);
                        droidList.RemoveAt(i);
                        i--;
                        destroyedDroidsCount++;
                    }
                }
            }
        }

        private void ManageDroidSprites()
        {
            if (spritesGonkDroidList.Count != 0)
            {
                for (int i = 0; i < spritesGonkDroidList.Count; i++)
                {
                    if (spritesGonkDroidList[i].isVisible == false)
                    {
                        spritesGonkDroidList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void WalkerCreateBullets(Walker w)
        {
            if (walkerBulletList.Count <= 20)
            {
                if (bulletDelay > 0)
                    bulletDelay--;

                if (bulletDelay <= 0)
                {
                    Bullet bullet = new Bullet(walkerBulletTexture) { isVisible = true };
                    if (w.spriteEffect == SpriteEffects.FlipHorizontally)
                    bullet.position = new Vector2(w.position.X, w.position.Y - 40 + (w.texture.Height/2));
                    if(w.spriteEffect == SpriteEffects.None)
                    bullet.position = new Vector2(w.position.X + w.texture.Width, w.position.Y - 40 + (w.texture.Height / 2));
                    bullet.boundingBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bullet.texture.Width, bullet.texture.Height);
                    bullet.walkerRef = w;
                    if (w.spriteEffect == SpriteEffects.None)
                        bullet.isRight = true;
                    else
                        if (w.spriteEffect == SpriteEffects.FlipHorizontally)
                            bullet.isLeft = true;
                    walkerBulletList.Add(bullet);
                }
                if (bulletDelay <= 0)
                    bulletDelay = 30;

            }

        }

        private void WalkerManageBullets()
        {
                foreach (Bullet b in walkerBulletList)
                {
                    if (b.isLeft)
                    {
                    // bullet traectory
                  //  b.position.X -= b.speed;

                        b.position.X -= b.speed;
                        if (b.step == 0)
                        {
                            b.step = (int)(b.walkerRef.position.X - player.position.X) / (int)(player.position.Y - b.walkerRef.position.Y);
                        }
                        b.position.Y += b.step;
                    //
                    }
                    else if (b.isRight)
                    {
                        if (b.step == 0)
                        {
                            b.step = (int)(b.walkerRef.position.X - player.position.X) / (int)(player.position.Y - b.walkerRef.position.Y);
                            b.step -= 4;
                        }
                        b.position.X += b.speed;
                        b.position.Y -= b.step;
                    }
                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                    // bullet destroy
                    if (b.position.Y > 800)
                        b.isVisible = false;

                    if (b.boundingBox.Intersects(player.boundingBox))
                    {
                        b.isVisible = false;

                        // damage
                        if (player.isDefending)
                        {
                            if (player.spriteEffect == SpriteEffects.None && b.isRight == true)
                            {
                                player.health -= 35;
                            }
                            else if (player.spriteEffect == SpriteEffects.FlipHorizontally && b.isLeft == true)
                            {
                                player.health -= 35;
                            }
                        }
                        else
                        {
                            player.health -= 35; // COLLISION
                        }
                      //  Hit hit = new Hit(hitTextures, new Vector2(player.position.X + (player.texture.Width/2), player.position.Y + (player.texture.Height/2) ));
                        Hit hit = new Hit(hitTextures, player.position);
                        hit.isVisible = true;
                        hitExplosions.Add(hit);
                    }
                }

            for (int i = 0; i < walkerBulletList.Count; i++)
            {
                if (walkerBulletList[i].isVisible == false)
                {
                    walkerBulletList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void HitManage()
        {
            if (hitExplosions.Count != 0)
            {
                for (int i = 0; i < hitExplosions.Count; i++)
                {
                    if (hitExplosions[i].isVisible == false)
                    {
                        hitExplosions.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        // imperial

        private void SpawnImperials()
        {
            if (imperialList.Count <= 1)
            {
                Imperial imp = new Imperial(stand1, stand2, kneel1, kneel2, new Vector2(8000, 640), player);
                imp.isVisible = true;
                imperialList.Add(imp);
            }
        }

        private void ManageImperialBullets()
        {
            if (imperialBulletList.Count != 0)
            {
                foreach (Bullet b in imperialBulletList)
                {
                    if (b.isLeft)
                    {
                        b.position.X -= b.speed;
                    }
                    else if (b.isRight)
                    {
                        b.position.X += b.speed;
                    }

                    b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                    if (b.isLeft)
                    {
                        if (b.position.X < 6800)
                        {
                            b.isVisible = false;
                        }
                    }
                    else if (b.isRight)
                    {
                        if (b.position.X > 16000)
                        {
                            b.isVisible = false;
                        }
                    }
                }

                for (int i = 0; i < imperialBulletList.Count; i++)
                {
                    if (imperialBulletList[i].isVisible == false)
                    {
                        imperialBulletList.RemoveAt(i);
                        i--;
                    }
                }

            }
        }

        // Explosions
        private void ManageExplosionsBoom()
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

        private void ManageWalkers()
        {
            if (walkerList.Count != 0)
            {
                for (int i = 0; i < walkerList.Count; i++)
                {
                    if (walkerList[i].isVisible == false)
                    {
                        walkerList.RemoveAt(i);
                            i--;
                    }
                }
            }
        }

        private void BigMachineSpawn()
        {
            if (bigMachineList.Count <= 1)
            {
                BigMachine bm = new BigMachine(stand, walk1, walk2, new Vector2(9500, 190));
                bm.spriteEffect = SpriteEffects.None;
                bm.isVisible = true;

                BigMachine bm2 = new BigMachine(stand, walk1, walk2, new Vector2(1900, 190));
                bm2.spriteEffect = SpriteEffects.None;
                bm2.isVisible = true;
                bigMachineList.Add(bm);
                bigMachineList.Add(bm2);
            }
        }
    }
}
