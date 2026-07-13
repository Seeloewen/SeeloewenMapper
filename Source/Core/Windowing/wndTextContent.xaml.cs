using System.Windows;

namespace SeeloewenMapper.Core.Windowing
{
    public partial class wndTextContent : Window
    {
        public wndTextContent()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();
    }
}
