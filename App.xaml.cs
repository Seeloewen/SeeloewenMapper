using System.Configuration;
using System.Data;
using System.Windows;

namespace SeeloewenMapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Base.Init();
        }

        private void Application_Exit(object sender, StartupEventArgs e)
        {
            Base.Exit();
        }
    }
}
