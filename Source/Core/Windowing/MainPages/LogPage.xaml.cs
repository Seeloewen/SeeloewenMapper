using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Source.Core;
using System;
using System.Collections.Generic;
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

namespace SeeloewenMapper.Core.Windowing.MainPages
{
    
    public partial class LogPage : Page
    {
        private readonly Dictionary<string, SolidColorBrush> prefixColorMap = new()
        {
            {"INFO", new SolidColorBrush(Colors.Blue) },
            {"WARNING", new SolidColorBrush(Colors.DarkOrange) },
            {"ERROR", new SolidColorBrush(Colors.Red) }
        };

        public LogPage()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(TextBlock item in stpLog.Children)
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

        public void LogMessage(string text, string dateTime, string prefix, bool isVerbose)
        {
            TextBlock tbMessage = new TextBlock() { Text = $"[{dateTime}] [{prefix}] {text}", Foreground = prefixColorMap[prefix], TextWrapping = TextWrapping.Wrap };

            if (isVerbose && cbVerboseMessages.IsChecked == false) return;
            
            stpLog.Children.Add(tbMessage);
            if (stpLog.Children.Count > 1024) stpLog.Children.RemoveAt(0);
        }

        private void cbVerboseMessages_Click(object sender, RoutedEventArgs e) => ViewModel.UpdateLog();
    }
}
