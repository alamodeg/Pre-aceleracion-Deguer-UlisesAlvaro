using Alkemy_Challenge.Entities;
using System.Collections.Generic;

namespace Alkemy_Challenge.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Genre GetGenre(int id);
        
        List<Genre> GetGenres();
    }
}