
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    public interface IPokemonsService
    {
        Task <bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default);

        Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default);

        Task <IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken token = default);

        Task<Trainer?> AddPokemonToTrainer(int pokemonId, int trainerId, CancellationToken token = default);
    }
}