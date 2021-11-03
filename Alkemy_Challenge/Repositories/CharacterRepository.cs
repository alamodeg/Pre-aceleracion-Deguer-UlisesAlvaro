using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Repositories
{
    public class CharacterRepository : BaseRepository<Character, DisneyContext>,ICharacterRepository
    {
        public CharacterRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public Character GetCharacter(int id)
        {
            return DbSet.Include(x => x.Movies).FirstOrDefault(x => x.Id == id);
        }

        public List<Character> GetCharacters()
        {
            return DbSet.Include(x => x.Movies).ToList();
        }
    }
}
