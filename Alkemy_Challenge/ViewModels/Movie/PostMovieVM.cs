using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Movie
{
    public class PostMovieVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
