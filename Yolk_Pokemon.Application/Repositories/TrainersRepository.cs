

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
    }
}