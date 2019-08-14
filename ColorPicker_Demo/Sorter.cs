using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ColorPicker_Demo
{
    public class Sorter
    {
        public List<string> colorList = new List<string>();
        public Dictionary<string, int> availableItems = new Dictionary<string, int>();

        //List for colors in the image
        // Loop through the images pixels to get color.
        public void Classify(Bitmap bitMapPic)
        {     
            for (int x = 0; x < bitMapPic.Width; x++)
            {
                for (int y = 0; y < bitMapPic.Height; y++)
                {
                    System.Drawing.Color pixelColor = bitMapPic.GetPixel(x, y);

                    float hue = pixelColor.GetHue();
                    float sat = pixelColor.GetSaturation();
                    float lgt = pixelColor.GetBrightness();

                    if (lgt < 0.2)
                    {
                        Color black = new Color("Black");
                        colorList.Add(black.Name);
                    }
                    else if (lgt > 0.8)
                    {
                        Color white = new Color("White");
                        colorList.Add(white.Name);
                    }

                    else if (sat < 0.25)
                    {
                        Color grey = new Color("Grey");
                        colorList.Add(grey.Name);
                    }

                    else if (hue < 20)
                    {
                        Color red = new Color("Red");
                        colorList.Add(red.Name);
                    }
                    else if (hue < 50)
                    {
                        Color orange = new Color("Orange");
                        colorList.Add(orange.Name);
                    }
                    else if (hue < 90)
                    {
                        Color yellow = new Color("Yellow");
                        colorList.Add(yellow.Name);
                    }
                    else if (hue < 150)
                    {
                        Color green = new Color("Green");
                        colorList.Add(green.Name);
                    }
                    else if (hue < 210)
                    {
                        Color cyan = new Color("Cyan");
                        colorList.Add(cyan.Name);
                    }
                    else if (hue < 270)
                    {
                        Color blue = new Color("Blue");
                        colorList.Add(blue.Name);
                    }
                    else if (hue < 330)
                    {
                        Color magenta = new Color("Magenta");
                        colorList.Add(magenta.Name);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// Creates a dictionary and inserts a ItemName once making it unique, also icreases the value by 1 after every duplicate ItemName that is found.
        /// <returns>Returns Dictionary availbleitems</returns>
        public Dictionary<string, int> GetAllColors()
        {

            foreach (string color in colorList)
            {
                bool itemExists = false;
                foreach (KeyValuePair<string, int> entry in availableItems)
                {
                    if (entry.Key == color)
                    {
                        itemExists = true;
                    }
                }

                if (itemExists)
                {
                    availableItems[color] += 1;
                }
                else
                {
                    availableItems.Add(color, 1);
                }
            }
            return availableItems;
        }
        //public override string ToString()
        //{
        //    string builder = "";
        //    Dictionary<string, int> dic = this.GetAllColors();
        //    foreach (KeyValuePair<string, int> entry in dic)
        //    {
        //        builder += string.Format("{0}, Amount left: {1} \n", entry.Key, entry.Value);

        //    }

        //    return builder;
        //}
    }
}
