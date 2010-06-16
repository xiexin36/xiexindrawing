using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace DrawingSoft
{
    class LineSwitchConnect : ShapeLine
    {
        /// <summary>
        /// 保存经过的点
        /// </summary>
        private List<Point> listPoints=new List<Point>();
        private List<ShapeControl> hitResultsList = new List<ShapeControl>();
        private bool LineSubdirection = true;//true代表水平,fale代表垂直
        private Point endPoint;//连线的终点
        private const double subLength = 10;//确定每次探查的步进

        public LineSwitchConnect(PointConnect startPointConnect,PointConnect endPointConnect)
        {
            //使连接点保存连接线对象
            startPointConnect.LineConnect = this;
            endPointConnect.LineConnect = this;
            //获取点的当前位置
            this.GetPath(startPointConnect.GetNowLocation(), endPointConnect.GetNowLocation());
            Console.WriteLine(startPointConnect.GetNowLocation()+"    "+endPointConnect.GetNowLocation());
            Console.WriteLine("isRun");
            Paint(1);
        }

        /// <summary>
        /// 初始化判断起始点和终止点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public  void GetPath(Point start,Point end)
        {
            if (start.X==end.X)
            {              
                if (start.Y!=end.Y)
                {
                    Point p;
                    double lengthY;
                    if (start.Y < end.Y)//选取y较小的点作为起始点,另一个点为终点
                    {
                        p = start;
                        lengthY = end.Y - start.Y;
                        this.endPoint = end;
                    }
                    else if (start.Y == end.Y)
                        return;
                    else
                    {
                        p = end;
                        lengthY = start.Y - end.Y;
                        this.endPoint = start;
                    }
                    this.LineSubdirection = false;
                    //判断连接点之间的距离是否小于步进
                    if (lengthY >= subLength)
                        MyHitTest(p, new Vector(0,subLength ));
                    else
                        MyHitTest(p, new Vector(0,lengthY));
                }
            } 
            else
            {
                this.LineSubdirection = true;   
                Point p;
                double lengthX;
                if (start.X < end.X)//选取x较小的点作为起始点,另一个点为终点
                {
                    p = start;
                    lengthX = end.X - start.X;               
                    this.endPoint = end;
                }
                else
                {
                    p = end;
                    lengthX = start.X - end.X;
                    this.endPoint = start;
                }
                if (lengthX >= subLength)
                    MyHitTest(p, new Vector(subLength, 0));
                else
                    MyHitTest(p, new Vector(lengthX, 0));          
            }
        }


        /// <summary>
        /// 此处要负责对拐点链表的起始点进行检查
        /// </summary>
        /// <param name="Thickness"></param>
        public void Paint(int Thickness)
        {
           for (int i=0;i<listPoints.Count-1;i++)
           {
               this.Children.Add(new LineSub(listPoints[i],listPoints[i+1],this.color));
           }
        }

#region 处理线的粗细
        public override void LeaveLeftClickToDo()
        {
            Paint(1);
        }

        /// <summary>
        /// 单击事件,改变线的粗度,显示选中
        /// </summary>
        /// <param name="Comecolor"></param>
        public override void LeftClickToDo()
        {
            Paint(3);
        }
#endregion
       

        /// <summary>
        /// 执行点击测试,对画出的线进行判断
        /// </summary>
        /// <param name="pt"></param>
        public void MyHitTest(Point pt,Vector v)
        {      
            LineGeometry expandedHitTestArea = new LineGeometry(new Point(90,110), new Point(160,110));
            hitResultsList.Clear();
            Console.WriteLine("heelo");
            VisualTreeHelper.HitTest((Application.Current.MainWindow as MainWindow).canvasDrawPanel, null,
                new HitTestResultCallback(MyHitTestResultCallback),
                new GeometryHitTestParameters(expandedHitTestArea));
            Console.WriteLine("world");


            //生成拐点,查找边界
            //if (hitResultsList.Count > 0)
            //{
            //    if (Point.Add(pt, v).X == this.endPoint.X && Point.Add(pt, v).Y == this.endPoint.Y)
            //    {
            //        this.listPoints.Add(this.endPoint);
            //        return;
            //    }
            //    this.listPoints.Add(pt);
            //    ProcessHitTestResultsList();
            //}
            //else//继续查找拐点
            //{
            //    if (this.LineSubdirection)//继续水平查找
            //    {
            //        double lengthX;
            //        if (pt.X < this.endPoint.X)//水平探查向右走
            //        {
            //            lengthX = endPoint.X - pt.X;
            //            if (lengthX > subLength)
            //                MyHitTest(pt, new Vector(subLength, 0));
            //            else
            //                MyHitTest(pt, new Vector(lengthX, 0));
            //        }
            //        else
            //        {
            //            lengthX = pt.X - endPoint.X;
            //            if (lengthX > subLength)
            //                MyHitTest(pt, new Vector(-subLength, 0));
            //            else
            //                MyHitTest(pt, new Vector(-lengthX, 0));
            //        }              
            //    }
            //    else
            //    {
            //        double lengthY;
            //        if (pt.Y < this.endPoint.Y)//垂直探查向下走
            //        {
            //            lengthY = endPoint.Y - pt.Y;
            //            if (lengthY > subLength)
            //                MyHitTest(pt, new Vector(subLength, 0));
            //            else
            //                MyHitTest(pt, new Vector(lengthY, 0));
            //        }
            //        else
            //        {
            //            lengthY = pt.Y - endPoint.Y;
            //            if (lengthY > subLength)
            //                MyHitTest(pt, new Vector(-subLength, 0));
            //            else
            //                MyHitTest(pt, new Vector(-lengthY, 0));
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 查找边界的实现
        /// </summary>
        private void ProcessHitTestResultsList()
        {

        }

        //对点击的结果进行判断
        public HitTestResultBehavior MyHitTestResultCallback(HitTestResult result)
        {
            IntersectionDetail intersectionDetail = ((GeometryHitTestResult)result).IntersectionDetail;
            Console.WriteLine(intersectionDetail.ToString());
            Console.WriteLine(result.VisualHit);
            if (result.VisualHit is ShapeControl)//只有命中控件才避开,添加命中结果
            {
                this.hitResultsList.Add(result.VisualHit as ShapeControl);
                Console.WriteLine(this.hitResultsList[0].GetNowLocation());
            }
            if (result.VisualHit is PointConnect)
            {
                ShapeControl parent = (ShapeControl)((PointConnect)result.VisualHit).Parent;
                Console.WriteLine(parent.ContentBounds.Location);
                this.hitResultsList.Add(parent);
            }
            return HitTestResultBehavior.Stop;            
        }

    }
}
