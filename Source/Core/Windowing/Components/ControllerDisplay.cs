using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeeloewenMapper.Core.Windowing.Components
{
    public class ControllerDisplay : Canvas
    {
        private TextBlock tblIdentifier = new TextBlock();

        public ControllerDisplay(int id)
        {
            Width = 400;
            Height = 75;
            Background = new SolidColorBrush(Colors.AliceBlue);

            tblIdentifier.Text = $"Controller #{id}";
            Children.Add(tblIdentifier);
        }
    }
}
