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
    public class GenreRepository : BaseRepository<Genre, DisneyContext>, IGenreRepository
    {
        public GenreRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public Genre GetGenre(int id)
        {
            return DbSet.Include(x => x.Movies).FirstOrDefault(x => x.Id == id);
        }

        public List<Genre> GetGenres()
        {
            return DbSet.Include(x => x.Movies).ToList();
        }
    }
}
