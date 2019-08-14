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
        public static void SendToArm(string theCOLOR)
        {
            SerialPort seriPort = new SerialPort("COM3");

            if (!seriPort.IsOpen == true)
            {
                string toRobo;
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
