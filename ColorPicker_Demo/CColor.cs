using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorDifferentiater
{
    public class CColor
    {
        /// <summary>
        /// The name of the color
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Alpha value Normalized 0f -> 1f
        /// </summary>
        public float ANormalized
        {
            get
            {
                if (a != 0)
                    return a / 255;

                return 0;
            }
        }

        /// <summary>
        /// The Blue value Normalized 0f -> 1f
        /// </summary>
        public float BNormalized
        {
            get
            {
                if (b != 0)
                    return b / 255;

                return 0;
            }
        }

        /// <summary>
        /// The Green value Normalized 0f -> 1f
        /// </summary>
        public float GNormalized
        {
            get
            {
                if (g != 0)
                    return g / 255;

                return 0;
            }
        }

        /// <summary>
        /// The Red value Normalized 0f -> 1f
        /// </summary>
        public float RNormalized
        {
            get
            {
                if (r != 0)
                    return r / 255;

                return 0;
            }
        }

        /// <summary>
        /// The alpha value 0 -> 255
        /// </summary>
        public float A { get => a; set => a = value; }

        /// <summary>
        /// The blue value 0 -> 255
        /// </summary>
        public float B { get => b; set => b = value; }

        /// <summary>
        /// The green value 0 -> 255
        /// </summary>
        public float G { get => g; set => g = value; }

        /// <summary>
        /// The red value 0 -> 255
        /// </summary>
        public float R { get => r; set => r = value; }

        /// <summary>
        /// The total values of all the rgba values
        /// </summary>
        public float Total { get => a + b + g + r; }
        float r, g, b, a;

        /// <summary>
        /// Color Constructor
        /// r => 0
        /// b => 0
        /// g => 0
        /// a => 0
        /// </summary>
        public CColor()
        {
            r = 0;
            b = 0;
            b = 0;
            a = 0;
        }

        /// <summary>
        /// Color Constructor
        /// g => 0
        /// b => 0
        /// a => 255
        /// </summary>
        /// <param name="_r"></param>
        public CColor(float _r)
        {
            r = _r;
            g = 0;
            b = 0;
            a = 255;
        }

        /// <summary>
        /// Color Constructor
        /// b => 0
        /// a => 255
        /// </summary>
        /// <param name="_r"></param>
        /// <param name="_g"></param>
        public CColor(float _r, float _g) : this(_r)
        {
            g = _g;
        }

        /// <summary>
        /// Color Constructor
        /// a => 255
        /// </summary>
        /// <param name="_r"></param>
        /// <param name="_g"></param>
        /// <param name="_b"></param>
        public CColor(float _r, float _g, float _b) : this(_r, _g)
        {
            b = _b;
        }

        /// <summary>
        /// Color Constructor
        /// </summary>
        /// <param name="_r"></param>
        /// <param name="_g"></param>
        /// <param name="_b"></param>
        /// <param name="_a"></param>
        public CColor(float _r, float _g, float _b, float _a) : this(_r, _g, _b)
        {
            a = _a;
        }

        /// <summary>
        /// Color addition
        /// values can't be higher than 255
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static CColor operator + (CColor _a, CColor _b)
        {
            CColor r = new CColor(_a.r + _b.r, _a.g + _b.g, _a.b + _b.b, _a.a + _b.a);


            r.a = r.a > 255f ? 255f : r.a;
            r.r = r.r > 255f ? 255f : r.r;
            r.g = r.g > 255f ? 255f : r.g;
            r.b = r.b > 255f ? 255f : r.b;

            return r;
        }

        /// <summary>
        /// Subtraction operator values can't be less than 0
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static CColor operator -(CColor _a, CColor _b)
        {
            CColor r = new CColor(_a.r - _b.r, _a.g - _b.g, _a.b - _b.b, _a.a - _b.a);


            r.a = r.a < 0f ? 0f : r.a;
            r.r = r.r < 0f ? 0f : r.r;
            r.g = r.g < 0f ? 0f : r.g;
            r.b = r.b < 0f ? 0f : r.b;

            return r;
        }

        public static CColor operator / (CColor _a, int div)
        {
            float r = _a.r != 0 ? _a.r / div : 0f;
            float g = _a.g != 0 ? _a.g / div : 0f;
            float b = _a.b != 0 ? _a.b / div : 0f;

            return new CColor(r, g, b);
        }

        /// <summary>
        /// Get the domination RGB string
        /// </summary>
        /// <returns></returns>
        public string DominatingColor()
        {
            string dom = "";

            if (r > b && r > g)
                dom = "Red";

            if (b > r && b > g)
                dom = "Blue";

            if (g > r && g > b)
                dom = "Green";

            return dom;
        }

        /// <summary>
        /// Equal operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator ==(CColor _a, CColor _b)
        {
            //// If left hand side is null...
            //if (Object.ReferenceEquals(_a, null))
            //{
            //    // ...right hand side is not null, therefore not Equal.
            //    return false;
            //}

            //// ...and right hand side is null...
            //if (Object.ReferenceEquals(_b, null))
            //{
            //    //...both are null and are Equal.
            //    return false;
            //}

            if (_a.R == _b.R && _a.G == _b.G && _a.B == _b.B && _a.A == _b.A)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Equal operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator ==(CColor _a, object _b)
        {
            // If left hand side is null...
            if (Object.ReferenceEquals(_a, null))
            {
                // ...right hand side is not null, therefore not Equal.
                return true;
            }

            // ...and right hand side is null...
            if (Object.ReferenceEquals(_b, null))
            {
                //...both are null and are Equal.
                return true;
            }

            return _a.Equals(_b);

        }

        /// <summary>
        /// Not Equal operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator !=(CColor _a, object _b)
        {
            return !(_a == _b);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator !=(CColor _a, CColor _b)
        {
            return !(_a == _b);
        }

        /// <summary>
        /// Less than operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator <(CColor _a, CColor _b)
        {
            if (_a.Total < _b.Total)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Less than operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator <=(CColor _a, CColor _b)
        {
            if (_a.Total <= _b.Total)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Higher than operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator >(CColor _a, CColor _b)
        {
            if (_a.Total > _b.Total)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Higher than operator
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        public static bool operator >=(CColor _a, CColor _b)
        {
            if (_a.Total >= _b.Total)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get the color as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("R: {0} G {1} B: {2} A {3}", R, G, B, A);
        }


    }
}
