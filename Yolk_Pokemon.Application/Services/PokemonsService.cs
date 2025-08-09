
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    public class PokemonsService(IPokemonsRepository pokemonsRepository) : IPokemonsService
    {
        private readonly IPokemonsRepository _pokemonsRepository = pokemonsRepository;

        public async Task<bool> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            return await _pokemonsRepository.CreatePokemonAsync(pokemon, cancellationToken);
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default)
        {
            return await _pokemonsRepository.GetAllPokemonsAsync(cancellationToken);
        }
    }
}