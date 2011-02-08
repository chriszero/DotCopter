using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation;

namespace Kuehner.SPOT.Presentation.Controls
{
    public class HighlightableListBoxItem : ListBoxItem
    {
        public HighlightableListBoxItem()
        {
            this.Background = null;
        }
        
        public HighlightableListBoxItem(UIElement content) 
        {
            this.Child = content;
            this.Background = null;
        }

        protected override void OnIsSelectedChanged(bool isSelected)
        {
            if (isSelected)
            {
                Color selectionColor = ColorUtility.ColorFromRGB(0x00, 0x94, 0xFF);
                this.Background = new SolidColorBrush(selectionColor);
            }
            else
                this.Background = null;
        }
    }
}
