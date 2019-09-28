using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceDarkVaders
{
    class Lazer
    {
        const int DEFAULT_SPEED_LAZER = 10;

        int _speed = DEFAULT_SPEED_LAZER;
        bool _alienOrPlayer = false;
        int _damage = 1;
        Image _image;
        Point _position;
        bool _alive = true;



        public int Speed { get => _speed; set => _speed = value; }
        public bool AlienOrPlayer { get => _alienOrPlayer; set => _alienOrPlayer = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public Image Image { get => _image; set => _image = value; }
        public Point Position { get => _position; set => _position = value; }
        public Size LazerSize
        {
            get => this.Image.Size;
        }
        public bool Alive { get => _alive; set => _alive = value; }

        public Lazer(int x, int y) : this(new Point(x, y))
        {

        }
        public Lazer(Point position) : this(position, DEFAULT_SPEED_LAZER)
        {

        }

        public Lazer(Point position, int speed) : this(position, speed, false)
        {

        }
        public Lazer(Point position, int speed, bool alienOrPLayer) : this(position, speed, alienOrPLayer, 1)
        {

        }
        public Lazer(Point position, int speed, bool alienOrPlayer, int damage) : this(position, speed, alienOrPlayer, damage, new Bitmap(Properties.Resources.laserGreen))
        {

        }
        public Lazer(Point position, int speed, bool alienOrPlayer, int damage, Image image)
        {
            this.Position = position;
            this.Speed = speed;
            this.AlienOrPlayer = alienOrPlayer;
            this.Damage = damage;
            this.Image = image;
        }

        public void Move()
        {
            if (this.AlienOrPlayer)
            {
                _position.Y += this.Speed;
            }
            else
            {
                _position.Y -= this.Speed;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.Image, this.Position);
        }
        public void Draw(Graphics e)
        {
            e.DrawImage(this.Image, this.Position);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Position, this.LazerSize);
        }

        public List<Alien> LaserDestroyAlien(List<Alien> vessel)
        {
            List<Alien> result = new List<Alien>();
            foreach (Alien ves in vessel)
            {
                if (ves.InteractWithLazer(this))
                {
                    result.Add(ves);
                    this.Alive = false;
                }
            }

            return result;
        }


    }
}
