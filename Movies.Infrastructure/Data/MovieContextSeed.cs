using Microsoft.Extensions.Logging;
using Movies.Core.Entities;

namespace Movies.Infrastructure.Data
{
    public class MovieContextSeed
    {
        public static async Task SeedAsync(MovieContext movieContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                await movieContext.Database.EnsureCreatedAsync();
                if (!movieContext.Movies.Any())
                {
                    movieContext.Movies.AddRange(GetPreconfiguredMovies());
                    await movieContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MovieContextSeed>();
                    logger.LogError(ex, "An error occurred seeding the DB. Retrying... Attempt {RetryAttempt}", retryForAvailability);
                    await SeedAsync(movieContext, loggerFactory, retryForAvailability);
                }
                else
                {
                    throw;
                }
            }
        }
        private static IEnumerable<Movie> GetPreconfiguredMovies()
        {
            return
            [
                new Movie
                {
                    MovieName = "The Shawshank Redemption",
                    DirectorName = "Frank Darabont",
                    ReleaseYear = "1994"
                },
                new Movie
                {
                    MovieName = "The Godfather",
                    DirectorName = "Francis Ford Coppola",
                    ReleaseYear = "1972"
                },
                new Movie
                {
                    MovieName = "The Dark Knight",
                    DirectorName = "Christopher Nolan",
                    ReleaseYear = "2008"
                }
            ];

        }
    }

    
}
