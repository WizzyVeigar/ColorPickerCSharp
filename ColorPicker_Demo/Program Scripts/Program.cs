using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Linq;

//CREDIT TO NANSYT AND WIZZYVEIGAR AS CO-OWNERS
//https://github.com/WizzyVeigar
//https://github.com/NansyT

//LAST PUBLIC USE WAS: 20/11-2019
namespace ArduinoColorPicker
{
    class Program
    {

        static bool change;
        const int k = 3;
        static Picture pic = new Picture();
        static Thread sortingProcessThread;
        static void Main(string[] args)
        {
            Messenger.StopArm += Messenger_StopArm;
            Messenger.StartArm += Messenger_StartArm;
            sortingProcessThread = new Thread(SortingProcess);
            Sorter.MakeLists();
            Console.Title = "R2.0 SSSorter";
            Console.WriteLine(
                "Version 9.2.KMC \n" +
                "R2.0 SSSorter" + "\n" + "\n" +
                "What would you like to do?");
            string input = Console.ReadLine().ToLower();
            if (input == "start" || input == "sort" || input == "s")
            {
                while (!Messenger.listening)
                {
                    //! MAKE IT LOOP UNTIL BUTTON IS PRESSED
                    Console.Clear();
                    Messenger.OpenPort();
                    Console.WriteLine("Press button to start");
                    Console.ReadLine();
                }

            }
        }

        private static void Messenger_StartArm(object sender, EventArgs e)
        {
            sortingProcessThread.Start();
        }

        /// <summary>
        /// Runs when message from port contais "t"
        /// kills the sorting thread and restarts the arm
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public static void Messenger_StopArm(object o, EventArgs e)
        {
            sortingProcessThread.Abort();
            Messenger.ResetArm();
        }

        public static void SortingProcess()
        {
            change = true;
            while (true)
            {
                if (change == true)
                {
                    Messenger.CollectRight();
                    change = false;
                    Thread.Sleep(9000);
                    pic.TakePicture();
                }
                else
                {
                    Messenger.CollectLeft();
                    change = true;
                    Thread.Sleep(14000);
                    pic.TakePicture();
                }

                try
                {
                    Messenger.SendToArm(pic.sorter.ClosestColors(GetDominantColour(pic.PictureTaken, k)));
                    Console.WriteLine(DateTime.Now + " " + pic.sorter.theCOLOR);
                    File.Delete(pic.Path);
                    // Delete image to get ready for the next time
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        private static Color GetDominantColour(Image image, int k)
        {
            const int maxResizedDimension = 200;
            //Resizes the image to suitable size
            Size resizedSize;
            if (image.Width > image.Height)
            {
                resizedSize = new Size(maxResizedDimension, (int)Math.Floor((image.Height / (image.Width * 1.0f)) * maxResizedDimension));
            }
            else
            {
                //If height > width = 200px,200px
                resizedSize = new Size((int)Math.Floor((image.Width / (image.Width * 1.0f)) * maxResizedDimension), maxResizedDimension);
            }

            //making it a bitmap
            using (Bitmap resizedBitMapImage = new Bitmap(image, resizedSize))
            {
                //The amount the list can hold is equal to the picture's squaremeters.
                List<Color> colors = new List<Color>(resizedBitMapImage.Width * resizedBitMapImage.Height);
                for (int x = 0; x < resizedBitMapImage.Width; x++)
                {
                    for (int y = 0; y < resizedBitMapImage.Height; y++)
                    {
                        colors.Add(resizedBitMapImage.GetPixel(x, y));
                    }
                }
                //Makes a KMC instance, so we can get calculate()
                KMeansClusteringCalculator clustering = new KMeansClusteringCalculator();
                //Math starts here!
                IList<Color> dominantColours = clustering.Calculate(k, colors, 5.0d);

                //You will end up with a number of _colours lists depending on the numbers of K
                //_colour contains all the colour that were determined to be closest to the cluster
                //_colours calculate the new center for that cluster
                Console.WriteLine(dominantColours[0]);
                return dominantColours[0];
            }
        }
    }
}