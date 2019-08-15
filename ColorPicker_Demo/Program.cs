using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker_Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Version 0.4");
            Console.Title = "R2.0 SSSorter";
            try 
            {

                Console.WriteLine("R2.0 SSSorter" + "\n" + "\n" + "What would you like to do?");
                string input = Console.ReadLine().ToLower();
                if (input == "start" || input == "sort" || input == "s")
                {
                    Picture pic = new Picture();
                    Console.WriteLine("Width: " + pic.picBitMap.Width + "    Height: " + pic.picBitMap.Height);
                    Console.WriteLine(string.Join("\n", pic.sorter.GetAllColors()));
                    Messenger.SendToArm(pic.sorter.availableItems.Aggregate((next, biggest) => next.Value > biggest.Value ? next : biggest).Key);
                    Console.WriteLine("Sending data to arm...");
                }
                Console.WriteLine("Done");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
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

