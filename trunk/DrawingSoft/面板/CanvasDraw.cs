using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Forms;


namespace DrawingSoft
{
    class CanvasDraw:Canvas
    {
        public Shape LastClickShape = null;//保存最后一个被选中的对象
        public List<Shape> listShapes=new List<Shape>();//保存所有的对象集合
        private readonly static Point DashRectangleLocation=new Point(4,4);

        public Shape drawingVisualBackground=new Shape();//画布背景的DrawingVisua
        private Shape LastMoveInShape;//保存最后进入的对象
        //实现射线的绘制
        private LineTwoPoint line = new LineTwoPoint();
        public Shape drawingVisualDashRect = new Shape();//绘制虚线框
        public ScaleTransform resizeTransform = new ScaleTransform();


        public CanvasDraw()
        {
            this.LastMoveInShape =this.LastClickShape= drawingVisualBackground;
            this.drawingVisualDashRect.Opacity = 0;
            
            this.PaintBackground(new Point(500, 300));
            this.AddDrawingVisual(this.drawingVisualBackground);
            this.AddDrawingVisual(drawingVisualDashRect);
            this.AddDrawingVisual(this.line);
            for (int i = 0; i < 1000;i++ )
            {
                this.AddDrawingVisual(new ShapePowerSourse(new Point(10+i, i)));
            }
            this.AddDrawingVisual(new ShapePowerSourse(new Point(400, 200)));
            this.AddDrawingVisual(new ShapePowerSourse(new Point(100, 100)));
            this.AddDrawingVisual(new ShapeDoubleSwitch(new Point(120, 120)));
            this.AddDrawingVisual(new ShapeSingleSwitch(new Point(140, 140)));
        }
        /// <summary>
        /// 自定义方法实现元素的添加,并使其显示
        /// </summary>
        /// <param name="shape"></param>
        public void AddDrawingVisual(Shape shape)
        {
            listShapes.Add(shape);
            AddVisualChild(shape);
            AddLogicalChild(shape);
        }

        public void RemoveDrawingVisual()
        {
            listShapes.Remove(this.LastClickShape);
            RemoveVisualChild(this.LastClickShape);
            RemoveLogicalChild(this.LastClickShape);
        }

        public void RemoveDrawingVisual(Shape s)
        {
            RemoveVisualChild(s);
            RemoveLogicalChild(s);
        }

        /// <summary>
        ///绘制画布,根据调整的大小
        /// </summary>
        /// <param name="Nowsize"></param>
        public void PaintBackground(Point Nowsize)
        {
            this.MinWidth = Nowsize.X + 10;
            this.MinHeight = Nowsize.Y + 10;
            DrawingContext dc = drawingVisualBackground.RenderOpen();
            drawingVisualBackground.Children.Clear();//清除坐标点
            using (dc)
            {              
                DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                effect.Color = Colors.LightSlateGray;
                dc.PushEffect(effect, null);
                dc.DrawRectangle(Brushes.White, new Pen(Brushes.White, 1), new Rect(new Point(6, 6), new Size(Nowsize.X, Nowsize.Y)));
                drawingVisualBackground.Children.Add(new PointResize(new Point(6+Nowsize.X,6+ Nowsize.Y), PointResize.PointStyles.CornerResize));
                drawingVisualBackground.Children.Add(new PointResize(new Point(6+Nowsize.X, 6 + Nowsize.Y / 2), PointResize.PointStyles.WeightResize));
                drawingVisualBackground.Children.Add(new PointResize(new Point(6+Nowsize.X / 2, 6 + Nowsize.Y), PointResize.PointStyles.HeightResize));
            }
        }

        /// <summary>
        /// 绘制控制画布大小的虚线框
        /// </summary>
        /// <param name="NowSize"></param>
        public void PaintDashRect(Point NowSize)
        {
            DrawingContext dc = this.drawingVisualDashRect.RenderOpen();
            using (dc)
            {
                Pen pen=new Pen(Brushes.Black,1);
                pen.DashStyle = DashStyles.Dot;
                dc.DrawRectangle(Brushes.Transparent,pen,new Rect(DashRectangleLocation,NowSize));
            }
        }


#region 实现图像显示
        /// <summary>
        /// 覆盖方法实现Drawingvisual的显示
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            { return listShapes.Count; }
        }

