
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Requests
{
    public class CreatePokemonRequest
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public required int Level { get; init; }

        public required int Health { get; init; }
        
        public virtual ICollection<PokemonMove> PokemonMoves { get; init; } = [];

        public virtual Element Type { get; init; } = null!;
    }
}