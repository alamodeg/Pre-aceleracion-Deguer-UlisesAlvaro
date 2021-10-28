using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public byte Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
