using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var characters = _characterRepository.GetCharacters();
            return Ok(characters);
        }

        [HttpPost]
        public IActionResult Post(Character character)
        {
            _characterRepository.Add(character);
            return Ok(character);
        }

        [HttpPut]
        public IActionResult Put(Character character)
        {
            var characterToEdit = _characterRepository.GetCharacter(character.Id);

            if (characterToEdit == null)
            {
                return NotFound("El personaje buscado no existe");
            }
            else
            {
                characterToEdit.Name = character.Name;
                characterToEdit.Age = character.Age;

                _characterRepository.Update(characterToEdit);
                return Ok(characterToEdit);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var characterToEdit = _characterRepository.GetCharacter(id);

            if (characterToEdit == null)
            {
                return NotFound("El personaje buscado no existe");
            }
            else
            {
                _characterRepository.Delete(id);

                return Ok("Personaje eliminado");
            }
        }
    }
}
