using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SeeloewenMapper.Core.Logging
{
    public readonly record struct LogMessage(string message, DateTime dateTime, LogLevel level, bool verbose);

    public static class Log
    {
        private static ObservableCollection<LogMessage> messages = new ObservableCollection<LogMessage>();

        public static ObservableCollection<LogMessage> GetMessages() => messages;

        public static void Init()
        {
            messages.CollectionChanged += OnLogUpdated;
        }

        public static void Clear() => messages.Clear();

        public static void Info(string message, bool verbose = false)
        {
            messages.Add(new LogMessage(message, DateTime.Now, LogLevel.INFO, verbose));
        }

        public static void Warn(string message, bool verbose = false)
        {
            messages.Add(new LogMessage(message, DateTime.Now, LogLevel.WARNING, verbose));
        }

        public static void Error(string message, bool verbose = false)
        {
            messages.Add(new LogMessage(message, DateTime.Now, LogLevel.ERROR, verbose));
        }

        public static void OnLogUpdated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (messages.Count > 1024)
            {
                messages.RemoveAt(0);
            }

            ViewModel.UpdateLog();
        }
    }
}
