using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Image { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
