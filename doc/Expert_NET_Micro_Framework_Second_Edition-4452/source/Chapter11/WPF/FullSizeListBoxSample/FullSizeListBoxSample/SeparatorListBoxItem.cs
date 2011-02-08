using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Presentation.Media;

namespace Kuehner.SPOT.Presentation.Controls
{
    public class SeparatorListBoxItem : ListBoxItem
    {
        public SeparatorListBoxItem()
        {
            Rectangle rect = new Rectangle();
            rect.Height = 1;
            rect.Stroke = new Pen(Colors.Gray);
            this.Child = rect;
            this.IsSelectable = false;
            this.SetMargin(2);
        }
    }
}
