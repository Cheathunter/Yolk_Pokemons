
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Requests
{
    public class CreateTrainerRequest
    {
        public required string Name { get; init; }

        public string? Region { get; init; }

        public int? Age { get; init; }

        public DateTime? CreatedAt { get; init; }

        public int Wins { get; init; } = 0;

        public int Losses { get; init; } = 0;

        public virtual ICollection<Pokemon> Pokemons { get; init; } = [];
    }
}