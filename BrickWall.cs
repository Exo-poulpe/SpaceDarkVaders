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
        public Point Position { get => _position; set => _position = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }
        public Image Image { get => _image; set => _image = value; }

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

    }
}
