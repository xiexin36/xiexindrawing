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
        //处理平移变换
        protected TranslateTransform translateTransform=new TranslateTransform(0,0);
        //处理旋转变换
        protected RotateTransform rotateTransform = new RotateTransform(0);
        //变换集合
        protected TransformGroup transformGroup = new TransformGroup();
        public ShapeControl()
        {
            //这里实现变换的时候,要先旋转后平移,否则出错
            this.transformGroup.Children.Add(this.rotateTransform);
            this.transformGroup.Children.Add(this.translateTransform);            
            this.Transform = this.transformGroup;
        }

        /// <summary>
        /// 显示选中状态
        /// </summary>
        public override void LeftClickToDo()
        {
             this.color = Brushes.Red;
            //进行重绘操作时,系统会自动再次判断图像的位置
            this.Paint(this.ContentBounds.Location);
        }

        /// <summary>
        /// 撤销选中状态
        /// </summary>
        public override void LeaveLeftClickToDo()
        {
            this.color = Brushes.Black;
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
            translateTransform.X = location.X-this.ContentBounds.X-this.ContentBounds.Width/2;
            translateTransform.Y = location.Y-this.ContentBounds.Y-this.ContentBounds.Height/2;           
        }

        public void Rotate() 
        {         
            rotateTransform.CenterX = this.ContentBounds.Left+this.ContentBounds.Width / 2;
            rotateTransform.CenterY = this.ContentBounds.Top+this.ContentBounds.Height / 2;
            rotateTransform.Angle = rotateTransform.Angle+ 90;
        }

    }
}
