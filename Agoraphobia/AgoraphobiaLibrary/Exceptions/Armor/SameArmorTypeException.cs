using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.Exceptions.Armor
{
    public class SameArmorTypeException : Exception
    {
        public SameArmorTypeException() : base("You cannot have more than one armor of a type!") { }
    }
}
