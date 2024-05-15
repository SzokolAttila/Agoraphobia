using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public abstract record Element
    {
        [Key]
        public int Id { get; private set; }
        
        [Required]
        public string Name { get; set; }
        
        [JsonInclude]
        public string Description { get; set; }

        [JsonConstructor]
        public Element(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Element(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
