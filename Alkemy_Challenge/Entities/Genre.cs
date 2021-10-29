using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
