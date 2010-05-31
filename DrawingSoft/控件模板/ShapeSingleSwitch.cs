using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
namespace DrawingSoft
{
    public class ShapeSingleSwitch : ShapeControl
    {
        PointConnect connectPointLeft = null;
        PointConnect connectPointRight = null;
        public ShapeSingleSwitch(Point location)
        {
            connectPointLeft = new PointConnect(new Point(location.X, location.Y + 13));
            connectPointRight = new PointConnect(new Point(location.X + 35, location.Y + 13));
            this.Paint(location);
            this.Children.Add(connectPointLeft);
            this.Children.Add(connectPointRight);
        }

        public override void Paint(System.Windows.Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using (dc)
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent, 1), new Rect(new Point(location.X + 1, location.Y + 1), new Size(35, 16)));
                Pen pen = new Pen(this.color, 1);
                
                //左横线
                dc.DrawLine(pen, new Point(location.X, location.Y + 13), new Point(location.X + 10, location.Y + 13));
                dc.DrawLine(new Pen(Brushes.Transparent, 1), new Point(location.X + 10, location.Y), new Point(location.X + 10, location.Y + 1));
                //小圆圈
                dc.DrawEllipse(Brushes.Transparent, pen, new Point(location.X + 13, location.Y + 13), 3.0, 3.0);
                //斜线
                dc.DrawLine(pen, new Point(location.X + 16, location.Y + 13), new Point(location.X + 28, location.Y+1));
                //直线
                dc.DrawLine(pen, new Point(location.X + 25, location.Y + 13), new Point(location.X + 35, location.Y + 13));
            }
        }
    }
}
