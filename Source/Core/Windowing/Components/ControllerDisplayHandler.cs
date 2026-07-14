using System.Windows;

namespace SeeloewenMapper.Core.Windowing.Components
{
    public static class ControllerDisplayHandler
    {
        public static Dictionary<string, ControllerDisplay> controllerDisplays = new Dictionary<string, ControllerDisplay>();

        public static async void Add(string devicePath, int id)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ControllerDisplay display = new ControllerDisplay(id);
                controllerDisplays.Add(devicePath, display);
                ViewModelMain.UpdateControllerDisplay();
            });
        }

        public static async void Remove(string devicePath)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                controllerDisplays.Remove(devicePath);
                ViewModelMain.UpdateControllerDisplay();
            });
        }

        public static void Update(string devicePath)
        {
            //tbd
        }
    }
}
