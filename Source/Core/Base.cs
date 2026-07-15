using Nefarius.ViGEm.Client;
using SeeloewenMapper.Core.Controllers;
using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing;

namespace SeeloewenMapper.Core
{

    internal static class Base
    {
        public static ViGEmClient? vigemClient;

        public const string VERSION = "0.2.0";
        public const string VERSION_DATE = "15.07.2026";

        public static void Init()
        {
            Log.Init();
            Log.Info($"SeeloewenMapper Version {VERSION} ({VERSION_DATE})");

            WindowManager.Init();
            WindowManager.wndMain.Show();

            vigemClient = new ViGEmClient();
            ConnectionHandler.Init();
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
