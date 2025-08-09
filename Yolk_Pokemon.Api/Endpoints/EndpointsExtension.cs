using Yolk_Pokemon.Api.Endpoints.PokemonManegement;
using Yolk_Pokemon.Api.Endpoints.TrainerManagement;

namespace Yolk_Pokemon.Api.Endpoints
{
    public static class EndpointsExtension
    {
        public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCreateTrainer();
            app.MapGetTrainer();
            app.MapGetAllTrainers();
            app.MapUpdateTrainer();
            app.MapDeleteTrainer();

            app.MapCreatePokemon();
            app.MapGetAllPokemons();

            return app;
        }
    }
}
