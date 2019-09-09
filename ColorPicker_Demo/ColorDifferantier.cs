using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorDifferentiater
{
    //! PRETTY SURE WE DON'T NEED THIS ANYMORE
    /// <summary>
    /// Define a color range, lowerrange is the lowest rgba values which the color can have
    /// the upperrange is the highest rgb value the color can have
    /// </summary>
    public struct ColorRange
    {
        /// <summary>
        /// The lowest range of the color
        /// </summary>
        public CColor LowerRange { get; private set; }

        /// <summary>
        /// The highest range of the color
        /// </summary>
        public CColor UpperRange { get; private set; }

        /// <summary>
        /// The name of the color
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_lowerRange"></param>
        /// <param name="_upperRange"></param>
        /// <param name="name"></param>
        public ColorRange(CColor _lowerRange, CColor _upperRange, string name)
        {
            LowerRange = _lowerRange;
            UpperRange = _upperRange;
            Name = name;
        }
    }

    public class ColorDifferantier
    {
        //private List<CColor> colors;
        private List<ColorRange> ranges;

        public ColorDifferantier()
        {
            ranges = new List<ColorRange>();
        }

        ///// <summary>
        ///// Initialiase all the color ranges and extract the colors
        ///// </summary>
        //public void Initialise()
        //{
        //    colors = new List<CColor>();

        //    for (int i = 0; i < ranges.Count; i++)
        //    {
        //        AddColorsToList(ref colors, GetColorRange(ranges[i]));
        //    }

        //}

        /// <summary>
        /// Add a color range to the ranges list
        /// need to initialise the list afterwards
        /// </summary>
        /// <param name="_range"></param>
        public void AddColorRange(ColorRange _range)
        {
            ranges.Add(_range);
        }

        /// <summary>
        /// Get the best match from the colorranges and get the color name
        /// </summary>
        /// <param name="_color"></param>
        /// <returns></returns>
        public string GetColorName(CColor _color)
        {
            List<CColor> _posColors = new List<CColor>();

            for (int i = 0; i < ranges.Count; i++)
            {
                if (ranges[i].LowerRange <= _color && ranges[i].UpperRange >= _color)
                {
                    CColor _cur = new CColor(_color.R, _color.G, _color.B, _color.A);
                    _cur.Name = ranges[i].Name;

                    _posColors.Add(_cur);
                }
            }

            // Set first as the best match
            CColor outPut = new CColor(3000, 3000, 3000);
            outPut.Name = "Unknown";

            // Find the one closest to the highest nuance
            for (int i = 0; i < _posColors.Count; i++)
            {

                ColorRange _ran = new ColorRange();
                for (int j = 0; j < ranges.Count; j++)
                {
                    if (ranges[j].Name == _posColors[i].Name)
                        _ran = ranges[j];

                }

                CColor _ranqe = (_ran.LowerRange + (_ran.UpperRange - _ran.LowerRange)) / 2;

                CColor _o = new CColor(Math.Abs(_ranqe.R - _color.R), Math.Abs(_ranqe.G - _color.G), Math.Abs(_ranqe.B - _color.B));
                _o.Name = _ran.Name;

                if (_o < outPut)
                    outPut = _o;
            }

            return outPut.Name;
        }

        ///// <summary>
        ///// Add a color to the color list from a array
        ///// </summary>
        ///// <param name="_col"></param>
        ///// <param name="_arr"></param>
        //private void AddColorsToList(ref List<CColor> _col, CColor[] _arr)
        //{
        //    for (int i = 0; i < _arr.Length; i++)
        //    {
        //        _col.Add(_arr[i]);
        //    }
        //}

        ///// <summary>
        ///// Get the color range from a color range object
        ///// returns a array with all the possible colors
        ///// </summary>
        ///// <param name="_range"></param>
        ///// <returns></returns>
        //private CColor[] GetColorRange(ColorRange _range)
        //{
        //    List<CColor> _res = new List<CColor>();

        //    if (_range.UpperRange.R > 0)
        //    {
        //        for (int r = (int)_range.LowerRange.R; r < _range.UpperRange.R; r++)
        //        {
        //            for (int g = (int)_range.LowerRange.G; g < _range.UpperRange.G; g++)
        //            {
        //                for (int b = (int)_range.LowerRange.B; b < _range.UpperRange.B+1; b++)
        //                {
        //                    CColor _r = new CColor(r, g, b);
        //                    _r.Name = _range.Name;

        //                    _res.Add(_r);
        //                }
        //            }
        //        }
        //    }
        //    else if (_range.UpperRange.G > 0)
        //    {
        //        for (int g = (int)_range.LowerRange.G; g < _range.UpperRange.G; g++)
        //        {
        //            for (int b = (int)_range.LowerRange.B; b < _range.UpperRange.B; b++)
        //            {
        //                for (int r = (int)_range.LowerRange.R; r < _range.UpperRange.R+1; r++)
        //                {
        //                    CColor _r = new CColor(r, g, b);
        //                    _r.Name = _range.Name;
        //                    _res.Add(_r);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int b = (int)_range.LowerRange.B; b < _range.UpperRange.B; b++)
        //        {
        //            for (int r = (int)_range.LowerRange.R; r < _range.UpperRange.R; r++)
        //            {
        //                for (int g = (int)_range.LowerRange.G; g < _range.UpperRange.G + 1; g++)
        //                {
        //                    CColor _r = new CColor(r, g, b);
        //                    _r.Name = _range.Name;
        //                    _res.Add(_r);
        //                }
        //            }
        //        }
        //    }


        //    return _res.ToArray();
        //}
    }
}
