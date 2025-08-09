
using Microsoft.Extensions.DependencyInjection;
using Yolk_Pokemon.Application.Repositories;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ITrainersRepository, TrainersRepository>();
            services.AddSingleton<IPokemonsRepository, PokemonsRepository>();
            services.AddScoped<ITrainersService, TrainersService>();
            services.AddScoped<IPokemonsService, PokemonsService>();

            return services;
        }
    }
}