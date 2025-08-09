
namespace Yolk_Pokemon.Application.Responses
{
    public class TrainersResponse
    {
        public required IEnumerable<TrainerResponse> Trainers { get; init; } = [];
    }
}