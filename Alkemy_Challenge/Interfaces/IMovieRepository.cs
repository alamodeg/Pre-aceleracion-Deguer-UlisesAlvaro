using Alkemy_Challenge.Entities;
using System.Collections.Generic;

namespace Alkemy_Challenge.Interfaces
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        Movie GetMovie(int id);

        List<Movie> GetMovies();
    }
}