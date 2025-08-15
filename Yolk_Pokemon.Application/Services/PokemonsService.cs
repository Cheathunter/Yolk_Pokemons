
using FluentValidation;
using Yolk_Pokemon.Application.Validators;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Services
{
    /// <summary>
    /// Service for maintaining Pokemons.
    /// </summary>
    /// <param name="pokemonsRepository">Pokemon repository.</param>
    /// <param name="trainersRepository">Trainer repository.</param>
    /// <param name="pokemonValidator">Validator for Pokemon - <see cref="PokemonValidator"/>.</param>
    public class PokemonsService(IPokemonsRepository pokemonsRepository,
        ITrainersRepository trainersRepository, IValidator<Pokemon> pokemonValidator) : IPokemonsService
    {
        private readonly IPokemonsRepository _pokemonsRepository = pokemonsRepository;
        private readonly ITrainersRepository _trainersRepository = trainersRepository;
        private readonly IValidator<Pokemon> _pokemonValidator = pokemonValidator;

        /// <inheritdoc/>
        public async Task CreatePokemonAsync(Pokemon pokemon, CancellationToken token = default)
        {
            await _pokemonValidator.ValidateAndThrowAsync(pokemon, token);
            await _pokemonsRepository.CreatePokemonAsync(pokemon, token);
        }

        /// <inheritdoc/>
        public async Task<Pokemon?> GetPokemonByIdAsync(int id, CancellationToken token = default)
        {
            return await _pokemonsRepository.GetPokemonByIdAsync(id, token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(GetAllPokemonsOptions options, CancellationToken token = default)
        {
            return await _pokemonsRepository.GetAllPokemonsAsync(options, token);
        }

        /// <inheritdoc/>
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

            if (pokemon.OwnerId == trainerId)
            {
                throw new DuplicateRecordException($"Attempt to add duplicated pokémon '{pokemonId}' to trainer '{trainerId}'");
            }

            pokemon.OwnerId = trainerId;
            trainer.Pokemons.Add(pokemon);
            await _pokemonsRepository.UpdatePokemonsOwnerAsync(pokemonId, trainerId, token);

            return trainer;
        }
    }
}