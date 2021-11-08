using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Movie
{
    public class GetFormattedMovieVM
    {
        [Required]
        [MaxLength(150)]
        public string Image { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
