﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceDarkVaders
{
    interface Vessel
    {

        bool InteractWithLazer(Lazer laz);

        Rectangle ToRectangle();
    }
}
