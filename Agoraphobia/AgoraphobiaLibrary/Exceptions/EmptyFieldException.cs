using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException()
            : base("A required field has been left empty!")
        {

        }
    }
}
