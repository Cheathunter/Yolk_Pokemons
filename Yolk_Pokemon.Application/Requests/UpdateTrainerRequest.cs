

namespace Yolk_Pokemon.Application.Requests
{
    public class UpdateTrainerRequest
    {
        public required string Name { get; init; }

        public string? Region { get; init; }

        public int? Age { get; init; }

        public int Wins { get; init; }

        public int Losses { get; init; }
    }
}