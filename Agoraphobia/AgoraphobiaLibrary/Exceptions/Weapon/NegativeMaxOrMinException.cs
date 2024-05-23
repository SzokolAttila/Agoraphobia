using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.Exceptions.Weapon
{
    public class NegativeMaxOrMinException : Exception
    {
        public NegativeMaxOrMinException() : base("You cannot set MaxMultiplier or MinMultiplier to negative!")
        { }
    }
}
