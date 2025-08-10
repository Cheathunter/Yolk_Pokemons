using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class GetTrainerEndpoint
    {
        public const string Name = "GetTrainer";
        private const string SuccessfulMessage = "Trainer found successfully.";
        private const string UnsuccessfulMessage = "Trainer not found.";

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
