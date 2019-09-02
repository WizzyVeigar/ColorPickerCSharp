using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Media;
using ColorDifferentiater;

namespace ColorPicker_Demo
{
    public class Sorter
    {
        public List<string> colorList = new List<string>();
        //public Dictionary<string, int> availableItems = new Dictionary<string, int>();
                          
        //List for colors in the image
        // Loop through the images pixels to get color.
        public CColor Classify(Bitmap bitMapPic)
        {
            var colorIncidence = new Dictionary<int, int>();
            for (int x = 0; x < bitMapPic.Size.Width; x++)
                for (int y = 0; y < bitMapPic.Size.Height; y++)
                {
                    var pixelColor = bitMapPic.GetPixel(x, y).ToArgb();
                    if (colorIncidence.Keys.Contains(pixelColor))
                        colorIncidence[pixelColor]++;
                    else
                        colorIncidence.Add(pixelColor, 1);
                }


            // colorIncidence.Keys.ElementAt(0)
            System.Drawing.Color a = System.Drawing.Color.FromArgb(colorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).First().Key);
            Console.WriteLine(a.R +" "+ a.G +" "+ a.B);
            Console.WriteLine(colorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).First().Key);
            CColor dominant = new CColor(a.R, a.G, a.B); //ThiS IS POTENTIAL BS
            Console.WriteLine("We made it to goal 3");
            Console.ReadLine();
            return dominant;
            #region BadSorter
            //for (int x = 0; x < bitMapPic.Width; x++)
            //{
            //    for (int y = 0; y < bitMapPic.Height; y++)
            //    {
            //        System.Drawing.Color pixelColor = bitMapPic.GetPixel(x, y);

            //        float hue = pixelColor.GetHue();
            //        float sat = pixelColor.GetSaturation();
            //        float lgt = pixelColor.GetBrightness();

            //        if (lgt < 0.2)
            //        {
            //            Hue black = new Hue("Black");
            //            colorList.Add(black.Name);
            //        }
            //        else if (lgt > 0.8)
            //        {
            //            Hue white = new Hue("White");
            //            colorList.Add(white.Name);
            //        }

            //        else if (sat < 0.25)
            //        {
            //            Hue gray = new Hue("Gray");
            //            colorList.Add(gray.Name);
            //        }

            //        else if (hue < 20)
            //        {
            //            Hue red = new Hue("Red");
            //            colorList.Add(red.Name);
            //        }
            //        else if (hue < 50 && lgt < 0.3)
            //        {
            //            Hue brown = new Hue("Brown");
            //            colorList.Add(brown.Name);
            //        }
            //        else if (hue < 50)
            //        {
            //            Hue orange = new Hue("Orange");
            //            colorList.Add(orange.Name);
            //        }
            //        else if (hue < 90)
            //        {
            //            Hue yellow = new Hue("Yellow");
            //            colorList.Add(yellow.Name);
            //        }
            //        else if (hue < 150)
            //        {
            //            Hue green = new Hue("Green");
            //            colorList.Add(green.Name);
            //        }
            //        else if (hue < 210)
            //        {
            //            Hue cyan = new Hue("Cyan");
            //            colorList.Add(cyan.Name);
            //        }
            //        else if (hue < 270)
            //        {
            //            Hue blue = new Hue("Blue");
            //            colorList.Add(blue.Name);
            //        }
            //        else if (hue < 330)
            //        {
            //            Hue magenta = new Hue("Magenta");
            //            colorList.Add(magenta.Name);
            //        }
            //        else
            //        {
            //            Hue ErrorColor = new Hue("ErrorColor");
            //            colorList.Add(ErrorColor.Name);
            //        }
            //    }
            //}
            //    //bitMapPic.Save(@"/home/pi/Desktop/BitmapImage.png", System.Drawing.Imaging.ImageFormat.Png);
            #endregion
        }
    }
}
