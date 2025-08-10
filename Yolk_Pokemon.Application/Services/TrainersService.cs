
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    public class TrainersService(ITrainersRepository trainersRepository) : ITrainersService
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task CreateTrainerAsync(Trainer trainer, CancellationToken token)
        {
            await _trainersRepository.CreateTrainerAsync(trainer, token);
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default)
        {
            return await _trainersRepository.GetTrainerByIdAsync(id, token);
        }

        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default)
        {
            return await _trainersRepository.GetAllTrainersAsync(token);
        }

        public async Task<Trainer?> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default)
        {
            var currentTrainer = await _trainersRepository.GetTrainerByIdAsync(trainer.Id, token);

            if (currentTrainer == null)
            {
                return null;
            }

            trainer.Pokemons = currentTrainer.Pokemons;
            bool result = await _trainersRepository.UpdateTrainerAsync(trainer, token);

            return result ? trainer : null;
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken token = default)
        {
            return await _trainersRepository.DeleteByIdAsync(id, token);
        }

        public async Task<int> GetNewTrainerIdAsync(CancellationToken token = default)
        {
            return await _trainersRepository.GetLastTrainerIdAsync(token) + 1;
        }
    }
}