using SeeloewenMapper.Core.Controllers;
using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing;
using SeeloewenMapper.Core.Windowing.Components;
using System.Windows;

namespace SeeloewenMapper.Core
{
    public static class ViewModelMain //Interface between data and main window
    {
        private static Dictionary<string, ControllerDisplay> controllerDisplays = new Dictionary<string, ControllerDisplay>(); //String is DevicePath

        public static void UpdateLog()
        {
            //Needs invoke because it might be called from a different thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                WindowManager.wndMain?.logPage.stpLog.Children.Clear();
                List<LogMessage> messages = Log.GetMessages().ToList();
                foreach (LogMessage message in messages)
                {
                    WindowManager.wndMain?.logPage.LogMessage(message.message, message.extraContent, message.dateTime.ToString(), message.level.ToString(), message.verbose);
                }
            });
        }

        public static void UpdateControllerDisplay()
        {
            //Update controller displays based on the information from ControllerDisplayHandler
            var listbox = WindowManager.wndMain.homePage.lbControllers.Items;
            listbox.Clear();
            foreach (var item in ControllerDisplayHandler.controllerDisplays)
            {
                listbox.Add(item.Value);
            }
        }
    }
}
