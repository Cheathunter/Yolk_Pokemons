using Yolk_Pokemon.Api.Mapping;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Api.Endpoints.TrainerManagement
{
    public static class GetAllTrainersEndpoint
    {
        public const string Name = "GetAllTrainers";
        private const string SuccessfulMessage = "Trainers listed successfully.";

        public static IEndpointRouteBuilder MapGetAllTrainers(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Trainers.GetAll, static async (
                [AsParameters] GetAllTrainersRequest request, ITrainersService trainersService, HttpContext context, CancellationToken token) =>
            {
                var trainers = await trainersService.GetAllTrainersAsync(token);
                var response = trainers.MapToResponse();

                return response.ToGenericResponse(SuccessfulMessage, StatusCodes.Status200OK);
            })
            .WithName(Name)
            .Produces<GenericResponse<TrainersResponse>>(StatusCodes.Status200OK);

            return app;
        }
    }
}