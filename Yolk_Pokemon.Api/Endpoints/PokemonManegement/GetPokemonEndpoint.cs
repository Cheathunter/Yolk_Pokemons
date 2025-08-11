using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    /// <summary>
    /// Get Pokemon endpoint
    /// </summary>
    public static class GetPokemonEndpoint
    {
        public const string Name = "GetPokemon";
        private const string SuccessfulMessage = "Pokemon found successfully.";
        private const string UnsuccessfulMessage = "Pokemon not found.";

        /// <summary>
        /// Method to map get Pokemon by ID endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="PokemonResponse"/> and OK status or error status.</returns>
        public static IEndpointRouteBuilder MapGetPokemon(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Pokemons.Get, static async (
                int id, IPokemonsService pokemonsService, HttpContext context, CancellationToken token) =>
            {
                var pokemon = await pokemonsService.GetPokemonByIdAsync(id, token);

                if (pokemon is null)
                {
                    return ((PokemonResponse?)null).ToGenericResponse(UnsuccessfulMessage, StatusCodes.Status404NotFound, false);
                }

                var response = pokemon.MapToResponse();

                return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
            })
            .WithName(Name)
            .Produces<GenericResponse<PokemonResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}