using HidSharp;
using HidSharp.Reports;
using SeeloewenMapper.Core.Logging;
using System.Text;

namespace SeeloewenMapper.Core.Controllers
{
    internal static class ConnectionHandler
    {
        public static int nextId = 0;

        public static Dictionary<string, Controller> controllers; //String is DevicePath
        private static bool skipNextConnection = false; //Used when connecting virtual devices to avoid duplicate OnConnect calls
        private static readonly object connectionLock = new object();

        public static void Init()
        {
            controllers = new Dictionary<string, Controller>();

            DeviceList.Local.Changed += (sender, e) => OnConnectDevice();

            OnConnectDevice(); //Call once when starting the software to get already connected devices
        }

        public static void OnConnectDevice() //Gets called when ANY device gets connected
        {
            lock (connectionLock)
            {
                if (skipNextConnection)
                {
                    skipNextConnection = false;
                    Log.Debug("Connection detected, skipping call to avoid recursive connections");
                    return;
                }

                Log.Info("Connection detected, searching for controllers...");
                foreach (var d in DeviceList.Local.GetHidDevices())
                {
                    if (d.ProductID == 0x028E && d.VendorID == 0x045E) continue; //Skip XBOX 360 Controllers
                    if (controllers.ContainsKey(d.DevicePath)) continue; //Skip already added controllers

                    try
                    {
                        ReportDescriptor? inputDesc = d.GetReportDescriptor();
                        foreach (DeviceItem di in inputDesc.DeviceItems)
                        {
                            foreach (var val in di.Usages.GetAllValues())
                            {
                                ushort usagePage = (ushort)(val >> 16); //Last 16 bytes are usage page
                                ushort usage = (ushort)(val & 0xFFFF); //First 16 bytes are usage

                                if (usagePage == 0x01 && usage == 0x05)
                                {
#if DEBUG
                                    LogReportDescriptions(di);
#endif
                                    //Even though were using a dictionary and can handle duplicates, we don't want to show a duplicate connection info
                                    if (controllers.ContainsKey(d.DevicePath)) continue;

                                    skipNextConnection = true;
                                    Log.Info($"Detected new controller #{nextId} (Name: {d.GetProductName()}, VID: 0x{d.VendorID:X4}, PID: 0x{d.ProductID:X4}, DevicePath: {d.DevicePath.ToLowerInvariant()})");
                                    controllers.Add(d.DevicePath, new Controller(d));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn($"Could not get Report Descriptor for device {d.GetProductName()}. It cannot be determined if it's a controller. {ex.Message}", extra: ex.StackTrace!, verbose: true);
                    }
                }

                Log.Info($"Search completed, {controllers.Count} controller(s) are currently connected.");
            }
        }

        public static void LogReportDescriptions(DeviceItem item)
        {
            //Constucts a log entry consisting of the report descriptors
            StringBuilder sb = new StringBuilder();
            foreach (Report r in item.Reports)
            {
                sb.AppendLine($"Descriptor: Id {r.ReportID}, Type {r.ReportType}, Length {r.Length}");
            }
            Log.Debug("Possible controller connected detected, listing report descriptors. [CLICK TO VIEW]", sb.ToString());

        }
    }
}
