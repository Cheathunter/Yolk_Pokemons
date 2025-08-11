using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    /// <summary>
    /// Get Trainer endpoint
    /// </summary>
    public static class GetTrainerEndpoint
    {
        public const string Name = "GetTrainer";
        private const string SuccessfulMessage = "Trainer found successfully.";
        private const string UnsuccessfulMessage = "Trainer not found.";

        /// <summary>
        /// Method to map get Trainer by ID endpoint.
        /// </summary>
        /// <param name="app">Endpoint router builder.</param>
        /// <returns>Json from <see cref="GenericResponse{T}"/>, <see cref="TrainerResponse"/> and OK status or error status.</returns>
        public static IEndpointRouteBuilder MapGetTrainer(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Trainers.Get, static async (
                int id, ITrainersService trainersService, HttpContext context, CancellationToken token) =>
            {
                var trainer = await trainersService.GetTrainerByIdAsync(id, token);

                if (trainer is null)
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
