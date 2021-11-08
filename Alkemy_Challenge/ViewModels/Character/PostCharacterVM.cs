using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Character
{
    public class PostCharacterVM
    {
        [Required]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
    }
}
