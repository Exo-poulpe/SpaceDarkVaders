using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpaceDarkVaders
{
    class Alien : Vessel
    {

        const int DEFAULT_POSITION_X = 210;
        const int DEFAULT_POSITION_Y = 10;

        Bitmap _image;
        Point _pos;
        Point _lazPos;
        bool _alive = true;
        int _detahStep = 0;
        int _scoreValue = 1;
        int _columnX = 0, _columnY = 0;
        uint _lifePoint = 1;
        static Random rng = new Random();

        public Bitmap Image { get => _image; private set => _image = value; }
        public Point Pos { get => _pos; private set => _pos = value; }

        public Size AlienSize
        {
            get => this.Image.Size;
        }

        private Point LazPos { get => new Point(this.Pos.X - 20, this.Pos.Y + 10); }
        public bool Alive { get => _alive; set => _alive = value; }
        public int DetahStep { get => _detahStep; set => _detahStep = value; }
        public int ScoreValue { get => _scoreValue; set => _scoreValue = value; }
        public int ColumnX { get => _columnX; set => _columnX = value; }
        public int ColumnY { get => _columnY; set => _columnY = value; }
        public uint LifePoint { get => _lifePoint; set => _lifePoint = value; }

        public Alien() : this(DEFAULT_POSITION_X, DEFAULT_POSITION_Y)
        {

        }

        public Alien(int columnX, int columnY) : this(new Point(columnX * 54, columnY * 26))
        {

        }

        public Alien(Point position)
        {
            this.Image = new Bitmap(Properties.Resources.alien1);
            this.Pos = position;
            this.ColumnX = this.Pos.X / 54;
            this.ColumnY = this.Pos.Y / 26;
        }


        public void Draw(PaintEventArgs g)
        {
            if (this.Alive)
            {
                g.Graphics.DrawImage(this.Image, this.Pos);
            }
            else
            {
                switch (DetahStep)
                {
                    case 0:
                        g.Graphics.DrawImage(Properties.Resources.explosion1, this.Pos);
                        DetahStep += 1;
                        break;
                    case 1:
                        g.Graphics.DrawImage(Properties.Resources.explosion2, this.Pos);
                        DetahStep += 1;
                        break;
                    case 2:
                        g.Graphics.DrawImage(Properties.Resources.explosion3, this.Pos);
                        DetahStep += 1;
                        break;
                    default:
                        break;
                }

            }
        }


        public Alien AlienByColumns(int columnsValueX, int columnsValueY)
        {
            return new Alien(columnsValueX, columnsValueY);
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(this.Image, this.Pos);
        }

        public void MoveRight(int value = 2)
        {
            this._pos.X += value;
        }

        public void MoveLeft(int value = 2)
        {

            this._pos.X -= value;
        }

        public Lazer Fire()
        {
            Lazer laz = new Lazer(this.LazPos, 10, true, 1, new Bitmap(Properties.Resources.laserRed));
            return laz;
        }
        public Lazer Fire(int speed)
        {
            return null;
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Pos, this.AlienSize);
        }
        public bool InteractWithLazer(Lazer laz)
        {
            return this.ToRectangle().IntersectsWith(laz.ToRectangle());
        }

        public bool IsDestroyByLaz(Lazer laz)
        {
            return (laz.Damage >= this.LifePoint) ? true : false;
        }

        public void Move()
        {
            //if (this.ColumnX >= Form1.MAX_COLUMNS_X)
            //{
            //    this.Pos = new Point(0 * 26, (this.ColumnY += 1) * 54);
            //}
            //else
            //{
            //    this.Pos = new Point((this.ColumnX += 1) * 26, this.ColumnY * 54);
            //}


            //if (this.ColumnX == Form1.MAX_COLUMNS_X)
            //{
            //    this.ColumnY += 1;
            //    this.Pos = new Point(this.ColumnX * 54, this.ColumnY * 26);
            //}
            //else
            //{
            //    this.ColumnX += 1;
            //    this.Pos = new Point(this.ColumnX * 54, this.ColumnY * 26);
            //}
            this.Pos = new Point(this.ColumnX * 54, (this.ColumnY += 1) * 26);
        }

        public void Destroy()
        {
            Alive = false;
        }

    }
}
