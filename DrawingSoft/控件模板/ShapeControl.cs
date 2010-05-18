using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace DrawingSoft
{
    public class ShapeControl : Shape
    {
        protected  TranslateTransform translateTransform=new TranslateTransform(0,0);
        public override void LeftClickToDo(Brush Nowcolor)
        {
            this.color = Nowcolor;
            //进行重绘操作时,系统会自动再次判断图像的位置
            this.Paint(this.ContentBounds.Location);
        }

        public override void MouseEnterToDo()
        {
            ToolMouseShape.SetMouseShape(Cursors.Hand);
        }

        public override void Paint(System.Windows.Point location)
        {
            
        }

        public override void MouseMoveToDo(Point location)
        {
            translateTransform.X=location.X-this.ContentBounds.X-this.ContentBounds.Width/2;
            translateTransform.Y = location.Y-this.ContentBounds.Y-this.ContentBounds.Height/2;
            this.Transform = translateTransform;
        }
    }
}
