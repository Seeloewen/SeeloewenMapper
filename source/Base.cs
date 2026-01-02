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
    }
}
