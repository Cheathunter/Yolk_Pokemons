using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class TrainersRepository(PokemonDbContext context) : ITrainersRepository
    {
        private readonly PokemonDbContext _context = context;
        private readonly Dictionary<int, Trainer> _trainers = [];

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

        public Task<bool> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default)
        {
            bool exists = _trainers.TryGetValue(trainer.Id, out _);

            if (exists)
            {
                _trainers[trainer.Id] = trainer;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> DeleteByIdAsync(int id, CancellationToken token = default)
        {
            return Task.FromResult(_trainers.Remove(id));
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