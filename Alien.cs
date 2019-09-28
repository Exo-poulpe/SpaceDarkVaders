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

        public Alien() : this(DEFAULT_POSITION_X, DEFAULT_POSITION_Y)
        {

        }

        public Alien(int x, int y) : this(new Point(x, y))
        {

        }

        public Alien(Point position)
        {
            this.Image = new Bitmap(Properties.Resources.alien1);
            this.Pos = position;
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

        public Alien AlienByColumns(int columnsValueX,int columnsValueY)
        {
            return new Alien(columnsValueX * 54, columnsValueY * 26);
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

        public Lazer Fire(Graphics e)
        {
            Lazer laz = new Lazer(this.LazPos,10,true);
            laz.Draw(e);
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

        public void Destroy()
        {
            Alive = false;
        }

    }
}
