using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceDarkVaders
{
    class Bonus
    {
        enum BonusCode : int
        {
            Comet = 1,
            Speed,
            Shield,
        }

        Image _image;
        Point _position;
        int _code = 0;

        public Image Image { get => _image; set => _image = value; }
        public Point Position { get => _position; set => _position = value; }
        public int Code { get => _code; set => _code = value; }

        public Bonus(int code,Point position)
        {
            this.Position = position;
            this.Code = code;
            switch (code)
            {
                case 1:
                    this.Image = new Bitmap(Properties.Resources.cometBonus);
                    break;
                case 2:
                    this.Image = new Bitmap(Properties.Resources.shieldBonus);
                    break;
                case 3:
                    this.Image = new Bitmap(Properties.Resources.speedBonnus);
                    break;
                default:
                    break;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.Image, this.Position);
        }
    }
}
