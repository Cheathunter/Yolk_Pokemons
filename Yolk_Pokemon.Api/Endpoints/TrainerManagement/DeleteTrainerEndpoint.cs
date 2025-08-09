using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class DeleteTrainerEndpoint
    {
        public const string Name = "DeleteTrainer";

        public static IEndpointRouteBuilder MapDeleteTrainer(this IEndpointRouteBuilder app)
        {
            app.MapDelete(ApiEndpoints.Trainers.Delete, static async (
                int id, ITrainersService trainersService, CancellationToken token) =>
            {
                bool deleted = await trainersService.DeleteByIdAsync(id, token);

                return deleted ? TypedResults.Ok() : Results.NotFound();
            })
            .WithName(Name)
            .Produces<TrainerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
