using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class DeleteTrainerEndpoint
    {
        public const string Name = "DeleteTrainer";
        private const string SuccessfulMessage = "Trainer deleted successfully.";
        private const string UnsuccessfulMessage = "Trainer not found.";

        public static IEndpointRouteBuilder MapDeleteTrainer(this IEndpointRouteBuilder app)
        {
            app.MapDelete(ApiEndpoints.Trainers.Delete, static async (
                int id, ITrainersService trainersService, CancellationToken token) =>
            {
                bool deleted = await trainersService.DeleteByIdAsync(id, token);

                if (!deleted)
                {
                    return ((TrainerResponse?)null).ToGenericResponse(UnsuccessfulMessage, StatusCodes.Status404NotFound, false);
                }

                return ((TrainerResponse?)null).ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
