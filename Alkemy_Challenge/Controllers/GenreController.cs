using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DisneyContext _context;

        public GenreController(DisneyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Genres.ToList());
        }

        [HttpPost]
        public IActionResult Post(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return Ok(_context.Genres.ToList());
        }

        [HttpPut]
        public IActionResult Put(Genre genre)
        {
            if (_context.Genres.FirstOrDefault(gen => gen.Id == genre.Id) == null)
            {
                return BadRequest("El genero buscado no existe");
            }
            else
            {
                var internalGenre = _context.Genres.Find(genre.Id);

                internalGenre.Name = genre.Name;
                internalGenre.Image = genre.Image;
                _context.SaveChanges();
                return Ok(_context.Genres.ToList());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Genres.FirstOrDefault(gen => gen.Id == id) == null)
            {
                return BadRequest("El genero buscado no existe");
            }
            else
            {
                var internalGenre = _context.Genres.Find(id);
                _context.Genres.Remove(internalGenre);
                _context.SaveChanges();
                return Ok(_context.Genres.ToList());
            }
        }
    }
}
