using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ArduinoColorPicker
{
    public class Sorter
    {

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
        /// Finds the closest color on <paramref name="colors"/> compared to <paramref name="target"/> without the use of Lambdas
        /// </summary>
        /// <Returns>the index of the color that is closest to the <paramref name="target"/>, from <paramref name="colors"/></returns>
        public int ClosestColorTo(List<Color> colors, Color target)
        {
            int iIndex = 0;
            int index = int.MaxValue;

            for (int i = 0; i < colors.Count; i++)
            {
                if (index > ColorDiff(colors[i], target))
                {

                    iIndex = i;
                    index = ColorDiff(colors[i], target);
                }
            }
            return iIndex;
        }
        /// <summary>
        /// Gets the closest color from each list and places them in closestColors 
        /// </summary>
        /// <param name="compareColor"></param>
        /// <returns>Returns the name of the color list</returns>
        public string ClosestColors(Color compareColor)
        {
            List<Color> closestColorsWithNoLambdas = new List<Color>
            {
                Color.FromArgb(RedList[ClosestColorTo(RedList, compareColor)].ToArgb()),
                Color.FromArgb(OrangeList[ClosestColorTo(OrangeList, compareColor)].ToArgb()),
                Color.FromArgb(YellowList[ClosestColorTo(YellowList, compareColor)].ToArgb()),
                Color.FromArgb(GreenList[ClosestColorTo(GreenList, compareColor)].ToArgb()),
                Color.FromArgb(BlueList[ClosestColorTo(BlueList, compareColor)].ToArgb()),
                Color.FromArgb(BrownList[ClosestColorTo(BrownList, compareColor)].ToArgb())
            };
            return AssignToColor(ClosestColorTo(closestColorsWithNoLambdas, compareColor));
        }

        /// <summary>
        /// Assigns what color <see cref="theCOLOR"/> belongs to
        /// </summary>
        /// <param name="indexOfCC"></param>
        /// <returns>returns a string depending on <paramref name="indexOfCC"/></returns>
        private string AssignToColor(int indexOfCC)
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
            string colorLib = @"/home/pi/images/ColorLib"; //"..\\..\\ColorLib";
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
            using (Bitmap bitmap = new Bitmap(file))
            {
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
}
