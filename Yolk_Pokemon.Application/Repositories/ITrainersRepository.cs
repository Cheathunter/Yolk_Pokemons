
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public interface ITrainersRepository
    {
        Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token);
    }
}