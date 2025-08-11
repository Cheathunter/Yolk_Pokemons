
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    /// <summary>
    /// Trainer repository
    /// </summary>
    public interface ITrainersRepository
    {
        /// <summary>
        /// Creates a <see cref="Trainer"/> in the current storage.
        /// </summary>
        /// <param name="trainer">Object to store.</param>
        /// <param name="token">Cancellation token.</param>
        Task CreateTrainerAsync(Trainer trainer, CancellationToken token = default);

        /// <summary>
        /// Finds and returns <see cref="Trainer"/> object with the selected Id.
        /// </summary>
        /// <param name="id">Id of the Trainer.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trainer with the selected id. Null if it does not exist.</returns>
        Task<Trainer?> GetTrainerByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Finds and returns set of <see cref="Trainer"/> objects.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>All Trainers in the storage.</returns>
        Task<IEnumerable<Trainer>> GetAllTrainersAsync(CancellationToken token = default);

        /// <summary>
        /// Updates the <see cref="Trainer"/> record in the current storage.
        /// </summary>
        /// <param name="trainer">Trainer object to store.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if record is updated.</returns>
        Task<bool> UpdateTrainerAsync(Trainer trainer, CancellationToken token = default);

        /// <summary>
        /// Deletes the <see cref="Trainer"/> record in the current storage.
        /// </summary>
        /// <param name="id">Id of the selected Trainer.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if record is removed.</returns>
        Task<bool> DeleteByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Gets the highest ID of Trainers table.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Id of the last trainer.</returns>
        Task<int> GetLastTrainerIdAsync(CancellationToken token = default);
    }
}