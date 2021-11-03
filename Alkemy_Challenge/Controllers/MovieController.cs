using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.Repositories;
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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = _movieRepository.GetMovies();
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
