using HidSharp;
using HidSharp.Reports;
using SeeloewenMapper.Core.Logging;

namespace SeeloewenMapper.Core.Controller
{
    internal static class ConnectionHandler
    {
        public static Dictionary<string, Controller> controllers;
        private static bool skipNextConnection = false; //Used when connecting virtual devices to avoid duplicate OnConnect calls

        public static void Init()
        {
            controllers = new Dictionary<string, Controller>();

            OnConnect(); //Call once when starting the software to get already connected devices

            DeviceList.Local.Changed += (sender, e) => OnConnect();
        }

        public static void OnConnect() //Gets called when ANY device gets connected
        {
            if (skipNextConnection) return;

            Log.Info("Searching for controllers...");

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
                                //Even though were using a dictionary and can handle duplicates, we don't want to show a duplicate connection info
                                if (controllers.ContainsKey(d.DevicePath)) continue; 

                                Log.Info($"Detected new controller (Name: {d.GetProductName()}, VID: 0x{d.VendorID:X4}, PID: 0x{d.ProductID:X4}, DevicePath: {d.DevicePath.ToLowerInvariant()})");                         
                                controllers.Add(d.DevicePath, new Controller(d));
                                skipNextConnection = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn($"Warning: Could not get Report Descriptor for device {d.GetProductName()}. It cannot be determined if it's a controller. {ex.Message}", true);
                }
            }

            Log.Info($"Search completed, {controllers.Count} controller(s) are currently connected.");
        }
    }
}
