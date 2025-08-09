
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public class TrainersRepository : ITrainersRepository
    {
        public async Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token)
        {
            return true;
        }
    }
}