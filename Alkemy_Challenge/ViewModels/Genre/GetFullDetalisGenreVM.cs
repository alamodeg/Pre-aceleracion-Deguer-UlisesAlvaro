using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Genre
{
    public class GetFullDetalisGenreVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<Entities.Movie> Movies { get; set; }
    }
}
