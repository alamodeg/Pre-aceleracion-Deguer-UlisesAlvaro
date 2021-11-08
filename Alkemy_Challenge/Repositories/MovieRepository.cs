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
    public class MovieRepository : BaseRepository<Movie, DisneyContext>, IMovieRepository
    {
        public MovieRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public Movie GetMovie(int id)
        {
            return DbSet.Include(x => x.Characters).Include(x => x.Genre).FirstOrDefault(x => x.Id == id);
        }

        public List<Movie> GetMovies()
        {
            return DbSet.Include(x => x.Characters).Include(x => x.Genre).ToList();
        }
    }
}
