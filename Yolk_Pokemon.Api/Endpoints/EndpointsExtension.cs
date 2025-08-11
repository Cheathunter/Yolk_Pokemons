using Yolk_Pokemon.Api.Endpoints.PokemonManegement;
using Yolk_Pokemon.Api.Endpoints.TrainerManagement;

namespace Yolk_Pokemon.Api.Endpoints
{
    /// <summary>
    /// Class to extend endpoint of this application.
    /// </summary>
    public static class EndpointsExtension
    {
        /// <summary>
        /// Adds enpoints to Get, Get All, Post, Put and Delete Trainers.
        /// Adds enpoints to Get, Get All, Post Pokemons and Add Pokemon to Trainer Endpoints.
        /// </summary>
        /// <param name="app">Endpoint route builder.</param>
        /// <returns>Enhanced endpoint route builder.</returns>
        public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCreateTrainer();
            app.MapGetTrainer();
            app.MapGetAllTrainers();
            app.MapUpdateTrainer();
            app.MapDeleteTrainer();

            app.MapCreatePokemon();
            app.MapGetPokemon();
            app.MapGetAllPokemons();
            app.MapAddPokemonToTrainer();

            return app;
        }
    }
}
