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
        protected Brush color = Brushes.Black;
        public  Shape()
        {}

        public virtual void LeftClickToDo(Brush color)
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
    }
}
