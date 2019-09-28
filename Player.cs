using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceDarkVaders
{
    class Player : Vessel
    {
        const int DEFAULT_POSITION_X = 210;
        const int DEFAULT_POSITION_Y = 400;

        Bitmap _image;
        Point _pos;
        Point _lazPos;
        uint _scoreValue = 0;
        int _bonusValue = 0;

        public Bitmap Image { get => _image;private set => _image = value; }
        public Point Pos { get => _pos; private set => _pos = value; }
        public Size PlayerSize
        {
            get => this.Image.Size;
        }
        private Point LazPos { get => new Point(this.Pos.X + 20,this.Pos.Y - 10);}
        public uint ScoreValue { get => _scoreValue; set => _scoreValue = value; }
        public int BonusValue { get => _bonusValue; set => _bonusValue = value; }

        public Player():this(DEFAULT_POSITION_X,DEFAULT_POSITION_Y)
        {

        }

        public Player(int x,int y):this(new Point(x,y))
        {

        }

        public Player(Point position)
        {
            this.Image = new Bitmap(Properties.Resources.player);
            this.Pos = position;
        }

        public void Draw(PaintEventArgs g)
        {
            g.Graphics.DrawImage(this.Image,this.Pos);
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

        public bool InteractWithLazer(Lazer laz)
        {
            return this.ToRectangle().IntersectsWith(laz.ToRectangle());
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Pos, this.PlayerSize);
        }


        public void UpScore(int value = 1)
        {
            this.ScoreValue += (uint)value;
            this.BonusValue += value;
        }

        public Lazer Fire(Graphics e)
        {
            Lazer laz = new Lazer(this.LazPos);
            laz.Draw(e);
            return laz;
        }
        public Lazer Fire(int speed)
        {
            return null;
        }
    }
}
