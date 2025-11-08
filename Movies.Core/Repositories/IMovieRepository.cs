using Movies.Core.Entities;
using Movies.Core.Repositories.Base;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IReadOnlyList<Movie>> GetMoviesByDirectorNameAsync(string directorName);
    }
}
