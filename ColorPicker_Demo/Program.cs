﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#region pils
//                            |.
//                           ::.
//                           :::
//          ___              |::
//         `-._''--.._       |::
//             `-._   `-._.|::
//                `-._    `-::::
//                   `.     |:::.
//                     )    |::`:"-._ 
//                   <'   _.7  ::::::`-,.._
//                    `-.:        `` '::::::".
//                    .:'       .    .   `::::\
//                  .:'        .           .:::}
//               _.:'    __          .     :::/
// ,-.___,,..--'' --.""``  ``"".-- --,.._____.-.
//((___ """   -- ...     ....   __  ______  (D  )
// "-'`   ```''-.  __,,.......,,__      ::.  `-"
//               `<-....,,,,....-<   .:::'
//                 "._       ___,,._:::(
//                    ::--=''       `\:::.
//                   / :::'           `\::.
//        pils      / ::'               `\::
//                 / :'                   `\:
//                ( /                       `"
//                 "
#endregion

namespace ColorPicker_Demo
{
    class Program
    {
        //x MAKE STOP BUTTON
        //x IMPLEMENT STOP BUTTON
        //x CLEAN UP CODE + SEE IF ANY COMMENTS ARE NEEDED/MISSING

        const int k = 3;
        static void Main(string[] args)
        {
            string inputArg = "..\\..\\Sample\\"; //! CONTENTS ARE NOW M&M PICTURES!

            Sorter.MakeLists();
            //Looks for the sample folder in all directories 
            Console.WriteLine("Version 8.5.KMC");
            Console.Title = "R2.0 SSSorter";
            Console.WriteLine("Please wait a moment...");
            //Console.Clear();
            Console.WriteLine("R2.0 SSSorter" + "\n" + "\n" + "What would you like to do?");
            string input = Console.ReadLine().ToLower();
            if (input == "start" || input == "sort" || input == "s")
            {
                Console.WriteLine("What OS are you running?... (W) & (R)");
                input = Console.ReadLine().ToLower();
                if (input == "r")
                {
                    Messenger.OpenPort();
                    while (true)
                    {
                        Thread sortingProcessThread = new Thread(SortingProcess);

                        Console.WriteLine("Press button to start");
                        input = Messenger.StartProcess();
                        //Console.Clear();
                        if (input == "h")
                        {
                            sortingProcessThread.Start();
                        }

                        while (sortingProcessThread.IsAlive)
                        {
                            input = Messenger.StopProcess();

                            if (input == "t")
                            {
                                sortingProcessThread.Abort();
                                Messenger.RestartArm();
                            }
                        }
                    }
                }

                if (input == "w")
                {
                    Sorter.MakeLists();
                    if (Directory.Exists(inputArg) == true)
                    {
                        for (int i = 1; i < 51; i++)
                        {
                            Console.WriteLine("Test number {0}", i);
                            List<string> checkList = new List<string>()
                            {
                                "Brown", "Brown", "Green", "Green", "Blue", "Blue", "Orange", "Orange", "Orange", "Red", "Red", "Yellow", "Yellow"
                            };

                            List<string> results = new List<string>();
                            foreach (string file in Directory.EnumerateFiles(Path.GetFullPath(inputArg), "*.*", SearchOption.AllDirectories))
                            {
                                //results.Add(sorter.ClosestColors(GetDominantColour(file, k)));
                            }
                            if (!checkList.SequenceEqual(results))
                            {
                                Console.WriteLine(string.Join("\n", results));
                            }
                            Console.WriteLine("Done");
                        }
                    }
                    Console.ReadLine();
                }
            }
            Console.WriteLine("Unable to open {0}. Ensure it's a file or directory", inputArg);
        }

