using System.Drawing;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace ArduinoColorPicker
{
    public class Picture
    {
        //Put global variables here
        //Local variables are in the top of a method!

        public Sorter sorter = new Sorter();

        // Path to image that cameraPicture.Py created
        private readonly string path = @"/home/pi/images/newImage.png";

        public string Path
        {
            get { return path; }            
        }

        private Image pictureTaken;

        public Image PictureTaken
        {
            get { return pictureTaken; }
            set { pictureTaken = value; }
        }

        public Picture()
        {
        }
        

        //Takes the actual picture
        public Image TakePicture() 
        {            
            bool foundImage = false;

            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    // File is the the python install folder
                    FileName = @"/usr/bin/python3",
                    // File where our python script is
                    Arguments = @"/home/pi/images/cameraPicture.py",

                    UseShellExecute = false,
                    RedirectStandardOutput = false
                };

                //Console.WriteLine("Starting Process");
                Process process = Process.Start(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            while (foundImage == false)
            {
                try
                {
                    if (File.Exists(Path))
                    {
                        PictureTaken = Image.FromFile(Path);
                        foundImage = true;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }
            }
            return PictureTaken;
        }
    }
}



