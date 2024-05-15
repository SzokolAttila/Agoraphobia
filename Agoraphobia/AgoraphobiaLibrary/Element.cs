using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public abstract class Element
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string Description { get; init; }

        public Element(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

    }
}
