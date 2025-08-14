
using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    /// <summary>
    /// Entity framework repository for Pokemons.
    /// </summary>
    /// <param name="context">Database context.</param>
    public class PokemonsRepository(PokemonDbContext context) : IPokemonsRepository
    {
        private readonly PokemonDbContext _context = context;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(GetAllPokemonsOptions options, CancellationToken token = default)
        {
            IQueryable<Pokemon> query = _context.Pokemons
                .AsNoTracking()
                .Where(p =>
                    // apply filter
                    (options.Type == null || EF.Functions.ILike(p.Type.Name, options.Type)) &&
                    (options.Region == null || (p.Owner != null && p.Owner.Region != null && EF.Functions.ILike(p.Owner.Region, options.Region)))
                )
                .Include(p => p.PokemonMoves)
                    .ThenInclude(pm => pm.Move)
                        .ThenInclude(m => m.Type)
                .Include(p => p.Type)
                .Include(p => p.Owner);

            // Aply sort mechanizm
            if (options.SortedOrder != SortedOrder.Unsorted && options.SortedField != null)
            {
                query = options.SortedField switch
                {
                    "name" => options.SortedOrder == SortedOrder.Ascending ? query.OrderBy(static q => q.Name) : query.OrderByDescending(static q => q.Name),
                    "level" => options.SortedOrder == SortedOrder.Ascending ? query.OrderBy(static q => q.Level) : query.OrderByDescending(static q => q.Level),
                    "caughtAt" => options.SortedOrder == SortedOrder.Ascending ? query.OrderBy(static q => q.CaughtAt) : query.OrderByDescending(static q => q.CaughtAt),
                    _ => query.OrderBy(static q => q.Id)
                };
            }

            var pokemons = await query
                    .ToListAsync(token);

            return pokemons;
        }

        /// <inheritdoc/>
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