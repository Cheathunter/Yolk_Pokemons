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
        private const string SuccessfulMessage = "Trainer created successfully.";

        public static IEndpointRouteBuilder MapCreateTrainer(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Trainers.Create, async (
                CreateTrainerRequest request, ITrainersService trainerService, CancellationToken token) =>
            {
                int trainerId = await trainerService.GetNewTrainerIdAsync(token);

                Trainer trainer = request.MapToTrainer(trainerId);
                await trainerService.CreateTrainerAsync(trainer, token);
                var response = trainer.MapToResponse();

                return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status201Created);
            })
            .WithName(Name)
            .Produces<GenericResponse<TrainerResponse>>(StatusCodes.Status201Created);

            return app;
        }
    }
}