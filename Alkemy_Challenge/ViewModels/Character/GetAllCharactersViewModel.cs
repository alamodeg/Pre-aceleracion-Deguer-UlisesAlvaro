using Alkemy_Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.ViewModels.Character
{
    public class GetAllCharactersViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public float Weight { get; set; }

        public string Story { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
