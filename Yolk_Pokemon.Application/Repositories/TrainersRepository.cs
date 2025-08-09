using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class TrainersRepository : ITrainersRepository
    {
        private readonly Dictionary<int, Trainer> _trainers = [];

        public Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token = default)
        {
            bool result = _trainers.TryAdd(trainer.Id, trainer);
            
            if (!result)
            {
                throw new DuplicateRecordException($"Attempt to save duplicated trainer '{trainer.Id}'");
            }

            return Task.FromResult(true);
        }

        public Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default)
        {
            return Task.FromResult(_trainers.GetValueOrDefault(id));
        }

        public Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default)
        {
            return Task.FromResult((IEnumerable<Trainer>)_trainers.Values);
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
    }
}