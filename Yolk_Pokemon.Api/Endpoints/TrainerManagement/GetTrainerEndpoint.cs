using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class GetTrainerEndpoint
    {
        public const string Name = "GetTrainer";

        public static IEndpointRouteBuilder MapGetTrainer(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Trainers.Get, async (
                int id, ITrainersService trainersService, HttpContext context, CancellationToken token) =>
            {
                var trainer = await trainersService.GetTrainerByIdAsync(id, token);

                if (trainer is null)
                {
                    return Results.NotFound();
                }

                var response = trainer.MapToResponse();

                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<TrainerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
