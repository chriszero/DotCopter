using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace Kuehner.SPOT.Presentation.Controls
{
    public class HighlightableTextListBoxItem : HighlightableListBoxItem
    {
        private readonly Text text;

        public HighlightableTextListBoxItem(Font font, string content) : base()
        {
            // create and remember a text element from the given font and text content
            this.text = new Text(font, content);
            this.text.SetMargin(2); // set the margin for the text
            this.Child = this.text; // add as child content
        }

        protected override void OnIsSelectedChanged(bool isSelected)
        {
            if (isSelected)
            {
                Color selectionColor = ColorUtility.ColorFromRGB(0x00, 0x94, 0xFF);
                this.Background = new SolidColorBrush(selectionColor);
                this.text.ForeColor = Color.White;
            }
            else
            {
                this.Background = null;
                this.text.ForeColor = Color.Black;
            }
        }
    }
}
