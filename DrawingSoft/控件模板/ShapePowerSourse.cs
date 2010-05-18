using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace DrawingSoft
{
    public class ShapePowerSourse : ShapeControl
    {
        PointConnect connectPointLeft = null;
        PointConnect connectPointRight = null;

        public ShapePowerSourse(Point location)
        {
            connectPointLeft = new PointConnect(new Point(location.X, location.Y + 13));
            connectPointRight = new PointConnect(new Point(location.X + 30, location.Y + 13));
            this.Paint(location);
            this.Children.Add(connectPointLeft);
            this.Children.Add(connectPointRight);
        }

        public override void Paint(Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using(dc)
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent,1),new Rect(new Point(location.X+1,location.Y+1),new Size(30,26)));
                Pen pen = new Pen(this.color, 1);
                //左横线
                dc.DrawLine(pen, new Point(location.X,location.Y+13), new Point(location.X+10, location.Y + 13));
                //左竖线
                dc.DrawLine(pen, new Point(location.X + 10, location.Y), new Point(location.X + 10, location.Y + 26));
                //右竖线
                dc.DrawLine(pen, new Point(location.X + 20, location.Y), new Point(location.X+20, location.Y + 26));
                //右横线
                dc.DrawLine(pen, new Point(location.X + 20, location.Y + 13), new Point(location.X + 30, location.Y + 13));
            }
        }
    }
}
