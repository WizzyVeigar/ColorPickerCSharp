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

        public static string StartProcess() //! STARTS THE ARM!
        {
            while (true)
            {
                seriPort.Close();
                seriPort.Open();
                string a = seriPort.ReadExisting();

                if (a.Trim().Contains("h"))
                {
                    seriPort.Close();
                    return "h";
                }
                return "";
            }
        }
        public static string StopProcess()
        {
            while (true)
            {
                seriPort.Close();
                seriPort.Open();
                string a = seriPort.ReadExisting();

                if (a.Contains("t"))
                {
                    seriPort.Close();
                    return "t";
                }
                return "";
            }
        }

        public static void StartArm()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("s");
                seriPort.Close();
            }
        }
        public static void RestartArm()
        {
            if (seriPort.IsOpen == true)
            {
                seriPort.Write("r");
                seriPort.Close();
            }
        }

        public static void SendToArm(string theCOLOR)
        {
            if (!seriPort.IsOpen == true)
            {
                switch (theCOLOR)
                {
                    case "Red":
                        seriPort.Open();
                        seriPort.Write("a");
                        seriPort.Close();
                        break;
                    case "Orange":
                        seriPort.Open();
                        seriPort.Write("b");
                        seriPort.Close();
                        break;
                    case "Yellow":
                        seriPort.Open();
                        seriPort.Write("c");
                        seriPort.Close();
                        break;
                    case "Green":
                        seriPort.Open();
                        seriPort.Write("d");
                        seriPort.Close();
                        break;
                    case "Blue":
                        seriPort.Open();
                        seriPort.Write("e");
                        seriPort.Close();
                        break;
                    case "Brown":
                        seriPort.Open();
                        seriPort.Write("f");
                        seriPort.Close();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
