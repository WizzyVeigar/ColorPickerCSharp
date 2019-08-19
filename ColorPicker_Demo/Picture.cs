using System.Drawing;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

// _________         .    .
//(..       \_    ,  |\  /|
// \       0  \  /|  \ \/ /
//  \______    \/ |   \  /
//     vvvv\    \ |   /  |
//     \^^^^  ==   \_/   |
//      `\_   ===    \.  |
//      / /\_   \ /      |
//      |/   \_  \|      /
//             \________/snd

namespace ColorPicker_Demo
{
    public class Picture
    {
        public Sorter sorter = new Sorter();
        public Bitmap picBitMap;
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private Image pictureTaken;

        public Image PictureTaken
        {
            get { return pictureTaken; }
            set { pictureTaken = value; }
        }


        public Picture()
        {
            picBitMap = new Bitmap(TakePicture());
            sorter.Classify(picBitMap);
        }

        public Bitmap TakePicture() //Takes the actual picture
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                // File is the the python install folder
                //start.FileName = @"C:\Users\pete168s\AppData\Local\Programs\Python\Python36\python.exe";

                //start.FileName = @"/usr/share/rasp-ui-overrides/applications/IDLE (using Python-2.7).exe";
                start.FileName = @"/usr/bin/python3";

                // File where our python script is
                start.Arguments = @"/home/pi/images/cameraPicture.py";

                start.UseShellExecute = false;
                start.RedirectStandardOutput = false;

                Console.WriteLine("Starting Proccess");
                Process process = Process.Start(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #region IronPython
            //ScriptEngine engine = Python.CreateEngine();
            //try
            //{
            //    using (Process myProcess = new Process())
            //    {
            //        myProcess.StartInfo.UseShellExecute = false;
            //        // You can start any process, HelloWorld is a do-nothing example.
            //        myProcess.StartInfo.FileName = @"/home/pi/images/cameraPicture.py";
            //        myProcess.StartInfo.CreateNoWindow = true;
            //        Console.WriteLine("Starting python code");
            //        myProcess.Start();
            //        // This code assumes the process you are starting will terminate itself. 
            //        // Given that is is started without a window so you cannot terminate it 
            //        // on the desktop, it must terminate itself or you can do it programmatically
            //        // from this application using the Kill method.
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine(e);
            //}
            #endregion
            #region The good code

            //try
            //{
            //    Console.WriteLine("Starting python script");

            //    ICollection<string> Paths = engine.GetSearchPaths();
            //    Paths.Add(@"/usr/lib/python2.7/dist-packages/");
            //    //Paths.Add(@"/usr/lib/python2.7/dist-packages/picamera");
            //    //Paths.Add(@"/usr/lib/python2.7/dist-packages/picamera");
            //    //Paths.Add(@"/usr/lib/python2.7");
            //    //Paths.Add(@"/usr/lib/python3/dist-packages/picamera/__pycache__");
            //    Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"IronPython.2.7.9\lib");
            //    Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"IronPython.2.7.9");
            //    Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"IronPython.27\lib");
            //    Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"IronPython.27");

            //    Paths.Add(@"C:\Users\Kenn5073\source\repos\ColorPicker_Demo\packages\IronPython.2.7.9\lib");
            //    Paths.Add(@"IronPython.2.7.9/lib");
            //    Paths.Add(@"/usr/lib/python2.7/__future__.py");
            //    Paths.Add(@"/usr/lib/python2.7/__future__.pyc");

            //    Paths.Add(@"/home/pi/Desktop/IronPython2.7.9");

            //    engine.SetSearchPaths(Paths);
            //    Console.WriteLine("Before exec python");
            //    engine.ExecuteFile(@"/home/pi/images/cameraPicture.py");
            //    Console.WriteLine("file executed");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine(e);
            //}
            #endregion

            // Path to image that python created
            string path = @"/home/pi/images/newImage.png";
            //string path = @"c:\Orange2.png";
            Bitmap bigBoiImage = null;



            bool foundImage = false;

            while (foundImage == false)
            {
                //if (File.Exists(path))
                //{
                //        Thread.Sleep(30000);
                //    Console.WriteLine("File found");
                //    if(bigBoiImage == null)
                //    {
                //        Console.WriteLine("big boi image is null");
                //        bigBoiImage = new Bitmap(path);
                //    }
                //    else
                //    {
                //        Console.WriteLine("image in not null");
                //        foundImage = true;
                //    }
                //}
                //Thread.Sleep(1000);
                //Console.WriteLine("while");

                try
                {
                    bigBoiImage = new Bitmap(path);
                    Console.WriteLine("Found Image");
                    foundImage = true;
                }
                catch (Exception e)
                {
                    Thread.Sleep(3000);
                }
            }
            // Delete image to get ready for the next time
            //File.Delete(path);
            return bigBoiImage;
        }
    }
}



