using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.Repositories;
using Alkemy_Challenge.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieRepository movieRepository, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into MovieController");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Movies-FullDetails")]
        public IActionResult Get()
        {
            try
            {
                var movies = _movieRepository.GetMovies();
                var movieModel = new List<GetFullDetailsMovieVM>();

                foreach (var movie in movies)
                {
                    GetFullDetailsMovieVM tempMovie = new()
                    {
                        Id = movie.Id,
                        Image = movie.Image,
                        Title = movie.Title,
                        CreationDate = movie.CreationDate,
                        Rating = movie.Rating,
                        Genre = movie.Genre,
                        Characters = movie.Characters
                    };
                    movieModel.Add(tempMovie);
                }
                return Ok(movieModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Movies")]
        [AllowAnonymous]
        public IActionResult GetFormatted()
        {
            try
            {
                var movies = _movieRepository.GetMovies();
                var FormattedMovie = new List<GetFormattedMovieVM>();

                foreach (var movie in movies)
                {
                    GetFormattedMovieVM tempMovie = new()
                    {
                        Image = movie.Image,
                        Title = movie.Title,
                        CreationDate = movie.CreationDate
                    };
                    FormattedMovie.Add(tempMovie);
                }
                return Ok(FormattedMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Search")]
        [AllowAnonymous]
        public IActionResult Get(string title,int idGenre)
        {
            try
            {
                var movies = _movieRepository.GetMovies();
                var moviesModel = new List<GetFullDetailsMovieVM>();

                if (!string.IsNullOrEmpty(title))
                {
                    movies = movies.Where(x => x.Title == title).OrderBy(x => x.CreationDate).ToList();
                }

                if (idGenre > 0)
                {
                    movies = movies.Where(x => x.Genre.Id == idGenre).ToList();
                }

                movies = movies.OrderBy(x => x.CreationDate).ToList();

                if (!movies.Any())
                {
                    return NoContent();
                }
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        public IActionResult Post(PostMovieVM model)
        {
            try
            {
                Movie NewMovie = new Movie
                {
                    Image = model.Image,
                    Title = model.Title,
                    Rating = model.Rating
                };
                _movieRepository.Add(NewMovie);
                return Ok(NewMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public IActionResult Put(PutMovieVM model)
        {
            try
            {
                var movieToEdit = _movieRepository.GetMovie(model.Id);

                if (movieToEdit == null)
                {
                    return NotFound("La pelicula buscada no existe");
                }
                else
                {
                    movieToEdit.Title = model.Title;
                    movieToEdit.Image = model.Image;
                    movieToEdit.Rating = model.Rating;

                    _movieRepository.Update(movieToEdit);
                    return Ok(movieToEdit);
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
                var movieToDelete = _movieRepository.GetMovie(id);

                if (movieToDelete == null)
                {
                    return NotFound("La pelicula buscada no existe");
                }
                else
                {
                    _movieRepository.Delete(id);

                    return Ok("Pelicula eliminada");
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
