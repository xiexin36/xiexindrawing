using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace DrawingSoft
{
    public class PointConnect : Shape
    {
        public PointConnect()
        {
            this.Opacity = 0;
        }

        public PointConnect(Point location)
        {
            this.Opacity = 0;
            this.Paint(location);
        }

        public override void LeftClickToDo(Brush color)
        {
        }

        public override void Paint(Point location)
        {
            DrawingContext dc = this.RenderOpen();
            using (dc)
            {
                dc.DrawEllipse(this.color, null, location, 4.0, 4.0);
            }
        }

        public override void MouseEnterToDo()
        {
            this.Opacity = 1;
            ToolMouseShape.SetMouseShape(Cursors.Hand);
        }

        public override void MouseLeaveToDo()
        {
            base.MouseLeaveToDo();
            this.Opacity = 0;
        }
    }
}
