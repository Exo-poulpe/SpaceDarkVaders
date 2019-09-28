using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.IO;
using System.Windows.Media;
using System.Diagnostics;

namespace SpaceDarkVaders
{
    public partial class Form1 : Form
    {

        const int DEFAULT_TIME_PLAYER_FIRE = 2000;
        const int DEFAULT_ALIEN_VALUE_START = 9;
        const int DEFAULT_WALL_NUMBER = 4;


        Player player;
        Thread MusicThread;
        List<Lazer> ListLazerPlayer = new List<Lazer>();
        List<Lazer> ListLazerAlien = new List<Lazer>();
        List<Alien> ListAlien = new List<Alien>();
        List<BrickWall> ListWall = new List<BrickWall>();
        Stopwatch playerFire = new Stopwatch();
        bool bonusPlayer = false;



        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            this.Load += InitGame;
            this.Paint += OnDraw;
            this.KeyDown += ControlKey;
            this.FormClosing += OnQuit;
            this.tmr.Tick += Update;
        }
        private void ControlKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (playerFire.ElapsedMilliseconds < DEFAULT_TIME_PLAYER_FIRE)
                        break;
                    ListLazerPlayer.Add(player.Fire(this.CreateGraphics()));
                    SoundPlayer sounds = new SoundPlayer(Properties.Resources.laser6);
                    sounds.Play();
                    playerFire.Restart();
                    break;
                case Keys.Right:
                    player.MoveRight();
                    break;
                case Keys.Left:
                    player.MoveLeft();
                    break;
                case Keys.Space:
                    bonusPlayer = false;
                    break;
                default:
                    break;
            }
            Update();
        }
        private void InitGame(object sender, EventArgs e)
        {
            player = new Player();
            playerFire.Start();
            for (int i = 0; i < DEFAULT_WALL_NUMBER; i++)
            {
                ListWall.Add(new BrickWall(new Point((i+1) * 80, 300)));
            }
            for (int i = 0; i < DEFAULT_ALIEN_VALUE_START; i++)
            {
                Alien tmp = new Alien().AlienByColumns(i, 0);
                ListAlien.Add(tmp);
            }

            MusicThread = new Thread(new ThreadStart(() =>
            {
                MediaPlayer backSound = new MediaPlayer();
                backSound.Open(new Uri(@"D:\C#\SpaceDarkVaders\res\audio\yellow.wav"));
                backSound.Play();
            }));
            MusicThread.Start();
        }

        private void OnDraw(object sender, PaintEventArgs e)
        {
            player.Draw(e);
            ListWall.ForEach((wall) => { wall.Draw(e); });
            foreach (Alien al in ListAlien)
            {
                al.Draw(e);
            }
            if (ListLazerPlayer.Count > 0)
            {
                foreach (Lazer laz in ListLazerPlayer)
                {
                    laz.Draw(e);
                }
            }
            if (bonusPlayer)
            {
                e.Graphics.DrawImage(Properties.Resources.bonus,new Point(0,this.player.Pos.Y));
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (player.BonusValue >= 5)
            {
                bonusPlayer = true;
            }
            if (ListLazerPlayer.Count > 0)
            {
                ListLazerPlayer.ForEach((laz) => 
                {
                    List<Alien> destoyAlien = laz.LaserDestroyAlien(ListAlien);
                    laz.Move();
                    destoyAlien.ForEach((al) => { al.Destroy();player.UpScore(al.ScoreValue); });
                });

                ListLazerAlien.ForEach((laz) => { if (!laz.Alive) ListLazerAlien.Remove(laz); });
                ListLazerPlayer.ForEach((laz) => { if (!laz.Alive) ListLazerPlayer.Remove(laz); });
                ListAlien.ForEach((al) => { if (!al.Alive && al.DetahStep >= 3) ListAlien.Remove(al); });
            }
            this.Invalidate();
        }

        private void OnQuit(object sender, EventArgs e)
        {

        }
    }
}
