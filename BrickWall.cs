using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceDarkVaders
{
    class BrickWall
    {
        Point _position;
        int _sizeX, sizeY;
        Image _image;
        int _lifePoint = 4;
        public Point Position { get => _position; set => _position = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }
        public Image Image { get => _image; set => _image = value; }
        public int LifePoint { get => _lifePoint; set => _lifePoint = value; }

        public BrickWall(int x,int y):this(new Point(x,y))
        {

        }
        public BrickWall(Point position)
        {
            this.Position = position;
            this.Image = Properties.Resources.wall;
        }

        public void Draw(PaintEventArgs e)
        {
            switch (LifePoint)
            {
                case 4:
                    this.Image = new Bitmap(Properties.Resources.wall);
                    break;
                case 3:
                    this.Image = new Bitmap(Properties.Resources.wallDamaged1);
                    break;
                case 2:
                    this.Image = new Bitmap(Properties.Resources.wallDamaged2);
                    break;
                case 1:
                    this.Image = new Bitmap(Properties.Resources.wallDamaged3);
                    break;
                default:
                    break;
            }
            e.Graphics.DrawImage(this.Image,this.Position);
        }

        public bool InteractWithLazer(Lazer laz)
        {
            return this.ToRectangle().IntersectsWith(laz.ToRectangle());
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Position, this.Image.Size);
        }

        public void LifeDown(int value = 1)
        {
            this.LifePoint -= value;
        }

        public void LifeUp(int value = 1)
        {
            this.LifePoint += value;
        }

    }
}
