using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.ViewModels.Character;
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
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var characters = _characterRepository.GetCharacters();
            var charactersModel = new List<GetAllCharactersViewModel>();

            foreach (var character in characters)
            {
                GetAllCharactersViewModel tempCharac = new()
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

        [HttpGet]
        [Route("Characters")]
        public IActionResult Get(string name, int age, float weight, int idMovie)
        {
            var characters = _characterRepository.GetCharacters();
            var charactersModel = new List<GetListadoPersonajesViewModel>();

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

            //FALTA FUNCIONALIDAD
            if (idMovie > 0)
            {
                //characters.ForEach(charac => charac.Movies.ToList().RemoveAll( x => x.Id != idMovie ));
            }

            foreach (var x in characters)
            {
                GetListadoPersonajesViewModel tempCharac = new()
                {
                    Name = x.Name,
                    Image = x.Image
                };
                charactersModel.Add(tempCharac);
            }
            return Ok(charactersModel);
        }

        [HttpPost]
        public IActionResult Post(PostRequestViewModel model)
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

        [HttpPut]
        public IActionResult Put(PutRequestViewModel model)
        {
            var characterToEdit = _characterRepository.GetCharacter(model.Id);

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

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
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
