using SeeloewenMapper.Core.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeeloewenMapper.Core.Windowing.MainPages
{

    public partial class LogPage : Page
    {
        private readonly Dictionary<string, SolidColorBrush> prefixColorMap = new()
        {
            {"INFO", new SolidColorBrush(Colors.Blue) },
            {"WARNING", new SolidColorBrush(Colors.DarkOrange) },
            {"ERROR", new SolidColorBrush(Colors.Red) },
            {"DEBUG", new SolidColorBrush(Colors.DarkCyan) }
        };

        public LogPage()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TextBlock item in stpLog.Children)
            {
                sb.AppendLine(item.Text);
            }

            Clipboard.SetText(sb.ToString());
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            stpLog.Children.Clear();
            Log.Clear();
        }

        public void LogMessage(string text, string extra, string dateTime, string prefix, bool isVerbose)
        {
            string message = $"[{dateTime}] [{prefix}] {text}";
            TextBlock tbMessage = new TextBlock() { Text = message, Foreground = prefixColorMap[prefix], TextWrapping = TextWrapping.Wrap };
            tbMessage.MouseDown += (sender, e) => WindowManager.ShowTextWindow("Log Entry", $"{text}\n{extra}");

            if (isVerbose && cbVerboseMessages.IsChecked == false) return; //Ignore verbose messages if disabled

            stpLog.Children.Add(tbMessage);
            if (stpLog.Children.Count > 1024) stpLog.Children.RemoveAt(0);
        }

        private void cbVerboseMessages_Click(object sender, RoutedEventArgs e) => ViewModelMain.UpdateLog();
    }
}
