using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class CreateTrainerEndpoint
    {
        public const string Name = "CreateTrainer";

        public static IEndpointRouteBuilder MapCreateTrainer(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Trainers.Create, async (
                CreateTrainerRequest request, ITrainersService trainerService, CancellationToken token) =>
            {
                Trainer trainer = request.MapToTrainer();
                await trainerService.CreateTrainerAsync(trainer, token);
                var response = trainer.MapToResponse();

                return TypedResults.CreatedAtRoute(response, GetTrainerEndpoint.Name, new { id = trainer.Id });
            })
            .WithName(Name)
            .Produces<TrainerResponse>(StatusCodes.Status201Created);

            return app;
        }
    }
}