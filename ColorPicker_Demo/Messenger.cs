using System.IO.Ports;

namespace ColorPicker_Demo
{
    static class Messenger
    {
        static SerialPort seriPort = new SerialPort("/dev/ttyACM0");
        static string inputArm;

        public static void OpenPort()
        {
            seriPort.Open();
        }

        //STARTS THE COLOR SORTING PROCESS
        public static string StartProcess() 
        {
            while (true)
            {
                inputArm = seriPort.ReadExisting();

                if (inputArm.Trim().Contains("h"))
                {
                    return "h";
                }
            }
        }

        //STOPS THE ARM
        public static string StopProcess() 
        {
            while (true)
            {
                inputArm = seriPort.ReadExisting();

                if (inputArm.Trim().Contains("t"))
                {
                    return "t";
                }
            }
        }

        //STARTS THE ARM
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