        /// <summary>
        /// 覆盖方法实现Drawingvisual的显示
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= listShapes.Count)
            {
                throw new ArgumentOutOfRangeException(index.ToString());
            }
            return listShapes[index];
        }
#endregion
       

        /// <summary>
        /// 处理鼠标单击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseEnter(e);
            Point location = e.GetPosition(this);
            HitTestResult result = VisualTreeHelper.HitTest(this, location);
            //双击同一个对象
            if (result.VisualHit.Equals(this.LastClickShape))                
            {   
                //双击一段折线
                if (this.LastClickShape is LineSub)
                {
                    ColorDialog dialog = new ColorDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                                    
                    }
                }
                return;
            }
            if (result.Equals(this.drawingVisualBackground))//点中背景使改变控件的红色恢复为黑色
            {
                this.LastClickShape.LeftClickToDo();
                this.LastClickShape = this.drawingVisualBackground;
                return;
            }
            if (result.VisualHit is Shape)//切换选中的控件
            {
                if(this.LastClickShape is ShapeControl)//是模型控件才改变颜色,对于点不改变颜色
                    this.LastClickShape.LeaveLeftClickToDo();
                Shape dv = result.VisualHit as Shape;
                dv.LeftClickToDo();
                this.LastClickShape = dv;
            }
        }
       
        /// <summary>
        /// 控制全局的鼠标位置信息,实现鼠标进入离开事件的触发
        ///因为CanvasDraw面板有了自己的大小,这时可以响应Enter事件
        ///但是只在从其他控件进入本控件时才有效
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point location = e.GetPosition(this);

            //完成相应的鼠标拖动操作
            if (e.LeftButton== MouseButtonState.Pressed)
            {
                if (location.X >= this.MinWidth-15 || location.Y >= this.MinHeight-15)         
                    return;           
                this.LastClickShape.MouseMoveToDo(location);
                //实现连线时的射线绘制
                if (this.LastClickShape is PointConnect)
                {
                    this.LastMoveInShape.MouseLeaveToDo();//使上次准备连接的点直接消失
                    PointConnect p = this.LastClickShape as PointConnect;
                    line.LineConnect(p.GetNowLocation(), location);
                    HitTestResult result = VisualTreeHelper.HitTest(this, location);
                    if (result.VisualHit is PointConnect)
                    {
                        PointConnect dv = result.VisualHit as PointConnect;
                        dv.MouseEnterToDo();
                        this.LastMoveInShape = dv;
                    }
                }
            }
            else //设置相应的鼠标进入事件
            {
                HitTestResult result = VisualTreeHelper.HitTest(this, location);
                if (result.VisualHit is Shape)
                {
                    Shape dv = result.VisualHit as Shape;
                    if (dv.Equals(this.LastMoveInShape))//鼠标仍在同一个对象内
                        return;
                    else
                    {
                        //此处先处理离开事件,否则对于先处理的进入事件,离开事件会重新修改,使得失效
                        this.LastMoveInShape.MouseLeaveToDo();
                        dv.MouseEnterToDo();
                        this.LastMoveInShape = dv;
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            //实现画布大小的改变
            if (this.drawingVisualDashRect.Opacity==1&&this.LastClickShape is PointResize)
            {
                this.drawingVisualDashRect.Opacity = 0;
                this.PaintBackground(e.GetPosition(this));
            }
            else if (this.LastClickShape is PointConnect)
            {
                Point location = e.GetPosition(this);
                HitTestResult result = VisualTreeHelper.HitTest(this, location);
                if (result.VisualHit is PointConnect)
                {
                    this.line.Opacity = 0;
                    PointConnect pointEnd = result.VisualHit as PointConnect;
                    PointConnect pointStart=this.LastClickShape as PointConnect;
                    this.AddDrawingVisual(new LineSwitchConnect(pointStart,pointEnd));
                }
                else
                    this.line.Opacity = 0;
            }
        }
    }
}
