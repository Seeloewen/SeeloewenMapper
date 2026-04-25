using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing;
using System.Collections.ObjectModel;
using System.Windows;

namespace SeeloewenMapper.Core
{
    public static class ViewModel //Interface between data and windowing
    {
        public static void UpdateLog()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                WindowManager.wndMain?.logPage.stpLog.Children.Clear();
                List<LogMessage> messages = Log.GetMessages().ToList();
                foreach (LogMessage message in messages)
                {
                    WindowManager.wndMain?.logPage.LogMessage(message.message, message.dateTime.ToString(), message.level.ToString(), message.verbose);
                }
            });
        }
    }
}
