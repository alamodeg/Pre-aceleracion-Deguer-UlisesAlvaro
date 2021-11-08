using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Genre
{
    public class PostGenreVM
    {
        [Required]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
