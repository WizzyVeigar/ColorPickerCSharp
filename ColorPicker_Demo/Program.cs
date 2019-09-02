using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ColorDifferentiater;
using System.Threading.Tasks;


namespace ColorPicker_Demo
{
    class Program
    {

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
        static void Main(string[] args)
        {
            ColorDifferantier differantier = new ColorDifferantier();
            ColorRange brown = new ColorRange(new CColor(50, 1, 1), new CColor(130, 60, 50), "Brown");
            ColorRange red = new ColorRange(new CColor(51, 0, 0), new CColor(255, 40, 75), "red");
            ColorRange orange = new ColorRange(new CColor(150, 63, 0), new CColor(255, 175, 35), "orange");
            ColorRange yellow = new ColorRange(new CColor(208, 208, 0), new CColor(255, 255, 230), "yellow");
            ColorRange green = new ColorRange(new CColor(0, 1, 0), new CColor(210, 255, 210), "green");
            ColorRange blue = new ColorRange(new CColor(50, 0, 0), new CColor(130, 60, 50), "blue");
            differantier.AddColorRange(brown);
            differantier.AddColorRange(red);
            differantier.AddColorRange(orange);
            differantier.AddColorRange(yellow);
            differantier.AddColorRange(green);
            differantier.AddColorRange(blue);


            string inputArg = "..\\..\\Sample\\";
            const int k = 3;
            //Looks for the sample folder in all directories 
            Console.WriteLine("Version 7.7.KMC");
            Console.Title = "R2.0 SSSorter";
            Console.WriteLine("Please wait a moment...");
            //Messenger.RestartArm();
            //Console.Clear();
            Console.WriteLine("R2.0 SSSorter" + "\n" + "\n" + "What would you like to do?");
            string input = Console.ReadLine().ToLower();
            if (input == "start" || input == "sort" || input == "s")
            {
                Console.WriteLine("What OS are you running?... (W) & (R)");
                input = Console.ReadLine().ToLower();
                if (input == "r")
                {
                    Picture pic = new Picture();
                    try
                    {
                        Console.WriteLine(differantier.GetColorName(pic.sorter.Classify(pic.ResizeImage(GetDominantColour(pic.Path, k))))); //THIS IS POTENTIAL BS
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                if (input == "w")
                {
                    if (Directory.Exists(inputArg) == true)
                    {
                        foreach (string file in Directory.EnumerateFiles(Path.GetFullPath(inputArg), "*.*", SearchOption.AllDirectories))
                        {
                            GetDominantColour(file, k);
                        }
                        return;
                    }
                    //Checks if the sample image is in the same directory as the program
                    if (File.Exists(inputArg) == true)
                    {
                        GetDominantColour(inputArg, k);
                    }

                }
            }

            Console.WriteLine("Unable to open {0}. Ensure it's a file or directory", inputArg);
        }

        private static Bitmap GetDominantColour(string inputFile, int k)
        {
            using (Image image = Image.FromFile(inputFile))
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

                    //Du kommer til at ende med et antal _colours lister alt efter antallet af K
                    //_colours indeholder så alle de farver som var determined at være tættest på closest cluster
                    //_colours beregner så det nye center for den cluster.English

                    //Writes to Console
                    Console.WriteLine("Dominant colours for {0}:", inputFile);
                    foreach (Color color in dominantColours)
                    {
                        Console.WriteLine("K: {0} (#{1:x2}{2:x2}{3:x2})", color, color.R, color.G, color.B);
                    }
                    //CColor dominantCColor = new CColor(dominantColours[0].R, dominantColours[0].G, dominantColours[0].G);

                    //Make a bar for the most dominant colors beneath the image
                    const int swatchHeight = 20;
                    using (Bitmap bmp = new Bitmap(resizedBitMapImage.Width, resizedBitMapImage.Height + swatchHeight))
                    {
                        using (Graphics gfx = Graphics.FromImage(bmp))
                        {
                            gfx.DrawImage(resizedBitMapImage, new Rectangle(0, 0, resizedBitMapImage.Width, resizedBitMapImage.Height));

                            //makes a block of each dominant color, based on K amount
                            int swatchWidth = (int)Math.Floor(bmp.Width / (k * 1.0f));
                            for (int i = 0; i < k; i++)
                            {
                                using (SolidBrush brush = new SolidBrush(dominantColours[i]))
                                {
                                    gfx.FillRectangle(brush, new Rectangle(i * swatchWidth, resizedBitMapImage.Height, swatchWidth, swatchHeight));
                                }
                            }
                        }
                        string outputFile = string.Format("{0}.output.png", Path.GetFileNameWithoutExtension(inputFile));
                        bmp.Save(outputFile, ImageFormat.Png);
                        Console.WriteLine("We made it to goal 1");
                        Console.ReadLine();
                        return new Bitmap(bmp); // THIS IS POTENTIAL BS

                        //Process.Start("explorer.exe", outputFile); //opens the newly created picture
                        //Messenger.SendToArm(pic.sorter.availableItems.Aggregate((next, biggest) => next.Value > biggest.Value? next : biggest).Key);


                    }
                }
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

