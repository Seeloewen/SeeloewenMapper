namespace SeeloewenMapper.Core.Windowing.Components
{
    public static class ControllerDisplayHandler
    {
        public static Dictionary<string, ControllerDisplay> controllerDisplays = new Dictionary<string, ControllerDisplay>();

        public static void Add(string devicePath, int id)
        {
            ControllerDisplay display = new ControllerDisplay(id);
            controllerDisplays.Add(devicePath, display);
            ViewModelMain.UpdateControllerDisplay();
        }

        public static void Remove(string devicePath)
        {
            controllerDisplays.Remove(devicePath);
            ViewModelMain.UpdateControllerDisplay();
        }

        public static void Update(string devicePath)
        {
            //tbd
        }
    }
}
