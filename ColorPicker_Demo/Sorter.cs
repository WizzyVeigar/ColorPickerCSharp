using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace ColorPicker_Demo
{
    public class Sorter
    {
        //x MAKE LISTS FOR THE REMAINING COLORS
        //x TAKE PICTURES OF REMAINING M&M
        //x CUT THOSE PICTURES
        //x ADD LISTS TO ClosestColors()
        //x SEE IF THE IFS IN MakeList() CAN BE MADE INTO METHOD
        //x MAYBE NEW PICTURES
        //x BROWN NEEDS FIXING, BROWN PICS ARE RED

        static readonly string colorLib = @"/home/pi/images/ColorLib";

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
        public string theCOLOR;

        int ColorDiff(Color c1, Color c2) //+ COULD POSSIBLY BE ERASED AND USE EuclideanDistance() FROM KCluster.cs INSTEAD!!!!
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }
        public int ClosestColorTo(List<Color> colors, Color target) //THIS IS THE METHOD THAT CALCULATES THE CLOSEST COLOR
        {
            int colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
            return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
        }

        public string ClosestColors(Color compareColor) //GETS THE CLOSEST COLOR FROM EACH COLOR LIST. THEN GETS THE CLOSEST COLOR FROM THOSE COLORS AND RETURNS THE NAME OF THE COLOR LIST
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

            return FindListName(closestColors[ClosestColorTo(closestColors, compareColor)]);
        }

       
        string FindListName(Color closestColor) //LOOK AT NAME
        {
            if (RedList.Contains(closestColor))
            {
                theCOLOR = "Red";
                return "Red";
            }
            if (OrangeList.Contains(closestColor))
            {
                theCOLOR = "Orange";
                return "Orange";
            }
            if (YellowList.Contains(closestColor))
            {
                theCOLOR = "Yellow";
                return "Yellow";
            }
            if (GreenList.Contains(closestColor))
            {
                theCOLOR = "Green";
                return "Green";
            }
            if (BlueList.Contains(closestColor))
            {
                theCOLOR = "Blue";
                return "Blue";
            }
            if (BrownList.Contains(closestColor))
            {
                theCOLOR = "Brown";
                return "Brown";
            }
            return null;
        }
        public static void MakeLists() //MAKES OUR LISTS CONTAINING OUR KNOW COLORS
        {
            if (Directory.Exists(colorLib) == true)
            {
                foreach (string file in Directory.EnumerateFiles(Path.GetFullPath(colorLib), "*.*", SearchOption.AllDirectories))
                {
                    Console.WriteLine("Found da lib :P");
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
