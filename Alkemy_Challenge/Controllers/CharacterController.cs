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
    public class CharacterController : ControllerBase
    {
        private readonly DisneyContext _context;

        public CharacterController(DisneyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Characters.ToList());
        }

        [HttpPost]
        public IActionResult Post(Character charac)
        {
            _context.Characters.Add(charac);
            _context.SaveChanges();
            return Ok(_context.Characters.ToList());
        }

        [HttpPut]
        public IActionResult Put(Character charac)
        {
            if (_context.Characters.FirstOrDefault(charac => charac.Id == charac.Id) == null)
            {
                return BadRequest("El personaje buscado no existe");
            }
            else
            {
                var internalCharacter = _context.Characters.Find(charac.Id);

                internalCharacter.Name = charac.Name;
                internalCharacter.Age = charac.Age;
                _context.SaveChanges();
                return Ok(_context.Characters.ToList());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Characters.FirstOrDefault(charac => charac.Id == id) == null)
            {
                return BadRequest("El personaje buscado no existe");
            }
            else
            {
                var internalCharacter = _context.Characters.Find(id);
                _context.Characters.Remove(internalCharacter);
                _context.SaveChanges();
                return Ok(_context.Characters.ToList());
            }
        }
    }
}
