using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.ViewModels.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreRepository genreRepository, ILogger<GenreController> logger)
        {
            _genreRepository = genreRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into GenreController");
        }

        [HttpGet]
        [Route("Genre-FullDetails")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var genres = _genreRepository.GetGenres();
                var genresModel = new List<GetFullDetalisGenreVM>();

                foreach (var Genre in genres)
                {
                    GetFullDetalisGenreVM tempGenre = new()
                    {
                        Id = Genre.Id,
                        Name = Genre.Name,
                        Image = Genre.Image,
                        Movies = Genre.Movies
                    };
                    genresModel.Add(tempGenre);
                }
                return Ok(genresModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post(PostGenreVM model)
        {
            try
            {
                Genre NewGenre = new Genre
                {
                    Name = model.Name,
                    Image = model.Image
                };
                _genreRepository.Add(NewGenre);
                return Ok(NewGenre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public IActionResult Put(PutGenreVM model)
        {
            try
            {
                var genreToEdit = _genreRepository.GetGenre(model.Id);

                if (genreToEdit == null)
                {
                    return NotFound("El genero buscado no existe");
                }
                else
                {
                    genreToEdit.Image = model.Image;
                    genreToEdit.Name = model.Name;

                    _genreRepository.Update(genreToEdit);
                    return Ok(genreToEdit);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
