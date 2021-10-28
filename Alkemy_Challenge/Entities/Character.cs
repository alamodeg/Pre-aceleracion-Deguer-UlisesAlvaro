﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Entities
{
    public class Character
    {
        public int Id { get; set; }

        public string Image { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }

        public float Weight { get; set; }

        public string Story { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
