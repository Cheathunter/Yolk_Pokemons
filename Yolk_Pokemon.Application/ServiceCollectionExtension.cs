
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Database;
using Yolk_Pokemon.Application.Repositories;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ITrainersRepository, TrainersRepository>();
            services.AddScoped<IPokemonsRepository, PokemonsRepository>();
            services.AddScoped<ITrainersService, TrainersService>();
            services.AddScoped<IPokemonsService, PokemonsService>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
            services.AddDbContext<PokemonDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}