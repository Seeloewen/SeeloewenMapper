using SeeloewenMapper.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeeloewenMapper.Core.Windowing.Components
{
    public class ControllerDisplay : Canvas
    {
        private TextBlock tblIdentifier = new TextBlock() { FontSize = 16, FontWeight = FontWeights.DemiBold };
        private Image imgController = new Image() { Width = 40, Height = 40 };
        public ControllerDisplay(int id)
        {
            Width = 758;
            Height = 50;
            Background = new SolidColorBrush(Colors.AliceBlue);

            SetTop(tblIdentifier, 12);
            SetLeft(tblIdentifier, 60);
            tblIdentifier.Text = $"Controller #{id}";
            Children.Add(tblIdentifier);

            SetTop(imgController, 5);
            SetLeft(imgController, 5);
            imgController.Source = ResourceReader.GetImage("Controller.png");
Children.Add(imgController);
        }
    }
}
