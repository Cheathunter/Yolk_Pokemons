using Yolk_Pokemon.Api.Endpoints.TrainerManagement;
using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    public static class AddPokemonToTrainerEndpoint
    {
        public const string Name = "AddPokemonToTrainer";

        public static IEndpointRouteBuilder MapAddPokemonToTrainer(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Trainers.AddPokemon, static async (
                int trainerId, AddPokemonToTrainerRequest request, IPokemonsService pokemonsService,
                CancellationToken token) =>
            {
                try
                {
                    var updated = await pokemonsService.AddPokemonToTrainer(request.PokemonId, trainerId, token);

                    if (updated == null)
                    {
                        return Results.NotFound();
                    }

                    var response = updated.MapToResponse();

                    return TypedResults.CreatedAtRoute(response, GetTrainerEndpoint.Name, new { id = updated.Id });
                }
                catch (DuplicateRecordException ex)
                {
                    return Results.Conflict(new { Error = ex.Message });
                }
            })
            .WithName(Name)
            .Produces<TrainerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}