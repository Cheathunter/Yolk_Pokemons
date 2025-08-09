
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class PokemonsRepository : IPokemonsRepository
    {
        private readonly Dictionary<int, Pokemon> _pokemons = [];

        public Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IEnumerable<Pokemon>)_pokemons.Values);
        }
    }
}