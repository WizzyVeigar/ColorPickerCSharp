using System.IO.Ports;
using System.Threading;
using System;

namespace ColorPicker_Demo
{
    public class Messenger
    {
        static SerialPort seriPort = new SerialPort("/dev/ttyACM0");
        static string inputArm;
        static Thread tr = new Thread(ListenToState);
        public static event EventHandler StopArm;
        public static event EventHandler StartArm;
        public static bool listening = false;

        /// <summary>
        /// Open port, strart listening and 
        /// stard thread if nessesary
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

                // if message contains "t" stop listening  
                // and stop the program
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

        //RESETS THE ARM
        public static void RestartArm()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("r");
                if (!listening)
                {
                    seriPort.Close();
                    tr.Start();
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
