using Alkemy_Challenge.Context;
using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interface;
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
            return GetEntity(id);
        }
    }
}
