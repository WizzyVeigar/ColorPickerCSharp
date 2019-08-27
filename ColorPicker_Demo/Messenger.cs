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
                        toRobo = "a";
                        seriPort.Open();
                        seriPort.Write(toRobo);
                        seriPort.Close();
                        break;
                    case "Orange":
                        toRobo = "b";
                        seriPort.Open();
                        seriPort.Write(toRobo);
                        seriPort.Close();
                        break;
                    case "Yellow":
                        toRobo = "c";
                        seriPort.Open();
                        seriPort.Write(toRobo);
                        seriPort.Close();
                        break;
                    case "Green":
                        toRobo = "d";
                        seriPort.Open();
                        seriPort.Write(toRobo);
                        seriPort.Close();
                        break;
                    case "Blue":
                        toRobo = "e";
                        seriPort.Open();
                        seriPort.Write(toRobo);
                        seriPort.Close();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
