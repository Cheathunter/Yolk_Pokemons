
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Responses
{
    public class PokemonsResponse
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public required int Level { get; init; }

        public required int Health { get; init; }

        public DateTime? CaughtAt { get; init; }

        public virtual Trainer? Owner { get; init; }

        public virtual ICollection<PokemonMove> PokemonMoves { get; init; } = [];

        public required Element Type { get; init; }
    }
}