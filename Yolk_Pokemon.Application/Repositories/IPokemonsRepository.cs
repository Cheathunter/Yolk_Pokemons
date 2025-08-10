
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public interface IPokemonsRepository
    {
        Task<bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default);

        Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default);

        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken token = default);

        Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken token = default);
    }
}