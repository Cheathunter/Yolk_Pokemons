
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    public class TrainersService(ITrainersRepository trainersRepository) : ITrainersService
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task<bool> CreateTrainerAsync(Trainer trainer, CancellationToken token)
        {
            return await _trainersRepository.CreateTrainerAsync(trainer, token);
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default)
        {
            return await _trainersRepository.GetTrainerByIdAsync(id, token);
        }
    }
}