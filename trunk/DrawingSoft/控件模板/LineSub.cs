using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace DrawingSoft
{
    /// <summary>
    /// 折线中的每一段直线
    /// </summary>
    class LineSub : ShapeLine
    {

        public LineSub(Point start,Point end,Brush color)
        {
            Paint(start, end, color);
        }

        public void Paint(Point start,Point end,Brush color)
        {
            DrawingContext dc = this.RenderOpen();
            using(dc)
            {
                dc.DrawLine(new Pen(color,1), start, end);
            }
        }


        public override void LeftClickToDo()
        {
            throw new System.NotImplementedException();
        }

    }
}
