using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    /// <summary>
    /// Update Trainer edpoint
    /// </summary>
    public static class UpdateTrainerEndpoint
    {
        public const string Name = "UpdateTrainer";
        private const string SuccessfulMessage = "Trainer updated successfully.";
        private const string UnsuccessfulMessage = "Trainer not found.";

        /// <summary>
        /// Method to map update Trainer endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="TrainerResponse"/> and OK status or error status.</returns>
        public static IEndpointRouteBuilder MapUpdateTrainer(this IEndpointRouteBuilder app)
        {
            app.MapPut(ApiEndpoints.Trainers.Update, static async (
                int id, UpdateTrainerRequest request, ITrainersService trainersService, HttpContext context, CancellationToken token) =>
            {
                Trainer updated = request.MapToTrainer(id);
                var trainer = await trainersService.UpdateTrainerAsync(updated, token);

                if (trainer == null)
                {
                    return ((TrainerResponse?)null).ToGenericResponse(UnsuccessfulMessage, StatusCodes.Status404NotFound, false);
                }
                var response = trainer.MapToResponse();

                return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
            })
            .WithName(Name)
            .Produces<GenericResponse<TrainerResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
