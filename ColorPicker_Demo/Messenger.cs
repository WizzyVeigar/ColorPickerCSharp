using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ColorPicker_Demo
{
    static class Messenger
    {
        static SerialPort seriPort = new SerialPort("/dev/ttyACM0");
        static string toRobo;

        public static void RestartArm()
        {
            if (!seriPort.IsOpen == true)
            {
                seriPort.Open();
                toRobo = "r";
                seriPort.Write(toRobo);
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
