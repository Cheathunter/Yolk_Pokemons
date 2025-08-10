using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class TrainersRepository(PokemonDbContext context) : ITrainersRepository
    {
        private readonly PokemonDbContext _context = context;

        public async Task CreateTrainerAsync(Trainer trainer, CancellationToken token = default)
        {
            await _context.Trainers.AddAsync(trainer, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default)
        {
            var trainer = await _context.Trainers
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.PokemonMoves)
                        .ThenInclude(pm => pm.Move)
                            .ThenInclude(m => m.Type)
                .SingleOrDefaultAsync(t => t.Id == id, token);

            return trainer;
        }

        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default)
        {
            return await _context.Trainers
                .Include(static t => t.Pokemons)
                    .ThenInclude(static p => p.PokemonMoves)
                        .ThenInclude(static pm => pm.Move)
                            .ThenInclude(static m => m.Type)
                .ToListAsync(token);
        }

        public async Task<bool> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default)
        {
            var result = await _context.Trainers
                .Where(t => t.Id == trainer.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.Name, trainer.Name)
                    .SetProperty(t => t.Region, trainer.Region)
                    .SetProperty(t => t.Age, trainer.Age)
                    .SetProperty(t => t.Wins, trainer.Wins)
                    .SetProperty(t => t.Losses, trainer.Losses), token);

            await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken token = default)
        {
            await _context.Pokemons
                .Where(p => p.OwnerId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.OwnerId, (int?)null), token);

            var result = await _context.Trainers
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync(token);

            await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<int> GetLastTrainerIdAsync(CancellationToken token = default)
        {
            if (await _context.Trainers.AnyAsync(token))
            {
                return await _context.Trainers.MaxAsync(t => t.Id, token);
            }

            return 0;
        }
    }
}