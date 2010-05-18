using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace DrawingSoft
{
    public class ToolMouseShape
    {

        public static  void SetMouseShape(Cursor NowCursor)
        {
            Window window = Application.Current.Windows[0];
            MainWindow main = window as MainWindow;
            main.setCanvasMouseShape(NowCursor);
        }
    }
}
