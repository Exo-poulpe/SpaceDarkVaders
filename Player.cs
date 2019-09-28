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
        int _lifePoint = 3;
        int _damage = 1;
        int _lazSpeed = 10;
        int _direction = 0;

        public Bitmap Image { get => _image; private set => _image = value; }
        public Point Pos { get => _pos; private set => _pos = value; }
        public Size PlayerSize
        {
            get => this.Image.Size;
        }
        private Point LazPos { get => new Point(this.Pos.X + 22, this.Pos.Y - 10); }
        public uint ScoreValue { get => _scoreValue; set => _scoreValue = value; }
        public int BonusValue { get => _bonusValue; set => _bonusValue = value; }
        public int LifePoint { get => _lifePoint; set => _lifePoint = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public int LazSpeed { get => _lazSpeed; set => _lazSpeed = value; }
        public int Direction { get => _direction; set => _direction = value; }

        public Player() : this(DEFAULT_POSITION_X, DEFAULT_POSITION_Y)
        {

        }

        public Player(int x, int y) : this(new Point(x, y))
        {

        }

        public Player(Point position)
        {
            this.Image = new Bitmap(Properties.Resources.player);
            this.Pos = position;
        }

        public void Draw(PaintEventArgs g)
        {
            g.Graphics.DrawImage(this.Image, this.Pos);
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(this.Image, this.Pos);
        }

        public void MoveRight(int value = 2, int sizeWindow = 400)
        {
            if ((this.Pos.X + value) <= sizeWindow - (this.PlayerSize.Width + 10))
            {
                this._pos.X += value;
            }
        }

        public void MoveLeft(int value = 2)
        {
            if ((this.Pos.X - value) > 0)
            {
                this._pos.X -= value;
            }
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
            Lazer laz = new Lazer(this.LazPos, this.LazSpeed, false, this.Damage);
            laz.Draw(e);
            return laz;
        }
        public Lazer Fire(int speed)
        {
            return null;
        }

        public void FireBonus(Bonus bonus)
        {
            switch (bonus.Code)
            {
                case 1:
                    this.Damage *= 2;
                    break;
                case 2:
                    this.LifePoint += 1;
                    break;
                case 3:
                    Form1.DEFAULT_TIME_PLAYER_FIRE = 500;
                    break;
                default:
                    break;
            }
        }

        public void StopBonus(Bonus bonus)
        {
            switch (bonus.Code)
            {
                case 1:
                    this.Damage /= 2;
                    break;
                case 2:
                    this.LifePoint -= 1;
                    break;
                case 3:
                    Form1.DEFAULT_TIME_PLAYER_FIRE = 2000;
                    break;
                default:
                    break;
            }
        }

        public void LifeDownPlayer(int value = 1)
        {
            this.LifePoint -= value;
        }

        public void LifeUpPlayer(int value = 1)
        {
            this.LifePoint += value;
        }

    }
}
