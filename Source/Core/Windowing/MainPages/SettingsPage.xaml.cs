using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SeeloewenMapper.Core.Windowing.MainPages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Show the default gamecontroller dialog
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/C joy.cpl",
                CreateNoWindow = true
            };
            Process.Start(info);
        }
    }
}
