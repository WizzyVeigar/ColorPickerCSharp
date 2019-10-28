using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace ColorPicker_Demo
{
    public class Sorter
    {
        static readonly string colorLib = @"home / pi / images / ColorLib"; //"..\\..\\ColorLib\\";
        public string theCOLOR;

        //All lists contain the pixels of the ColorLib images, of which color they refer to
        private static List<Color> redList = new List<Color>();
        public static List<Color> RedList
        {
            get { return redList; }
            set { redList = value; }
        }

        private static List<Color> orangeList = new List<Color>();
        public static List<Color> OrangeList
        {
            get { return orangeList; }
            set { orangeList = value; }
        }

        private static List<Color> yellowList = new List<Color>();
        public static List<Color> YellowList
        {
            get { return yellowList; }
            set { yellowList = value; }
        }

        private static List<Color> greenList = new List<Color>();
        public static List<Color> GreenList
        {
            get { return greenList; }
            set { greenList = value; }
        }

        private static List<Color> blueList = new List<Color>();
        public static List<Color> BlueList
        {
            get { return blueList; }
            set { blueList = value; }
        }

        private static List<Color> brownList = new List<Color>();
        public static List<Color> BrownList
        {
            get { return brownList; }
            set { brownList = value; }
        }

        /// <summary>
        /// Finds the difference between <paramref name="c1"/> and <paramref name="c2"/>
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>Returns how close the two colors are related as an int</returns>
        int ColorDiff(Color c1, Color c2)
        {
            double distance = Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2);
            return (int)Math.Sqrt(distance);
        }
        /// <summary>
        /// Finds the closest color in <paramref name="colors"/> compared to <paramref name="target"/>
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="target"></param>
        /// <returns>Returns the index of the color that is closest to the <paramref name="target"/>, from <paramref name="colors"/></returns>
        public int ClosestColorTo(List<Color> colors, Color target)
        {
            int colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
            return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
        }
        /// <summary>
        /// Gets the closest color from each list and places them in closestColors 
        /// </summary>
        /// <param name="compareColor"></param>
        /// <returns>Returns the name of the color list</returns>
        public string ClosestColors(Color compareColor)
        {
            List<Color> closestColors = new List<Color>
            {
                Color.FromArgb(RedList[ClosestColorTo(RedList, compareColor)].ToArgb()),
                Color.FromArgb(OrangeList[ClosestColorTo(OrangeList, compareColor)].ToArgb()),
                Color.FromArgb(YellowList[ClosestColorTo(YellowList, compareColor)].ToArgb()),
                Color.FromArgb(GreenList[ClosestColorTo(GreenList, compareColor)].ToArgb()),
                Color.FromArgb(BlueList[ClosestColorTo(BlueList, compareColor)].ToArgb()),
                Color.FromArgb(BrownList[ClosestColorTo(BrownList, compareColor)].ToArgb())
            };

            return AssignToColor(ClosestColorTo(closestColors, compareColor));
        }

        /// <summary>
        /// Assigns what color <see cref="theCOLOR"/> belongs to
        /// </summary>
        /// <param name="indexOfCC"></param>
        /// <returns>returns a string depending on <paramref name="indexOfCC"/></returns>
        string AssignToColor(int indexOfCC)
        {
            switch (indexOfCC)
            {
                case 0:
                    theCOLOR = "Red";
                    return "Red";
                case 1:
                    theCOLOR = "Orange";
                    return "Orange";
                case 2:
                    theCOLOR = "Yellow";
                    return "Yellow";
                case 3:
                    theCOLOR = "Green";
                    return "Green";
                case 4:
                    theCOLOR = "Blue";
                    return "Blue";
                case 5:
                    theCOLOR = "Brown";
                    return "Brown";
            }
            return null;
        }
        /// <summary>
        /// Loads the pixels of the pictures in colorLib, into their respective lists
        /// </summary>
        public static void MakeLists()
        {
            if (Directory.Exists(colorLib) == true)
            {
                foreach (string file in Directory.EnumerateFiles(Path.GetFullPath(colorLib), "*.*", SearchOption.AllDirectories))
                {
                    if (file.Contains("red"))
                    {
                        AddPixelColor(redList, file);
                    }
                    if (file.Contains("orange"))
                    {
                        AddPixelColor(orangeList, file);
                    }
                    if (file.Contains("yellow"))
                    {
                        AddPixelColor(yellowList, file);
                    }
                    if (file.Contains("green"))
                    {
                        AddPixelColor(greenList, file);
                    }
                    if (file.Contains("blue"))
                    {
                        AddPixelColor(blueList, file);
                    }
                    if (file.Contains("brown"))
                    {
                        AddPixelColor(brownList, file);
                    }
                }
            }
        }
        public static void AddPixelColor(List<Color> list, string file)
        {
            Bitmap bitmap = new Bitmap(file);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    list.Add(Color.FromArgb(bitmap.GetPixel(i, j).ToArgb()));
                }
            }
        }
    }
}
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
