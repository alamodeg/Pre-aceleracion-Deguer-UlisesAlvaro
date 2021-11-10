using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.ViewModels.Character;
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
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ICharacterRepository characterRepository, ILogger<CharacterController> logger)
        {
            _characterRepository = characterRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into CharacterController");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Characters-FullDetails")]
        public IActionResult Get()
        {
            try
            {
                var characters = _characterRepository.GetCharacters();
                var charactersModel = new List<GetFullDetailsCharacterVM>();

                foreach (var character in characters)
                {
                    GetFullDetailsCharacterVM tempCharac = new()
                    {
                        Id = character.Id,
                        Age = character.Age,
                        Name = character.Name,
                        Image = character.Image,
                        Weight = character.Weight,
                        Story = character.Story,
                        Movies = character.Movies
                    };
                    charactersModel.Add(tempCharac);
                }
                return Ok(charactersModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Characters")]
        [AllowAnonymous]
        public IActionResult GetFormated()
        {
            try
            {
                var characters = _characterRepository.GetCharacters();
                var charactersModel = new List<GetFormattedCharacterVM>();

                foreach (var character in characters)
                {
                    GetFormattedCharacterVM tempCharac = new()
                    {
                        Name = character.Name,
                        Image = character.Image
                    };
                    charactersModel.Add(tempCharac);
                }
                return Ok(charactersModel);
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
        public IActionResult Get(string name, int age, float weight, int idMovie)
        {
            try
            {
                var characters = _characterRepository.GetCharacters();
                var charactersModel = new List<GetFormattedCharacterVM>();

                if (!characters.Any())
                {
                    return NoContent();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    characters = characters.Where(x => x.Name == name).ToList();
                }

                if (age > 0)
                {
                    characters = characters.Where(x => x.Age == age).ToList();
                }

                if (weight > 0)
                {
                    characters = characters.Where(x => x.Weight == weight).ToList();
                }
                if (idMovie > 0)
                {
                    characters = characters.Where(x => x.Movies.Any(x => x.Id == idMovie)).ToList();
                }

                foreach (var x in characters)
                {
                    GetFormattedCharacterVM tempCharac = new()
                    {
                        Name = x.Name,
                        Image = x.Image
                    };
                    charactersModel.Add(tempCharac);
                }
                return Ok(charactersModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post(PostCharacterVM model)
        {
            try
            {
                Character NewCharacter = new Character
                {
                    Image = model.Image,
                    Name = model.Name,
                    Age = model.Age

                };
                _characterRepository.Add(NewCharacter);
                return Ok(NewCharacter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put(PutCharacterVM model)
        {
            try
            {
                var characterToEdit = _characterRepository.GetCharacter(model.Id);
                _characterRepository.Update(characterToEdit);

                if (characterToEdit == null)
                {
                    return NotFound("El personaje buscado no existe");
                }
                else
                {
                    characterToEdit.Name = model.Name;
                    characterToEdit.Age = model.Age;
                    _characterRepository.Update(characterToEdit);
                    return Ok(characterToEdit);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
