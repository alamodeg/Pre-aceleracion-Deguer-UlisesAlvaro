using Alkemy_Challenge.Entities;
using System.Collections.Generic;

namespace Alkemy_Challenge.Interfaces
{
    public interface IMovieRepository
    {
        Movie GetMovie(int id); //si

        List<Movie> GetMovies(); //nose

        List<Movie> GetAllEntities();

        Movie GetEntity(int id);

        Movie Add(Movie entity);

        Movie Update(Movie entity);

        void Delete(int id);
    }
}