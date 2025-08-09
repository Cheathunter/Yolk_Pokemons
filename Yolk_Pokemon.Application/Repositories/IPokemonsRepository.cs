
using Yolk_Pokemon.Application.Models;

namespace Yolk_Pokemon.Application.Repositories
{
    public interface IPokemonsRepository
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default);
    }
}