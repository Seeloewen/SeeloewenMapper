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
        List<Canvas> cvsControllerDisplays = new List<Canvas>();

        public readonly LogPage logPage = new LogPage(); 

        public MainWindow()
        {
            InitializeComponent();

            tblHeader.Text = $"SeeloewenMapper Version {Base.VERSION} ({Base.VERSION_DATE})";
            frLog.Navigate(logPage);
        }

        public void ShowController(string id)
        {
            
        }

        public void HideController(string id)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Base.Exit();
        }
    }
}