        public static void SortingProcess()
        {
            while (true)
            {
                Messenger.StartArm();
                Thread.Sleep(5000);
                Picture pic = new Picture();
                try
                {
                    Console.WriteLine("Sorting....");
                    Messenger.SendToArm(pic.sorter.ClosestColors(GetDominantColour(pic.image, k))); //x NEEDS FIXING!!
                    Console.WriteLine(pic.sorter.theCOLOR);
                    File.Delete(pic.Path);// Delete image to get ready for the next time
                    Console.WriteLine("Sorting done");
                    Thread.Sleep(6000);
                    //Console.ReadLine();
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
            Size resizedSize; //Resizes the image to suitable size
            if (image.Width > image.Height)
            {
                resizedSize = new Size(maxResizedDimension, (int)Math.Floor((image.Height / (image.Width * 1.0f)) * maxResizedDimension));
            }
            else
            {
                resizedSize = new Size((int)Math.Floor((image.Width / (image.Width * 1.0f)) * maxResizedDimension), maxResizedDimension);
                //If height > width = 200px,200px
            }

            using (Bitmap resizedBitMapImage = new Bitmap(image, resizedSize)) /*making it a bitmap*/
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
                KMeansClusteringCalculator clustering = new KMeansClusteringCalculator(); //Makes a KMC instance, so we can get calculate()
                IList<Color> dominantColours = clustering.Calculate(k, colors, 5.0d); //Math starts here / Check it out!

                //You will end up with a numbre of _colours lists depending on the numbers of K
                //_colour contains all the colour that were determined to be closest to the cluster
                //_colours calculate the new center for that cluster

                //Writes to Console
                //? DO WE NEED THIS?
                //Console.WriteLine("Dominant colours for {0}:", inputFile);
                //? AND THIS?
                //foreach (Color color in dominantColours)
                //{
                //    Console.WriteLine("K: {0} (#{1:x2}{2:x2}{3:x2})", color, color.R, color.G, color.B);
                //}
                //! NOT REALLY NEEDED
                //Make a bar for the most dominant colors beneath the image
                //const int swatchHeight = 20;
                //using (Bitmap bmp = new Bitmap(resizedBitMapImage.Width, resizedBitMapImage.Height + swatchHeight))
                //{
                //    using (Graphics gfx = Graphics.FromImage(bmp))
                //    {
                //        gfx.DrawImage(resizedBitMapImage, new Rectangle(0, 0, resizedBitMapImage.Width, resizedBitMapImage.Height));

                //        //makes a block of each dominant color, based on K amount
                //        int swatchWidth = (int)Math.Floor(bmp.Width / (k * 1.0f));
                //        for (int i = 0; i < k; i++)
                //        {
                //            using (SolidBrush brush = new SolidBrush(dominantColours[i]))
                //            {
                //                gfx.FillRectangle(brush, new Rectangle(i * swatchWidth, resizedBitMapImage.Height, swatchWidth, swatchHeight));
                //            }
                //        }
                //    }
                //    string outputFile = string.Format("{0}.output.png", Path.GetFileNameWithoutExtension(inputFile));
                //    bmp.Save(outputFile, ImageFormat.Png);
                //    Console.WriteLine("We made it to goal 1");
                //    Console.ReadLine();
                //    Process.Start("explorer.exe", outputFile); //opens the newly created picture
                Console.WriteLine(dominantColours[0]);
                return dominantColours[0]; //! THIS IS NO LONGER BS!
            }
        }
    }
}


#region mydumbattempt
//    try
//    {
//        Bitmap image = new Bitmap(@"C:\Users\Kenn5073\Desktop\RPB.png", false);
//        int x, y;
//        // Loop through the images pixels to reset color.
//        for (x = 0; x < image.Width; x++)
//        {
//            for (y = 0; y < image.Height; y++)
//            {
//                Color pixelColor = image.GetPixel(x, y);

//                switch (Classify(pixelColor))
//                {
//                    case "Blues":
//                        Console.WriteLine("it is blue");
//                        break;
//                    case "Reds":
//                        Console.WriteLine("this is red");
//                        break;
//                    case "Magentas":
//                        Console.WriteLine("Porple");
//                        break;
//                }
//            }
//        }
//    }

//    catch (Exception)
//    {
//        Console.WriteLine("Check your file path, dummy");    
//    }
//Console.ReadLine();

//    string Classify(Color c)
//    {
//        float hue = c.GetHue();
//        float sat = c.GetSaturation();
//        float lgt = c.GetBrightness();

//        if (lgt < 0.2) return "Blacks";
//        if (lgt > 0.8) return "Whites";

//        if (sat < 0.25) return "Grays";

//        if (hue < 30) return "Reds";
//        if (hue < 90) return "Yellows";
//        if (hue < 150) return "Greens";
//        if (hue < 210) return "Cyans";
//        if (hue < 270) return "Blues";
//        if (hue < 330) return "Magentas";
//        return "Reds";
//    }
#endregion
#region PeterWebsiteCode
//    public static string Calling(string hex)
//    {
//        try
//        {
//            if (hex.Contains("FF"))
//                hex = hex.Remove(0, 2);
//            string html = string.Empty;
//            hex = "https://www.htmlcsscolor.com/hex/" + hex;

//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hex);

//            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//            using (Stream stream = response.GetResponseStream())
//            using (StreamReader reader = new StreamReader(stream))
//            {
//                html = reader.ReadToEnd();
//            }
//            return html;
//        }
//        catch (Exception e)
//        {
//            return e.Message;
//        }
//    }

//    public static string GetBetween(string strSource, string strStart = "known color: ", string strEnd = ".")
//    {
//        int start, end;
//        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
//        {
//            start = strSource.IndexOf(strStart, 0) + strStart.Length;
//            end = strSource.IndexOf(strEnd, start);
//            //return strSource.Substring(Start, End - Start);
//            //double a = ConvertToCelsius(strSource.Substring(start, end - start));
//            return strSource.Substring(start, end - start);
//        }
//        else
//        {
//            return GetBetweenUnknowColor(strSource);
//        }
//    }

//    private static string GetBetweenUnknowColor(string strSource, string strStart = "approx ", string strEnd = ".")
//    {
//        int start, end;
//        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
//        {
//            start = strSource.IndexOf(strStart, 0) + strStart.Length;
//            end = strSource.IndexOf(strEnd, start);

//            return strSource.Substring(start, end - start);
//        }
//        else
//        {
//            return "Not Found";
//        }
//    }
//}

#endregion
#region notmydumbattemptthatisnotdone
//try
//{
//    Bitmap image = new Bitmap(@"C:\Users\Kenn5073\Desktop\RPB.png", false);
//    int x, y;
//    List<string> colorI = new List<string>(); //List for colors in the image
//    // Loop through the images pixels to get color.
//    for (x = 0; x < image.Width; x++)
//    {
//        for (y = 0; y < image.Height; y++)
//        {
//            Color pixelColor = image.GetPixel(x, y);

//            switch (Classify(pixelColor))
//            {
//                case "Blacks":
//                    if (colorI.Contains("Black") != true)
//                    {
//                        colorI.Add("Black");
//                    }
//                    break;
//                case "Whites":
//                    if (colorI.Contains("White") != true)
//                    {
//                        colorI.Add("White");
//                    }
//                    break;
//                case "Grays":
//                    if (colorI.Contains("Gray") != true)
//                    {
//                        colorI.Add("Gray");
//                    }
//                    break;
//                case "Reds":
//                    if (colorI.Contains("Red") != true)
//                    {
//                        colorI.Add("Red");
//                    }
//                    break;
//                case "Yellows":
//                    if (colorI.Contains("Yellow") != true)
//                    {
//                        colorI.Add("Yellow");
//                    }
//                    break;
//                case "Greens":
//                    if (colorI.Contains("Green") != true)
//                    {
//                        colorI.Add("Green");
//                    }
//                    break;
//                case "Cyans":
//                    if (colorI.Contains("Cyan") != true)
//                    {
//                        colorI.Add("Cyan");
//                    }
//                    break;
//                case "Blues":
//                    if (colorI.Contains("Blue") != true)
//                    {
//                        colorI.Add("Blue");
//                    }
//                    break;
//                case "Magentas":
//                    if (colorI.Contains("Purple") != true)
//                    {
//                        colorI.Add("Purple");
//                    }
//                    break;
//            }
//        }
//    }

//    Console.WriteLine(image.Height + " x " + image.Width);
//    Console.WriteLine(string.Join("\n", colorI));
//    //Console.WriteLine(" amount : " + FindAmount(colorI));
//    //Console.WriteLine(string.Join(colorI.FindAll(FindAmount(colorI))));

//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//    Console.WriteLine("Check your file path, dummy");
//}
//Console.ReadLine();

//string Classify(Color c)
//{
//    float hue = c.GetHue();
//    float sat = c.GetSaturation();
//    float lgt = c.GetBrightness();

//    if (lgt < 0.2) return "Blacks";
//    if (lgt > 0.8) return "Whites";

//    if (sat < 0.25) return "Grays";

//    if (hue < 30) return "Reds";
//    if (hue < 90) return "Yellows";
//    if (hue < 150) return "Greens";
//    if (hue < 210) return "Cyans";
//    if (hue < 270) return "Blues";
//    if (hue < 330) return "Magentas";
//    return "Reds";
//}

////string FindAmount(List<string> colors)
////{
////    List<string> colorsInPic = new List<string>();

////    foreach (string color in colors)
////    {
////        if (!colorsInPic.Contains(color))
////        {
////            colorsInPic.Add(color);
////        }
////        if()
////    }
////}
#endregion

