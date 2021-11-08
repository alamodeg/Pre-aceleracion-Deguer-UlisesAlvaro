using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Movie
{
    public class GetFullDetailsMovieVM
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int Rating { get; set; }

        public Entities.Genre Genre { get; set; }

        public ICollection<Entities.Character> Characters { get; set; }
    }
}
