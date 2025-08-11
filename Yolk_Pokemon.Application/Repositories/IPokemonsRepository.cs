
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    /// <summary>
    /// Pokemon repository
    /// </summary>
    public interface IPokemonsRepository
    {
        /// <summary>
        /// Creates a <see cref="Pokemon"/> in the current storage.
        /// </summary>
        /// <param name="pokemon">Object to store.</param>
        /// <param name="token">Cancellation token.</param>
        /// <exception cref="DuplicateRecordException">In case of duplicate creation.</exception>
        /// <exception cref="InvalidOperationException">In case of saving non-existent Pokemon type.</exception>
        Task CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default);

        /// <summary>
        /// Finds and returns <see cref="Pokemon"/> object with the selected Id.
        /// </summary>
        /// <param name="id">Id of the Pokemon.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pokemon with the selected id. Null if it does not exist.</returns>
        Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Finds and returns set of <see cref="Pokemon"/> objects.
        /// </summary>
        /// <param name="options">Options to sort and/or filter records.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>All Pokemons in the storage filtered and sorted by options argument.</returns>
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(GetAllPokemonsOptions options, CancellationToken token = default);

        /// <summary>
        /// Updates the Owner field of <see cref="Pokemon"/> record in the current storage.
        /// </summary>
        /// <param name="pokemonId">Id of added Pokemon.</param>
        /// <param name="trainerId">Id of selected Trainer.</param>
        /// <param name="token">Cancellation token.</param>
        Task UpdatePokemonsOwnerAsync(int pokemonId, int trainerId, CancellationToken token = default);
    }
}