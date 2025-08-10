
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    public interface ITrainersService
    {
        Task CreateTrainerAsync(Trainer trainer, CancellationToken token = default);

        Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default);

        Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default);

        Task<Trainer?> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default);

        Task<bool> DeleteByIdAsync(int id, CancellationToken token = default);

        Task<int> GetNewTrainerIdAsync(CancellationToken token = default);
    }
}