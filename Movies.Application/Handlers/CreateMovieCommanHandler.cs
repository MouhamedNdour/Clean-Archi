using MediatR;
using Movies.Application.Commands;
using Movies.Application.Mappers;
using Movies.Application.Responses;
using Movies.Core.Entities;
using Movies.Core.Repositories;

namespace Movies.Application.Handlers
{
    public class CreateMovieCommanHandler : IRequestHandler<CreateMovieCommand, MovieResponse>
    {
        private readonly IMovieRepository movieRepository;

        public CreateMovieCommanHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<MovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieEntity = MovieMapper.Mapper.Map<Movie>(request);
            if(movieEntity is null)
            {
                throw new ArgumentNullException(nameof(movieEntity));
            }

           var newMovie = await movieRepository.AddAsync(movieEntity);
            return MovieMapper.Mapper.Map<MovieResponse>(newMovie);
        }
    }
}
