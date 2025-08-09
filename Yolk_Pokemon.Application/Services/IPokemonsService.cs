
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Services
{
    public interface IPokemonsService
    {
        Task <bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default);

        Task <IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default);
    }
}