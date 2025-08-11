
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    /// <summary>
    /// Service for maintaining Trainers.
    /// </summary>
    /// <param name="trainersRepository">Trainer repository.</param>
    public class TrainersService(ITrainersRepository trainersRepository) : ITrainersService
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        /// <inheritdoc/>
        public async Task CreateTrainerAsync(Trainer trainer, CancellationToken token)
        {
            await _trainersRepository.CreateTrainerAsync(trainer, token);
        }

        /// <inheritdoc/>
        public async Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default)
        {
            return await _trainersRepository.GetTrainerByIdAsync(id, token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default)
        {
            return await _trainersRepository.GetAllTrainersAsync(token);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<bool> DeleteByIdAsync(int id, CancellationToken token = default)
        {
            return await _trainersRepository.DeleteByIdAsync(id, token);
        }

        /// <inheritdoc/>
        public async Task<int> GetNewTrainerIdAsync(CancellationToken token = default)
        {
            return await _trainersRepository.GetLastTrainerIdAsync(token) + 1;
        }
    }
}