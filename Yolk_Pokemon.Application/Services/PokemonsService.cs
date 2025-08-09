
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    public class PokemonsService(IPokemonsRepository pokemonsRepository, ITrainersRepository trainersRepository) : IPokemonsService
    {
        private readonly IPokemonsRepository _pokemonsRepository = pokemonsRepository;
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task<Trainer?> AddPokemonToTrainer(int pokemonId, int trainerId, CancellationToken token = default)
        {
            var trainer = await _trainersRepository.GetTrainerByIdAsync(trainerId, token);

            if (trainer == null)
            {
                return null;
            }

            var pokemon = await _pokemonsRepository.GetPokemonByIdAsync(pokemonId, token);

            if (pokemon == null)
            {
                return null;
            }

            if (await _trainersRepository.PokemonAlreadyAddedAsync(trainerId, pokemonId, token))
            {
                throw new DuplicateRecordException($"Attempt to add duplicated pokémon '{pokemonId}' to trainer '{trainerId}'");
            }

            pokemon.OwnerId = trainerId;
            trainer.Pokemons.Add(pokemon);
            await _pokemonsRepository.UpdatePokemonAsync(pokemon, token);
            await _trainersRepository.UpdateTrainerAsync(trainer, token);

            return trainer;
        }

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