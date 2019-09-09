using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker_Demo
{
    //? WHAT IS THIS USED FOR? DON'T REMEMBER!
    public abstract class ColorsSC
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        } 
        public ColorsSC(string name)
        {
            Name = name;
        }
    }
}
