using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing;
using System;
using System.Collections.Generic;
using System.Text;
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
                foreach (LogMessage message in Log.GetMessages())
                {
                    WindowManager.wndMain?.logPage.LogMessage(message.message, message.dateTime.ToString(), message.level.ToString(), message.verbose);
                }
            });
        }
    }
}
