using HidSharp;
using HidSharp.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeloewenMapper.core
{
    internal static class ConnectionHandler
    {
        public static Dictionary<string, Controller> controllers;

        public static void Init()
        {
            controllers = new Dictionary<string, Controller>();

            OnConnect(); //Call once when starting the software to get already connected devices

            DeviceList.Local.Changed += (sender, e) => OnConnect();
        }

        public static void OnConnect() //Gets called when ANY device gets connected
        {
            Base.wndMain.Log("----- BEGINNING NEW SEARCH FOR CONTROLLERS -----");

            foreach (var d in DeviceList.Local.GetHidDevices())
            {
                if (d.ProductID == 0x028E && d.VendorID == 0x045E) continue; //Skip XBOX 360 Controllers
                if (controllers.ContainsKey(d.DevicePath)) continue;

                Base.wndMain.Log("-----------------");
                Base.wndMain.Log($"VID: 0x{d.VendorID:X4}");
                Base.wndMain.Log($"PID: 0x{d.ProductID:X4}");
                Base.wndMain.Log($"Name: {d.GetProductName()}");
                Base.wndMain.Log($"DevicePath: {d.DevicePath.ToLowerInvariant()}");

                try
                {
                    ReportDescriptor? inputDesc = d.GetReportDescriptor();
                    foreach (DeviceItem di in inputDesc.DeviceItems)
                    {                      
                        foreach (var val in di.Usages.GetAllValues())
                        {
                            ushort usagePage = (ushort)(val >> 16); //Last 16 bytes are usage page
                            ushort usage = (ushort)(val & 0xFFFF); //First 16 bytes are usage
                            Base.wndMain.Log($"DeviceItem: UsagePage=0x{usagePage:X4}, Usage=0x{usage:X4}");

                            if (usagePage == 0x01 && usage == 0x05) controllers.Add(d.DevicePath, new Controller(d));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not get Report Descriptor {ex.Message}");
                }
            }
        }
    }
}
