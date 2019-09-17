using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ColorPicker_Demo
{
    //x NEEDS A START ARM METHOD
    static class Messenger
    {
        static SerialPort seriPort = new SerialPort("/dev/ttyACM0");

        public static void OpenPort()
        {
            seriPort.Open();
        }

        public static string StartProcess() //STARTS THE ARM!
        {
            while (true)
            {
                string a = seriPort.ReadExisting();

                if (a.Trim().Contains("h"))
                {
                    return "h";
                }
            }
        }

        public static string StopProcess() //STOPS THE ARM
        {
            while (true)
            {
                string a = seriPort.ReadExisting();

                if (a.Contains("t"))
                {
                    return "t";
                }
            }
        }

        public static void StartArm() //STARTS THE ARM
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("s");
            }
        }

        public static void RestartArm() //RESETS THE ARM
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("r");
            }
        }

        public static void SendToArm(string theCOLOR) //SENDS THE COLOR TO THE ARM
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
                    default:
                        break;
                }
            }
        }
    }
}
