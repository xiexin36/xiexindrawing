using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;


namespace DrawingSoft
{
    public class ShapeLight : ShapeControl
    {
        PointConnect connectPointLeft = null;
        PointConnect connectPointRight = null;

        public ShapeLight(Point location)
        {
            connectPointLeft = new PointConnect(new Point(location.X, location.Y + 13));
            connectPointRight = new PointConnect(new Point(location.X + 40, location.Y + 13));
            this.Paint(location);
            this.Children.Add(connectPointLeft);
            this.Children.Add(connectPointRight);
        }

        public override void Paint(Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using (dc)
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Transparent, 1), new Rect(new Point(location.X + 1, location.Y + 1), new Size(40, 26)));
                Pen pen = new Pen(this.color, 1);
                //左横线
                dc.DrawLine(pen, new Point(location.X, location.Y + 13), new Point(location.X + 9, location.Y + 13));
                //大圆
                dc.DrawEllipse(Brushes.Transparent, pen, new Point(location.X + 20, location.Y + 13), 12.5, 12.5);
                //右斜线
                dc.DrawLine(pen, new Point(location.X + 12, location.Y + 4), new Point(location.X + 28, location.Y + 22));
                //左斜线
                dc.DrawLine(pen, new Point(location.X + 12, location.Y + 22), new Point(location.X + 28, location.Y + 4));
                //右横线
                dc.DrawLine(pen, new Point(location.X + 31, location.Y + 13), new Point(location.X + 40, location.Y + 13));
            }
        }
    }
}
