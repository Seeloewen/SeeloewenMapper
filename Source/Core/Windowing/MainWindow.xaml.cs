using SeeloewenMapper.Core;
using SeeloewenMapper.Core.Windowing.MainPages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeeloewenMapper
{
    public partial class MainWindow : Window
    {

        public readonly LogPage logPage = new LogPage();
        public readonly HomePage homePage = new HomePage();

        public MainWindow()
        {
            InitializeComponent();

            tblHeader.Text = $"SeeloewenMapper Version {Base.VERSION} ({Base.VERSION_DATE})";
            frLog.Navigate(logPage);
            frHome.Navigate(homePage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Base.Exit();
        }
    }
}