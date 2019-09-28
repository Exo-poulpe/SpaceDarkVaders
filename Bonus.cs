using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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

        public Image Image { get => _image; set => _image = value; }

        public Bonus(int code)
        {
            switch (code)
            {
                default:
                    break;
            }
        }
    }
}
