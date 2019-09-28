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

        public static int DEFAULT_TIME_PLAYER_FIRE = 2000;
        const int DEFAULT_UPDATE_TIME = 3000;
        const int DEFAULT_ATTACK_TIMER = 5000;
        const int DEFAULT_ALIEN_VALUE_START = 9;
        const int DEFAULT_WALL_NUMBER = 4;
        public const int MAX_COLUMNS_X = 9;


        Player player;
        Thread MusicThread;
        List<Lazer> ListLazerPlayer = new List<Lazer>();
        List<Lazer> ListLazerAlien = new List<Lazer>();
        List<Alien> ListAlien = new List<Alien>();
        List<BrickWall> ListWall = new List<BrickWall>();
        Stopwatch playerFire = new Stopwatch();
        Stopwatch updateTimer = new Stopwatch();
        Stopwatch alienAttack = new Stopwatch();
        Bonus playerBonus;
        static Random rng = new Random();
        bool bonusPlayer = false;
        int _niveau = 1;

        public int Niveau { get => _niveau; set => _niveau = value; }

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


        private int Randomise(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
        private void ControlKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (playerFire.ElapsedMilliseconds < DEFAULT_TIME_PLAYER_FIRE)
                        break;
                    ListLazerPlayer.Add(player.Fire(this.CreateGraphics()));
                    JukeBox.PlayerLazerSounds();
                    playerFire.Restart();
                    break;
                case Keys.Right:
                    player.Direction = 2;
                    //player.MoveRight();
                    break;
                case Keys.Left:
                    player.Direction = 1;
                    //player.MoveLeft();
                    break;
                case Keys.Space:
                    player.FireBonus(this.playerBonus);
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
            UpdateUI();
            playerFire.Start();
            updateTimer.Start();
            alienAttack.Start();
            for (int i = 0; i < DEFAULT_WALL_NUMBER; i++)
            {
                ListWall.Add(new BrickWall(new Point((i + 1) * 90, 300)));
            }
            for (int i = 0; i < DEFAULT_ALIEN_VALUE_START; i++)
            {
                Alien tmp = new Alien(i, 1);
                ListAlien.Add(tmp);
            }
            JukeBox.BackgroundMusic();
        }

        private void OnDraw(object sender, PaintEventArgs e)
        {
            player.Draw(e);
            UpdateUI();
            ListWall.ForEach((wall) => { wall.Draw(e); });
            ListAlien.ForEach((al) => al.Draw(e));
            if (ListLazerPlayer.Count > 0)
            {
                ListLazerPlayer.ForEach((laz) => laz.Draw(e));
            }
            if (ListLazerAlien.Count > 0)
            {
                ListLazerAlien.ForEach((laz => laz.Draw(e)));
            }
            if (bonusPlayer)
            {
                playerBonus.Draw(e);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            
            if (updateTimer.ElapsedMilliseconds >= DEFAULT_UPDATE_TIME)
            {
                
                ListAlien.Reverse();
                ListAlien.ForEach((al) => { al.Move(); });
                updateTimer.Restart();

            }
            switch (player.Direction)
            {
                case 0:
                    break;
                case 1:
                    this.player.MoveLeft();
                    break;
                case 2:
                    this.player.MoveRight(2,this.Width);
                    break;
                default:
                    break;
            }
            if (ListLazerPlayer.Count > 0)
            {
                ListLazerPlayer.ForEach((laz) =>
                {
                    ListWall.ForEach((wall) => { if (wall.InteractWithLazer(laz)) { ListLazerPlayer.Remove(laz); } });
                    List<Alien> destoyAlien = laz.LaserDestroyAlien(ListAlien);
                    laz.Move();
                    destoyAlien.ForEach((al) =>
                    {
                        if (al.IsDestroyByLaz(laz))
                        {
                            al.Destroy();
                            JukeBox.Explose();
                            player.UpScore(al.ScoreValue);
                            if (this.player.BonusValue >= 5)
                            {
                                bonusPlayer = true;
                            }
                            if (bonusPlayer)
                            {
                                playerBonus = new Bonus(Randomise(1, 3), new Point(0, this.player.Pos.Y));
                                this.player.BonusValue = 0;
                            }
                        }
                        else
                        {
                            al.LifePoint -= (uint)laz.Damage;
                        }
                    });
                });

            }
            
            if (ListLazerAlien.Count > 0)
            {
                ListLazerAlien.ForEach((laz) =>
                {
                    ListWall.ForEach((wall) => 
                    { 
                        if (wall.InteractWithLazer(laz)) 
                        {
                            ListLazerAlien.Remove(laz);
                        } 
                    });
                    laz.Move();
                    if (laz.LaserDestroyPlayer(this.player))
                    {
                        this.player.LifeDownPlayer(laz.Damage);
                        ListLazerAlien.Remove(laz);
                    }
                });
            }
            ListLazerAlien.ForEach((laz) => { if (!laz.Alive) ListLazerAlien.Remove(laz); });
            ListLazerPlayer.ForEach((laz) => { if (!laz.Alive) ListLazerPlayer.Remove(laz); });
            ListAlien.ForEach((al) => { if (!al.Alive && al.DetahStep >= 3) ListAlien.Remove(al); });
            if (alienAttack.ElapsedMilliseconds >= DEFAULT_ATTACK_TIMER)
            {
                if (Randomise(1, 10) == 10)
                {
                    try
                    {
                        ListLazerAlien.Add(ListAlien[Randomise(0, Randomise(0, ListAlien.Count - 1))].Fire());
                        JukeBox.AlienLazerSounds();
                        alienAttack.Restart();
                    }
                    catch (Exception)
                    {
                        alienAttack.Restart();
                    }
                    
                }
            }

            this.Invalidate();
        }

        private void UpdateUI()
        {
            this.lblLife.Text = $"PV : {player.LifePoint}  Niv : {this.Niveau}";
        }

        private void OnQuit(object sender, EventArgs e)
        {
            JukeBox.Dispose();
        }
    }
}
