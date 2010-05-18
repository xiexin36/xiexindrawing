using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace DrawingSoft
{
    public class PointResize : Shape
    {
        public enum PointStyles { HeightResize, WeightResize, CornerResize };
        private readonly PointStyles ownStyle;

        public PointResize()
        {
        }

        public PointResize(Point location, PointStyles resizeStyle)
        {
            this.ownStyle = resizeStyle;
            this.Paint(location);
        }

        public override void MouseEnterToDo()
        {
            switch (this.ownStyle)
            {
                case PointStyles.CornerResize: ToolMouseShape.SetMouseShape(Cursors.SizeNWSE); break;
                case PointStyles.HeightResize: ToolMouseShape.SetMouseShape(Cursors.SizeNS); break;
                case PointStyles.WeightResize: ToolMouseShape.SetMouseShape(Cursors.SizeWE); break;
            }
        }

        public override void MouseMoveToDo(Point offset)
        {
            MainWindow w = Application.Current.MainWindow as MainWindow;
            w.canvasDrawPanel.PaintDashRect(offset);
        }

        public  override void Paint(System.Windows.Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using (dc)
            {
                dc.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 0.8), new Rect(location, location + new Vector(5, 5)));
            }
        }

        public override void LeftClickToDo(System.Windows.Media.Brush color)
        {
            MainWindow w = Application.Current.MainWindow as MainWindow;
            w.canvasDrawPanel.drawingVisualDashRect.Opacity = 1;
        }
    }
}
