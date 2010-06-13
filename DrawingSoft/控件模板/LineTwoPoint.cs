﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace DrawingSoft
{
    class LineTwoPoint:Shape
    {
        public LineTwoPoint()
        {
            this.Opacity = 0;
        }
        public void LineConnect(Point start,Point end)
        {
            if (this.Opacity == 0)
                this.Opacity = 1;
            DrawingContext dc = this.RenderOpen();
            using(dc)
            {
                dc.DrawLine(new Pen(Brushes.Black,1),new Point(start.X,start.Y),end);
            }
        }
    }
}
