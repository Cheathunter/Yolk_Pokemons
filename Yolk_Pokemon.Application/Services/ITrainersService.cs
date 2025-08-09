
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    public interface ITrainersService
    {
        Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token = default);

        Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default);

        Task<bool> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default);

        Task<bool> DeleteByIdAsync(int id, CancellationToken token = default);
    }
}