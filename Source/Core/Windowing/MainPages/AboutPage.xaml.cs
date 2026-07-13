using SeeloewenMapper.Core.Util;
using System.Windows.Controls;

namespace SeeloewenMapper.Core.Windowing.MainPages
{
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            tblVersion.Text = $"Version {Base.VERSION} ({Base.VERSION_DATE})";
        }

        private void btnLicense_Click(object sender, System.Windows.RoutedEventArgs e)
            => WindowManager.ShowTextWindow("License", ResourceReader.GetText("License.txt"));

        private void btnChangelog_Click(object sender, System.Windows.RoutedEventArgs e)
            => WindowManager.ShowTextWindow("Changelog", ResourceReader.GetText("Changelog.txt"));

        private void btnThirdParty_Click(object sender, System.Windows.RoutedEventArgs e)
            => WindowManager.ShowTextWindow("Third-Party Licenses", ResourceReader.GetText("Third-Party_Licenses.txt"));
    }
}
