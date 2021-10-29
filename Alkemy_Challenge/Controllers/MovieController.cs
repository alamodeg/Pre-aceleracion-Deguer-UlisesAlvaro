using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DisneyContext _context;

        public MovieController(DisneyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Movies.ToList());
        }

        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok(_context.Movies.ToList());
        }

        [HttpPut]
        public IActionResult Put(Movie movie)
        {
            if (_context.Movies.FirstOrDefault(mov => mov.Id == movie.Id) == null)
            {
                return BadRequest("La pelicula buscada no existe");
            }
            else
            {
                var internalMovie = _context.Movies.Find(movie.Id);

                internalMovie.Title = movie.Title;
                internalMovie.Genre = movie.Genre;
                internalMovie.Rating = movie.Rating;
                _context.SaveChanges();
                return Ok(_context.Movies.ToList());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Movies.FirstOrDefault(mov => mov.Id == id) == null)
            {
                return BadRequest("La pelicula buscada no existe");
            }
            else 
            {
                var internalMovie = _context.Movies.Find(id);
                _context.Movies.Remove(internalMovie);
                _context.SaveChanges();
                return Ok(_context.Movies.ToList());
            }
        }
    }
}
