using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    public static class GetAllPokemonsEndpoint
    {
        public const string Name = "GetAllPokemons";

        public static IEndpointRouteBuilder MapGetAllPokemons(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Pokemons.GetAll, static async (
                [AsParameters] GetAllPokemonsRequest request, IPokemonsService pokemonsService, HttpContext context, CancellationToken token) =>
            {
                var pokemons = await pokemonsService.GetAllPokemonsAsync(token);

                return TypedResults.Ok(pokemons.MapToResponse());
            })
            .WithName(Name)
            .Produces<PokemonsResponse>(StatusCodes.Status200OK);

            return app;
        }
    }
}