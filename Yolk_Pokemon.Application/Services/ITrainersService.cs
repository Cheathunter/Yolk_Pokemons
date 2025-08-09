
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    public interface ITrainersService
    {
        Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token);
    }
}