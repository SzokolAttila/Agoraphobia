using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.Exceptions.Weapon
{
    public class MinGreaterThanMaxException : Exception
    {
        public MinGreaterThanMaxException() : base("MinMultiplier cannot be greater than MaxMultiplier!")
        { }
    }
}
