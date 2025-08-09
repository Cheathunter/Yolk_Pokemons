using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    public static class CeratePokemonEndpoint
    {
        public const string Name = "CreatePokemon";

        public static IEndpointRouteBuilder MapCreatePokemon(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Pokemons.Create, async (
                CreatePokemonRequest request, IPokemonsService pokemonsService, CancellationToken token) =>
            {
                try {
                    Pokemon pokemon = request.MapToPokemon();
                    await pokemonsService.CreatePokemonAsync(pokemon, token);
                    var response = pokemon.MapToResponse();

                    return TypedResults.Created();
                }
                catch (DuplicateRecordException ex)
                {
                    return Results.Conflict(new { Error = ex.Message });
                }
            })
            .WithName(Name)
            .Produces<PokemonResponse>(StatusCodes.Status201Created);

            return app;
        }
    }
}
