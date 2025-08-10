
namespace Yolk_Pokemon.Application.Responses
{
    public class TrainerResponse
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public string? Region { get; init; }

        public int? Age { get; init; }

        public DateTime? CreatedAt { get; init; }

        public int Wins { get; init; }

        public int Losses { get; init; }

        public virtual ICollection<PokemonResponse> Pokemons { get; init; } = [];
    }
}