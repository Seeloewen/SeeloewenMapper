using HidSharp;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using SeeloewenMapper.Core.Logging;
using System.ComponentModel;

namespace SeeloewenMapper.Core.Controller
{

    internal class Controller
    {
        public int id;
        private bool isConnected = false;

        private HidStream deviceStream;
        public string devicePath;
        private int maxInputReportLength = 0;
        private IXbox360Controller virtualDevice;

        public Controller(HidDevice device)
        {
            id = ConnectionHandler.nextId++;

            try
            {
                devicePath = device.DevicePath;
                deviceStream = device.Open();
                maxInputReportLength = device.GetMaxInputReportLength();
            }
            catch (Exception ex)
            {
                Log.Error($"Error while retrieving initial data from controller #{id}: {ex.Message}");
            }


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
                if (ex is Win32Exception wex && wex.NativeErrorCode == 0)
                {
                    Log.Debug($"Creation of virtual device for controller #{id} reported: {ex.Message}");
                }
                else
                {
                    Log.Error($"Error while connecting virtual device for controller #{id}: {ex.Message}");
                }
            }

            //Begin reading data from stream
            Thread t = new Thread(ReceiveData);
            t.Priority = ThreadPriority.Highest;
            t.Start();

            Log.Info($"Successfully connected and mapped controller #{id}.");
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
                    SetVirtualState(ControllerParser.FromDS4(buffer));
                }
                catch (Exception e)
                {
                    Log.Info($"Disconnected controller #{id}: {e.Message}");
                    OnDisconnect();
                    break;
                }
            }
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
