using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var genres = _genreRepository.GetGenres();
            return Ok(genres);
        }

        [HttpPost]
        public IActionResult Post(Genre genre)
        {
            _genreRepository.Add(genre);
            return Ok(genre);
        }
        
        [HttpPut]
        public IActionResult Put(Genre genre)
        {
            var genreToEdit = _genreRepository.GetGenre(genre.Id);

            if (genreToEdit == null)
            {
                return NotFound("El genero buscado no existe");
            }
            else
            {
                genreToEdit.Image = genre.Image;
                genreToEdit.Name = genre.Name;

                _genreRepository.Update(genreToEdit);
                return Ok(genreToEdit);
            }
        }
        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var genreToEdit = _genreRepository.GetGenre(id);

            if (genreToEdit == null)
            {
                return NotFound("El genero buscado no existe");
            }
            else
            {
                _genreRepository.Delete(id);

                return Ok("Genero eliminado");
            }
        }
    }
}
