using System;
using System.Windows;
using System.Windows.Controls;

namespace DrawingSoft
{
    public class RibbonPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count < 1) return new Size(0, 0);

            // Ask the first child for its desired size, given unlimited space
            UIElement firstChild = Children[0];
            firstChild.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            // If there's only one child, this panel would like to be the exact same size
            if (Children.Count < 2) return firstChild.DesiredSize;

            // If not, calculate the desired width based on all children
            double numRows = Math.Ceiling((Children.Count - 1) / 3d);
            double maxWidthForEachRemainingChild = 0;

            for (int i = 1; i < Children.Count; i++)
            {
                // Ask each child for its desired size, given unlimited space
                UIElement child = Children[i];
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                // Keep track of the maximum width
                maxWidthForEachRemainingChild = Math.Max(child.DesiredSize.Width, maxWidthForEachRemainingChild);
            }

            return new Size(
                firstChild.DesiredSize.Width + maxWidthForEachRemainingChild * numRows, // total width
                firstChild.DesiredSize.Height); // height = desired height of the first child
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count < 1) return finalSize;
            
            // Give the first child its desired width but the height of the panel
            UIElement firstChild = Children[0];
            Point childOrigin = new Point(0, 0);
            Size firstChildSize = new Size(firstChild.DesiredSize.Width, finalSize.Height);
            firstChild.Arrange(new Rect(childOrigin, firstChildSize));

            if (Children.Count < 2) return finalSize;

            // Determine the size for all the remaining children
            double numRows = Math.Ceiling((Children.Count - 1) / 3d);
            Size childSize = new Size((finalSize.Width - firstChildSize.Width) / numRows, finalSize.Height / 3);
            childOrigin.X += firstChildSize.Width;

            for (int i = 1; i < Children.Count; i++)
            {
                UIElement child = Children[i];
                child.Arrange(new Rect(childOrigin, childSize));

                if (i % 3 == 0)
                {
                    // Start a new column
                    childOrigin.X += childSize.Width;
                    childOrigin.Y = 0;
                }
                else
                    childOrigin.Y += childSize.Height;
            }

            // Fill all the space given
            return finalSize;
        }
    }
}