using System.Drawing;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;

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
            engine.ExecuteFile(@"C:\HelloWorld.py");

            // Path to image that python created
            string path = @"/home/pi/images/newimage.jpg";
            //string path = @"c:\Orange2.png";
            Bitmap bigBoiImage = null;


            bool foundImage = false;

            while (foundImage == false)
            {
                if (File.Exists(path))
                {
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