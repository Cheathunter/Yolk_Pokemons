using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    /// <summary>
    /// Get all Pokemons endpoint
    /// </summary>
    public static class GetAllPokemonsEndpoint
    {
        public const string Name = "GetAllPokemons";
        private const string SuccessfulMessage = "Pokemons listed successfully.";

        /// <summary>
        /// Method to map get all Pokemons endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="PokemonsResponse"/> and OK status.</returns>
        public static IEndpointRouteBuilder MapGetAllPokemons(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Pokemons.GetAll, static async (
                [AsParameters] GetAllPokemonsRequest request, IPokemonsService pokemonsService, HttpContext context, CancellationToken token) =>
            {
                var options = request.MapToOptions();
                var pokemons = await pokemonsService.GetAllPokemonsAsync(options, token);
                var response = pokemons.MapToResponse();

                return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
            })
            .WithName(Name)
            .Produces<GenericResponse<PokemonsResponse>>(StatusCodes.Status200OK);

            return app;
        }
    }
}