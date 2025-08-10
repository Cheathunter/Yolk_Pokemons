
using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class PokemonsRepository(PokemonDbContext context) : IPokemonsRepository
    {
        private readonly PokemonDbContext _context = context;

        public async Task CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default)
        {
            bool exists = await _context.Pokemons.AnyAsync(p => p.Id == pokemon.Id, token);

            if (exists)
            {
                throw new DuplicateRecordException($"Attempt to save duplicated pokemon with id='{pokemon.Id}'");
            }

            bool typeExists = await _context.Elements.AnyAsync(e => e.Id == pokemon.TypeId, token);

            if (!typeExists)
            {
                throw new InvalidOperationException($"Attempt to save non-existing type id='{pokemon.TypeId}'");
            }

            await _context.Pokemons.AddAsync(pokemon, token);
            await _context.SaveChangesAsync(token);
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

        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken token = default)
        {
            var pokemons = await _context.Pokemons
                .Include(p => p.PokemonMoves)
                    .ThenInclude(pm => pm.Move)
                        .ThenInclude(m => m.Type)
                .Include(p => p.Type)
                .Include(p => p.Owner).ToListAsync(token);

            return pokemons;
        }

        public async Task UpdatePokemonsOwnerAsync(int pokemonId, int trainerId, CancellationToken token = default)
        {
            await _context.Pokemons
                .Where(p => p.Id == pokemonId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.OwnerId, trainerId), token);

            await _context.SaveChangesAsync(token);
        }
    }
}