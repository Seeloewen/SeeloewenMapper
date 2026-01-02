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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Canvas> cvsControllerDisplays = new List<Canvas>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowController(string id)
        {
            
        }

        public void HideController(string id)
        {

        }


        public void Log(string text)
        {
           Application.Current.Dispatcher.Invoke(() => rtbLog.AppendText("[" + DateTime.Now.ToString() + "] " + text + "\n"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}