using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
namespace DrawingSoft
{
    public class ShapeDoubleSwitch : ShapeControl
    {
        PointConnect connectPointLeft = null;
        PointConnect connectPointRightTop = null;
        PointConnect connectPointRightBottom = null;
        public ShapeDoubleSwitch(Point location)
        {
            connectPointLeft = new PointConnect(new Point(location.X, location.Y + 13));
            connectPointRightTop = new PointConnect(new Point(location.X + 40, location.Y + 3));
            connectPointRightBottom = new PointConnect(new Point(location.X + 40, location.Y + 23));
            this.Paint(location);
            this.Children.Add(connectPointLeft);
            this.Children.Add(connectPointRightTop);
            this.Children.Add(connectPointRightBottom);
        }

        public override void Paint(System.Windows.Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using (dc)
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent, 1), new Rect(new Point(location.X + 1, location.Y + 1), new Size(40, 23)));
                Pen pen = new Pen(this.color, 1);
                //左横线
                dc.DrawLine(pen, new Point(location.X, location.Y + 13), new Point(location.X + 10, location.Y + 13));
                //小圆圈
                dc.DrawEllipse(Brushes.Transparent, pen, new Point(location.X + 12, location.Y + 13), 3.0, 3.0);
 
                dc.DrawLine(pen, new Point(location.X + 15, location.Y + 13), new Point(location.X + 30, location.Y + 13));
                //小圆圈
                dc.DrawEllipse(Brushes.Transparent, pen, new Point(location.X + 25, location.Y + 3), 3.0, 3.0);
                dc.DrawLine(pen, new Point(location.X+27, location.Y + 3), new Point(location.X + 40, location.Y + 3));
                //小圆圈
                dc.DrawEllipse(Brushes.Transparent, pen, new Point(location.X + 25, location.Y + 23), 3.0, 3.0);
                dc.DrawLine(pen, new Point(location.X + 27, location.Y + 23), new Point(location.X + 40, location.Y + 23));

            }
        }
    }
}
