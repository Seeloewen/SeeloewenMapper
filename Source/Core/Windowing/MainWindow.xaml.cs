using SeeloewenMapper.Core;
using SeeloewenMapper.Core.Windowing.MainPages;
using System.Windows;

namespace SeeloewenMapper
{
    public partial class MainWindow : Window
    {

        public readonly LogPage logPage = new LogPage();
        public readonly HomePage homePage = new HomePage();
        public readonly AboutPage aboutPage = new AboutPage();

        public MainWindow()
        {
            InitializeComponent();

            tblHeader.Text = $"SeeloewenMapper Version {Base.VERSION} ({Base.VERSION_DATE})";
            frLog.Navigate(logPage);
            frHome.Navigate(homePage);
            frAbout.Navigate(aboutPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Base.Exit();
        }
    }
}