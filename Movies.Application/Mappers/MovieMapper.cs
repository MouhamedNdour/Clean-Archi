using AutoMapper;



namespace Movies.Application.Mappers
{
    public class MovieMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = pi => pi.GetMethod.IsPublic || pi.SetMethod.IsPublic;
                cfg.AddProfile<MovieMappingProfile>();
            }, null);

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazy.Value;
    }
}
