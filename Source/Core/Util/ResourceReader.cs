using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SeeloewenMapper.Core.Util
{
    public static class ResourceReader
    {
        public static BitmapImage GetImage(string resource)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Resources/{resource}"));
        }

        public static string GetText(string resource)
        {
            var uri = new Uri($"pack://application:,,,/Resources/{resource}");
            var info = Application.GetResourceStream(uri);

            using var reader = new StreamReader(info.Stream);
            return reader.ReadToEnd();
        }
    }
}
