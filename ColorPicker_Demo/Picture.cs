using System.Drawing;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Collections.Generic;

namespace ColorPicker_Demo
{
    public class Picture
    {
        //  MMALCamera camera = MMALCamera.Instance;

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
            ScriptEngine engine = Python.CreateEngine();
            try
            {
                Console.WriteLine("Starting python script");

                ICollection<string> Paths = engine.GetSearchPaths();
                Paths.Add(@"/usr/lib/python3/dist-packages/");
                Paths.Add(@"/usr/lib/python3/dist-packages/picamera");
                Paths.Add(@"/usr/lib/python2.7");
                Paths.Add(@"/usr/lib/python3/dist-packages/picamera/__pycache__");
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"\IronPython.2.7.9\lib");
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"\IronPython.2.7.9");
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"\IronPython.27\lib");
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + @"\IronPython.27");
                Paths.Add(@"C:\Users\Kenn5073\source\repos\ColorPicker_Demo\packages\IronPython.2.7.9\lib");
                Paths.Add(@"IronPython.2.7.9/lib");
                engine.SetSearchPaths(Paths);
                engine.ExecuteFile(@"/home/pi/images/cameraPicture.py");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Path to image that python created
            string path = @"/home/pi/images/newimage.jpg";
            //string path = @"c:\Orange2.png";
            Bitmap bigBoiImage = null;


            bool foundImage = false;

            while (foundImage == false)
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("File found");
                    bigBoiImage = new Bitmap(path);

                    foundImage = true;
                }
            }

            // Delete image to get ready for the next time
            //File.Delete(path);
            return bigBoiImage;
        }
    }
}