using System.IO.Ports;
using System.Threading;
using System;

namespace ArduinoColorPicker
{
    public class Messenger
    {
        static SerialPort seriPort = new SerialPort("/dev/ttyACM0"); //The serialport changes depending on what usb port it's connected
        static string inputArm;
        static Thread tr = new Thread(ListenToState);
        public static event EventHandler StopArm;
        public static event EventHandler StartArm;
        public static bool listening = false;

        /// <summary>
        /// Open port, start listening and 
        /// start thread if nessesary
        /// </summary>
        public static void OpenPort()
        {
            seriPort.Open();
            listening = true;
            if (tr.IsAlive == false)
                tr.Start();
        }

        /// <summary>
        /// Listen to input from usb
        /// </summary>
        private static void ListenToState()
        {
            while (listening)
            {
                inputArm = seriPort.ReadExisting();

                // if message contains "t" stop listening and stop the program
                switch (inputArm.Trim())
                {
                    case "t":
                        listening = false;
                        StopArm?.Invoke("", new EventArgs());
                        break;
                    case "h":
                        if (StartArm != null)
                            StartArm("", new EventArgs());
                        break;
                }
            }
        }


        public static void CollectRight()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("s");
            }
        }

        public static void CollectLeft()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("w");
            }
        }

        public static void ResetArm()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("r");
                if (!listening)
                {
                    seriPort.Close();
                }
            }
        }

        //SENDS THE COLOR TO THE ARM
        public static void SendToArm(string theCOLOR)
        {
            if (seriPort.IsOpen == true)
            {
                switch (theCOLOR)
                {
                    case "Red":
                        seriPort.Write("a");
                        break;
                    case "Orange":
                        seriPort.Write("b");
                        break;
                    case "Yellow":
                        seriPort.Write("c");
                        break;
                    case "Green":
                        seriPort.Write("d");
                        break;
                    case "Blue":
                        seriPort.Write("e");
                        break;
                    case "Brown":
                        seriPort.Write("f");
                        break;
                }
            }
        }
    }
}
