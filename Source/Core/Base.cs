using Nefarius.ViGEm.Client;
using SeeloewenMapper.Core.Controller;
using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing;

namespace SeeloewenMapper.Core
{

    internal static class Base
    {
        public static ViGEmClient? vigemClient;

        public const string VERSION = "0.1.0";
        public const string VERSION_DATE = "20.05.2026";

        public static void Init()
        {
            Log.Init();
            Log.Info($"SeeloewenMapper Version {VERSION} ({VERSION_DATE})");

            vigemClient = new ViGEmClient();
            ConnectionHandler.Init();

            WindowManager.Init();
            WindowManager.wndMain.Show();

        }

        public static void Exit()
        {
            Log.Shutdown();

            //Disconnect every virtual controller before exiting
            foreach(var controller in ConnectionHandler.controllers)
            {
                controller.Value.Disconnect();
            }
        }
    }
}
