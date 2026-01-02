using Nefarius.ViGEm.Client;
using SeeloewenMapper.core;

namespace SeeloewenMapper
{

    internal static class Base
    {
        public static ViGEmClient vigemClient;
        public static MainWindow wndMain;

        public static void Init()
        {
            wndMain = new MainWindow();
            wndMain.Show();
            vigemClient = new ViGEmClient();
            ConnectionHandler.Init();
        }

        public static void Exit()
        {
            //Disconnect every virtual controller before exiting
            foreach(var controller in ConnectionHandler.controllers)
            {
                controller.Value.OnDisconnect();
            }
        }
    }
}
