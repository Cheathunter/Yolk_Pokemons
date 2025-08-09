using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class UpdateTrainerEndpoint
    {
        public const string Name = "UpdateTrainer";

        public static IEndpointRouteBuilder MapUpdateTrainer(this IEndpointRouteBuilder app)
        {
            app.MapPut(ApiEndpoints.Trainers.Update, async (
                int id, UpdateTrainerRequest request, ITrainersService trainersService, HttpContext context, CancellationToken token) =>
            {
                Trainer updated = request.MapToTrainer(id);
                var trainer = await trainersService.UpdateTrainerAsync(updated, token);

                if (!trainer)
                {
                    return Results.NotFound();
                }

                return TypedResults.Ok(updated.MapToResponse());
            })
            .WithName(Name)
            .Produces<TrainerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
