using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingSoft
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void setCanvasMouseShape(Cursor nowCursor)
        {
            this.canvasDrawPanel.Cursor = nowCursor;
        }

        //实现画布的拖动扩大
        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed&&this.canvasDrawPanel.LastClickShape is PointResize)
            {
                this.canvasDrawPanel.LastClickShape.MouseMoveToDo(e.GetPosition(this.canvasDrawPanel));
            }
        }
        //实现画布的虚框消失
        private void ScrollViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.canvasDrawPanel.drawingVisualDashRect.Opacity == 1&&this.canvasDrawPanel.LastClickShape is PointResize)
            {
	            Point location = e.GetPosition(this.canvasDrawPanel);
	            this.canvasDrawPanel.PaintBackground(location);
	            this.canvasDrawPanel.drawingVisualDashRect.Opacity = 0;
            }
        }

        private void buttonTranlate_Click(object sender, RoutedEventArgs e)
        {
            if (this.canvasDrawPanel.LastClickShape is ShapeControl)
            {
                ((ShapeControl)this.canvasDrawPanel.LastClickShape).Rotate();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (this.canvasDrawPanel.LastClickShape is ShapeControl)
            {
                this.canvasDrawPanel.RemoveDrawingVisual();
            }
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 2; i < this.canvasDrawPanel.listShapes.Count ; i++)
            {
                this.canvasDrawPanel.RemoveDrawingVisual(this.canvasDrawPanel.listShapes[i]);
            }
            int num = this.canvasDrawPanel.listShapes.Count;
            Console.WriteLine(num);
            this.canvasDrawPanel.listShapes.RemoveRange(2, num-2);
        }
    }
}
