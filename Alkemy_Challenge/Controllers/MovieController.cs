using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var movies = _movieRepository.GetMovies();
            return Ok(movies);
        }

        [HttpGet]
        [Route("BusquedaPelicula")]
        public IActionResult Get(string title)
        {
            var movies = _movieRepository.GetMovies();

            if (!string.IsNullOrEmpty(title))
            {
                movies = movies.Where(x => x.Title == title).OrderBy(x => x.CreationDate).ToList();
            }
            
            movies = movies.OrderBy(x => x.CreationDate).ToList();

            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            _movieRepository.Add(movie);
            return Ok(movie);
        }

        [HttpPut]
        public IActionResult Put(Movie movie)
        {
            var movieToEdit = _movieRepository.GetMovie(movie.Id);

            if (movieToEdit == null)
            {
                return NotFound("La pelicula buscada no existe");
            }
            else
            {
                movieToEdit.Title = movie.Title;
                movieToEdit.Genre = movie.Genre;
                movieToEdit.Rating = movie.Rating;

                _movieRepository.Update(movieToEdit);
                return Ok(movieToEdit);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
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
    }
}
