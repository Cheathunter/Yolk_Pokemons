
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class PokemonsRepository : IPokemonsRepository
    {
        private readonly Dictionary<int, Pokemon> _pokemons = [];

        public Task<bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            bool result = _pokemons.TryAdd(pokemon.Id, pokemon);

            if (!result)
            {
                throw new DuplicateRecordException($"Attempt to save duplicated pokemon with id='{pokemon.Id}'");
            }

            return Task.FromResult(true);
        }

        public Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default)
        {
            return Task.FromResult(_pokemons.GetValueOrDefault(id));
        }

        public Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IEnumerable<Pokemon>)_pokemons.Values);
        }

        public Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken token = default)
        {
            return Task.FromResult(_pokemons[pokemon.Id] = pokemon);
        }
    }
}