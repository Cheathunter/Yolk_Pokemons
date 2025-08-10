
using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class PokemonsRepository(PokemonDbContext context) : IPokemonsRepository
    {
        private readonly Dictionary<int, Pokemon> _pokemons = [];
        private readonly PokemonDbContext _context = context;

        public Task<bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            bool result = _pokemons.TryAdd(pokemon.Id, pokemon);

            if (!result)
            {
                throw new DuplicateRecordException($"Attempt to save duplicated pokemon with id='{pokemon.Id}'");
            }

            return Task.FromResult(true);
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default)
        {
            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonMoves)
                    .ThenInclude(pm => pm.Move)
                        .ThenInclude(m => m.Type)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .SingleOrDefaultAsync(p => p.Id == id, token);

            return pokemon;
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