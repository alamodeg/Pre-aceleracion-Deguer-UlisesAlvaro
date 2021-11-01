using Alkemy_Challenge.Entities;
using System.Collections.Generic;

namespace Alkemy_Challenge.Interface
{
    public interface IMovieRepository
    {
        Movie GetMovie(int id);

        List<Movie> GetAllEntities();

        Movie GetEntity(int id);

        Movie Add(Movie entity);

        Movie Update(Movie entity);

        void Delete(int id);
    }
}