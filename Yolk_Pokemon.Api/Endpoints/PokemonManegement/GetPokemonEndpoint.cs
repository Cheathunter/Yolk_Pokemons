using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    public static class GetPokemonEndpoint
    {
        public const string Name = "GetPokemon";

        public static IEndpointRouteBuilder MapGetPokemon(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Pokemons.Get, async (
                int id, IPokemonsService pokemonsService, HttpContext context, CancellationToken token) =>
            {
                var pokemon = await pokemonsService.GetPokemonByIdAsync(id, token);

                if (pokemon is null)
                {
                    return Results.NotFound();
                }

                var response = pokemon.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<PokemonResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}