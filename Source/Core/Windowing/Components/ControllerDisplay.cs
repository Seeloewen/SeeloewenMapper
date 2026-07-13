using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeeloewenMapper.Core.Windowing.Components
{
    public class ControllerDisplay : Canvas
    {
        private TextBlock tblIdentifier = new TextBlock() { FontSize = 16 };

        public ControllerDisplay(int id)
        {
            Width = 758;
            Height = 50;
            Background = new SolidColorBrush(Colors.AliceBlue);

            SetTop(tblIdentifier, 5);
            SetLeft(tblIdentifier, 5);
            tblIdentifier.Text = $"Controller #{id}";
            Children.Add(tblIdentifier);
        }
    }
}
