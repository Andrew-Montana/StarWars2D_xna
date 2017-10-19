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
    public class SoundManager
    {
        public SoundEffect enemyShootSound;
        public SoundEffect explodeSound;
        static public SoundEffect firepower;
        static public SoundEffect there_is_one;
        static public SoundEffect stop_that_blasthim;
        static public SoundEffect boss_start_level1;
        static public SoundEffect boss1_attack;
        static public SoundEffect boss1_death;
        static public SoundEffect boss1_boom;
        public Song bgMusic;
        static public Song bgMusic2_level1;
        static public Song mainthemeMusic;
        static public float musicVolume { get; set; }
        static public float effectsVolume { get; set; }

        // level 2
        static public SoundEffect endscene1;
        static public SoundEffect enemyShoot;
        static public SoundEffect justBoom;
        static public SoundEffect playerShoot;
        static public Song level2music;

        //# level 3
        static public SoundEffect scorp_attack;
        static public SoundEffect scorp_acklay;
        static public SoundEffect scorp_sand;
        static public SoundEffect gonkdroid_shoot;
        static public SoundEffect gonkdroid_explode;
        static public SoundEffect gonkdroid_work;
        static public SoundEffect walker_walk;
        static public SoundEffect walker_shoot;
        static public SoundEffect walker_boom;
        static public SoundEffect imp_talk;
        static public SoundEffect imp_shoot;
        static public SoundEffect imp_death;
        static public Song boss_3level;

        // Constructor
        public SoundManager()
        {
            enemyShootSound = null;
            explodeSound = null;
            bgMusic = null;

        }

        public void LoadContent(ContentManager Content)
        {
            // # 3 level 
         //   boss_3level = Content.Load<Song>("level3/sound/Star Wars Duel Of The Fates");
            boss_3level = Content.Load<Song>("level3/sound/Star Wars The Sith Spacecraft");
            scorp_attack = Content.Load<SoundEffect>("level3/sound/scorpion/attack");
            scorp_acklay = Content.Load<SoundEffect>("level3/sound/scorpion/acklay");
            scorp_sand = Content.Load<SoundEffect>("level3/sound/scorpion/sand");
            gonkdroid_shoot = Content.Load<SoundEffect>("level3/sound/gonk/shoot2");
            gonkdroid_explode = Content.Load<SoundEffect>("level3/sound/gonk/explode");
            gonkdroid_work = Content.Load<SoundEffect>("level3/sound/gonk/working");
            walker_walk = Content.Load<SoundEffect>("level3/sound/walker/walk");
            walker_shoot = Content.Load<SoundEffect>("level3/sound/walker/shoot");
            walker_boom = Content.Load<SoundEffect>("level3/sound/walker/boom");
            imp_talk = Content.Load<SoundEffect>("level3/sound/imp/talk");
            imp_shoot = Content.Load<SoundEffect>("level3/sound/imp/shoot");
            imp_death = Content.Load<SoundEffect>("level3/sound/imp/death");
            // #
            enemyShootSound = Content.Load<SoundEffect>("sound/blaster");
            explodeSound = Content.Load<SoundEffect>("sound/enemydeath");
            bgMusic = Content.Load<Song>("sound/background_theme1");
            firepower = Content.Load<SoundEffect>("firepower");
            there_is_one = Content.Load<SoundEffect>("thereisone");
            stop_that_blasthim = Content.Load<SoundEffect>("stop_that_blast");
            boss_start_level1 = Content.Load<SoundEffect>("boss_start_level1");
            boss1_attack = Content.Load<SoundEffect>("boss1_attack");
            bgMusic2_level1 = Content.Load<Song>("background2_level1_");
            boss1_death = Content.Load<SoundEffect>("boss1_boom_death");
            boss1_boom = Content.Load<SoundEffect>("boss1_boom");
            mainthemeMusic = Content.Load<Song>("Star Wars Theme");
            //
            enemyShoot = Content.Load<SoundEffect>("level2/enemyShoot");
            justBoom = Content.Load<SoundEffect>("level2/boom");
            playerShoot = Content.Load<SoundEffect>("level2/playerShoot");
            endscene1 = Content.Load<SoundEffect>("level2/endscene1");
            level2music = Content.Load<Song>("level2/Epic space battle music - Imperia");
        }

    }
}
