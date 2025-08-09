
namespace Yolk_Pokemon.Application.Responses
{
    public class PokemonsResponse
    {
        public required IEnumerable<PokemonResponse> Pokemons { get; init; } = [];
    }
}