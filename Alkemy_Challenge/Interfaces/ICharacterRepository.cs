using Alkemy_Challenge.Entities;
using System.Collections.Generic;

namespace Alkemy_Challenge.Interfaces
{
    public interface ICharacterRepository : IBaseRepository<Character>
    {
        Character GetCharacter(int id);

        List<Character> GetCharacters();
    }
}