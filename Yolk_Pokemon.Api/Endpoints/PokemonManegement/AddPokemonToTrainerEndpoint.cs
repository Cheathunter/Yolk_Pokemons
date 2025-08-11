using Yolk_Pokemon.Api.Endpoints.TrainerManagement;
using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.PokemonManegement
{
    /// <summary>
    /// Add Pokemon to Trainer endpoint
    /// </summary>
    public static class AddPokemonToTrainerEndpoint
    {
        public const string Name = "AddPokemonToTrainer";
        private const string SuccessfulMessage = "Pokemon successfully added to user.";
        private const string UnsuccessfulMessage = "Trainer or pokemon not found.";

        /// <summary>
        /// Method to map add Pokemon to Trainer endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="PokemonResponse"/> and OK status or error status.</returns>
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
                        return ((TrainerResponse?)null).ToGenericResponse(UnsuccessfulMessage, StatusCodes.Status404NotFound, false);
                    }

                    var response = updated.MapToResponse();

                    return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
                }
                catch (DuplicateRecordException ex)
                {
                    return ((PokemonResponse?)null).ToGenericResponse(ex.Message, StatusCodes.Status409Conflict, false);
                }
            })
            .WithName(Name)
            .Produces<GenericResponse<TrainerResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict);

            return app;
        }
    }
}