using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.Exceptions.Weapon
{
    public class MaxSmallerThanMinException : Exception
    {
        public MaxSmallerThanMinException() : base("MaxMultiplier cannot be smaller than MinMultiplier!")
        { }
    }
}
