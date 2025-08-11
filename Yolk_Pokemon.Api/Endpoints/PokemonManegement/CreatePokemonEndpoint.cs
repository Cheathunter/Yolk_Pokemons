using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    /// <summary>
    /// Create Pokemon endpoint
    /// </summary>
    public static class CreatePokemonEndpoint
    {
        public const string Name = "CreatePokemon";
        private const string SuccessfulMessage = "Pokemon created successfully.";

        /// <summary>
        /// Method to map create Pokemon endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="PokemonResponse"/> and Created status or error status.</returns>
        public static IEndpointRouteBuilder MapCreatePokemon(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Pokemons.Create, static async (
                CreatePokemonRequest request, IPokemonsService pokemonsService, CancellationToken token) =>
            {
                try {
                    Pokemon pokemon = request.MapToPokemon();
                    await pokemonsService.CreatePokemonAsync(pokemon, token);
                    var response = pokemon.MapToResponse();

                    return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status201Created);
                }
                catch (DuplicateRecordException ex)
                {
                    return ((PokemonResponse?)null).ToGenericResponse(ex.Message, StatusCodes.Status409Conflict, false);
                }
                catch (InvalidOperationException ex)
                {
                    return ((PokemonResponse?)null).ToGenericResponse(ex.Message, StatusCodes.Status406NotAcceptable, false);
                }
            })
            .WithName(Name)
            .Produces<GenericResponse<PokemonResponse>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status406NotAcceptable);

            return app;
        }
    }
}
