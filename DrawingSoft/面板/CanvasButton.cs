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
    class CanvasButton:Canvas
    {
        public static BevelBitmapEffect effect = new BevelBitmapEffect();
       
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            effect.EdgeProfile = EdgeProfile.Linear;
            this.BitmapEffect = effect;
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            this.BitmapEffect = null;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            effect.EdgeProfile = EdgeProfile.BulgedUp;
            this.BitmapEffect=effect;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            effect.EdgeProfile = EdgeProfile.Linear;
            this.BitmapEffect = effect;
        }
    }
}
