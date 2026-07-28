using HidSharp;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using SeeloewenMapper.Core.Logging;
using SeeloewenMapper.Core.Windowing.Components;
using System.ComponentModel;

namespace SeeloewenMapper.Core.Controllers
{

    internal class Controller
    {
        public int id;
        private bool isConnected = false;

        private HidStream deviceStream;
        public string devicePath;
        private int maxInputReportLength = 0;
        private int maxFeatureReportLength = 0;

        private IXbox360Controller virtualDevice;

        public Controller(HidDevice device)
        {
            id = ConnectionHandler.nextId++;

            try
            {
                devicePath = device.DevicePath;
                deviceStream = device.Open();
                maxInputReportLength = device.GetMaxInputReportLength();
                maxFeatureReportLength = device.GetMaxFeatureReportLength();

                EnableFullMode();
            }
            catch (Exception ex)
            {
                Log.Error($"Error while retrieving initial data from controller #{id}: {ex.Message}", extra: ex.StackTrace!);
            }

            CreateVirtualDevice();
            ControllerDisplayHandler.Add(devicePath, id);

            //Begin reading data from stream
            Thread t = new Thread(ReceiveData);
            t.Priority = ThreadPriority.Highest;
            t.Start();

            Log.Info($"Successfully connected and mapped controller #{id}.");
        }

        public void CreateVirtualDevice()
        {
            //Create virtual device
            try
            {
                virtualDevice = Base.vigemClient.CreateXbox360Controller();
                virtualDevice.Connect();
                virtualDevice.AutoSubmitReport = false;
                isConnected = true;
            }
            catch (Exception ex)
            {
                //This is a workaround for a weird case, where the virtual device fails to create correctly
                if (ex is Win32Exception wex && wex.NativeErrorCode == 0)
                {
                    Log.Debug($"Creation of virtual device for controller #{id} encountered an issue: {ex.Message} - Retrying...");
                    virtualDevice.Disconnect();
                    CreateVirtualDevice();
                }
                else
                {
                    Log.Error($"Error while connecting virtual device for controller #{id}: {ex.Message}", extra: ex.StackTrace!);
                }
            }
        }

        public void Disconnect()
        {
            isConnected = false;
        }

        private void OnDisconnect()
        {
            //This will be run when the controller is no longer connected
            virtualDevice.Disconnect();
            deviceStream.Dispose();
            ConnectionHandler.controllers.Remove(devicePath);
            isConnected = false;

            ControllerDisplayHandler.Remove(devicePath);
        }

        public void ReceiveData()
        {
            Log.Debug($"Beginning to receive data from controller #{id}");

            while (isConnected)
            {
                try
                {
                    byte[] buffer = new byte[maxInputReportLength];
                    int i = deviceStream.Read(buffer);
                    ConnectionMode m = GetConnectionMode(buffer[0]); //Retrieve the connection mode by checking the mode byte

                    if (m == ConnectionMode.INVALID)
                    {
                        Log.Error("Received a packet with an invalid connection mode. The connection may be corrupted or not supported. Skipping...");
                        return;
                    }

                    SetVirtualState(ControllerParser.FromDS4(buffer, m));
                }
                catch (Exception e)
                {
                    Log.Info($"Disconnected controller #{id}: {e.Message}");
                    OnDisconnect();
                    break;
                }
            }
        }

        private void EnableFullMode()
        {
            SetFeature(0x02); //Enable full mode for bt controllers

            //Get an initial report and check whether the correct mode is enabled
            byte[] buffer = new byte[maxInputReportLength];
            int i = deviceStream.Read(buffer);

            switch (GetConnectionMode(buffer[0]))
            {
                case ConnectionMode.BASIC:
                    Log.Debug($"Controller #{id} is now connected in basic mode. This is normal if the connection method is USB.");
                    break;
                case ConnectionMode.FULL:
                    Log.Debug($"Controller #{id} is now connected in full mode. This is normal if the connection method is Bluetooth.");
                    break;
                case ConnectionMode.INVALID:
                    throw new Exception("First bytes from the initial mode check were invalid. The connection may be faulty!");
            }
        }

        private ConnectionMode GetConnectionMode(byte b) => b switch
        {
            0x01 => ConnectionMode.BASIC,
            0x11 => ConnectionMode.FULL,
            _ => ConnectionMode.INVALID
        };

        public void SetFeature(int featureId)
        {
            byte[] buffer = new byte[maxFeatureReportLength];
            buffer[0] = (byte)featureId;

            deviceStream.GetFeature(buffer);
        }

        public void SetVirtualState(VirtualState state)
        {
            virtualDevice.SetButtonState(Xbox360Button.X, state.XPressed);
            virtualDevice.SetButtonState(Xbox360Button.A, state.APressed);
            virtualDevice.SetButtonState(Xbox360Button.B, state.BPressed);
            virtualDevice.SetButtonState(Xbox360Button.Y, state.YPressed);

            virtualDevice.SetAxisValue(Xbox360Axis.LeftThumbX, state.leftStickX);
            virtualDevice.SetAxisValue(Xbox360Axis.RightThumbX, state.rightStickX);
            virtualDevice.SetAxisValue(Xbox360Axis.LeftThumbY, state.leftStickY);
            virtualDevice.SetAxisValue(Xbox360Axis.RightThumbY, state.rightStickY);

            virtualDevice.SetButtonState(Xbox360Button.LeftShoulder, state.l1Pressed);
            virtualDevice.SetButtonState(Xbox360Button.RightShoulder, state.r1Pressed);

            virtualDevice.SetSliderValue(Xbox360Slider.RightTrigger, state.r2Value);
            virtualDevice.SetSliderValue(Xbox360Slider.LeftTrigger, state.l2Value);

            virtualDevice.SetButtonState(Xbox360Button.Up, state.upPressed);
            virtualDevice.SetButtonState(Xbox360Button.Down, state.downPressed);
            virtualDevice.SetButtonState(Xbox360Button.Left, state.leftPressed);
            virtualDevice.SetButtonState(Xbox360Button.Right, state.rightPressed);

            virtualDevice.SetButtonState(Xbox360Button.Start, state.startPressed);
            virtualDevice.SetButtonState(Xbox360Button.Back, state.backPressed);

            virtualDevice.SetButtonState(Xbox360Button.LeftThumb, state.l3Pressed);
            virtualDevice.SetButtonState(Xbox360Button.RightThumb, state.r3Pressed);

            virtualDevice.SubmitReport();
        }
    }
}