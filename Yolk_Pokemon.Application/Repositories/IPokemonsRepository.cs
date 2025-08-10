
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public interface IPokemonsRepository
    {
        Task CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default);

        Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default);

        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(GetAllPokemonsOptions options, CancellationToken token = default);

        Task UpdatePokemonsOwnerAsync(int pokemonId, int trainerId, CancellationToken token = default);
    }
}