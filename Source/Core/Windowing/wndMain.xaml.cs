using SeeloewenMapper.Core;
using SeeloewenMapper.Core.Windowing.MainPages;
using System.Windows;

namespace SeeloewenMapper
{
    public partial class wndMain : Window
    {

        public readonly LogPage logPage = new LogPage();
        public readonly HomePage homePage = new HomePage();
        public readonly AboutPage aboutPage = new AboutPage();
        public readonly SettingsPage settingsPage = new SettingsPage();

        public wndMain()
        {
            InitializeComponent();

            tblHeader.Text = $"SeeloewenMapper {Base.VERSION}";
            frLog.Navigate(logPage);
            frHome.Navigate(homePage);
            frAbout.Navigate(aboutPage);
            frSettings.Navigate(settingsPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Base.Exit();
        }
    }
}