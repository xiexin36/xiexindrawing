using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace DrawingSoft
{
    public class Shape:DrawingVisual
    {
        public SolidColorBrush color = Brushes.Black;
        public  Shape()
        {}

        public virtual void LeaveLeftClickToDo()
        {}

        public virtual void LeftClickToDo()
        {}

        public virtual void Paint(Point location)
        {}

        public virtual void MouseEnterToDo()
        {}

        public virtual void MouseLeaveToDo()
        {
            ToolMouseShape.SetMouseShape(Cursors.Arrow);
        }

        public virtual void MouseMoveToDo(Point offset)
        {}

        /// <summary>
        /// 返回点当前的位置,相对于面板
        /// </summary>
        /// <returns></returns>
        public Point GetNowLocation()
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;
            Point p = this.TransformToAncestor(w.canvasDrawPanel).Transform(this.ContentBounds.Location);
            return Point.Add(p, new Vector(4, 4));
        }
    }
}
