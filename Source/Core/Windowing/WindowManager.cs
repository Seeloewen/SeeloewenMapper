namespace SeeloewenMapper.Core.Windowing
{
    public static class WindowManager
    {
        public static MainWindow? wndMain;

        public static void Init()
        {
            wndMain = new MainWindow();

            ViewModelMain.UpdateLog();
        }

        public static void ShowTextWindow(string header, string content)
        {
            wndTextContent wnd = new wndTextContent();
            wnd.Title = header;
            wnd.tblHeader.Text = header;
            wnd.tbContent.Text = content;
            wnd.Show();
        }

    }
}
