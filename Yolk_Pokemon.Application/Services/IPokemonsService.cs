
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    /// <summary>
    /// Pokemon service
    /// </summary>
    public interface IPokemonsService
    {
        /// <summary>
        /// Creates a <see cref="Pokemon"/>.
        /// </summary>
        /// <param name="pokemon">Object to store.</param>
        /// <param name="token">Cancellation token.</param>
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
        /// <returns>All Pokemons filtered and sorted by options argument.</returns>
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(GetAllPokemonsOptions options, CancellationToken token = default);

        /// <summary>
        /// Adds selected <see cref="Pokemon"/> to selected <see cref="Trainer"/>.
        /// </summary>
        /// <param name="pokemonId">Id of added Pokemon.</param>
        /// <param name="trainerId">Id of selected Trainer.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated Trainer.</returns>
        Task<Trainer?> AddPokemonToTrainer(int pokemonId, int trainerId, CancellationToken token = default);
    }
}