using System;
using System.Collections.Generic;
using System.Text;

namespace SeeloewenMapper.Core.Windowing
{
    public static class WindowManager
    {
        public static MainWindow? wndMain;

        public static void Init()
        {
            wndMain = new MainWindow();

            ViewModel.UpdateLog();
        }
    }
}
