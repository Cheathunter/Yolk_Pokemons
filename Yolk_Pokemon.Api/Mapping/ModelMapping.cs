using System.Text.Json;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;

namespace Yolk_Pokemon.Api.Mapping
{
    /// <summary>
    /// Class to map objects like Trainer and Pokemon request/response
    /// </summary>
    public static class ModelMapping
    {
        private const string DescOrder = "desc";

        /// <summary>
        /// Extension method of <see cref="CreateTrainerRequest"/>.
        /// Maps <see cref="CreateTrainerRequest"/> to <see cref="Trainer"/>.
        /// </summary>
        /// <param name="request">CreateTrainerRequest object used for maping.</param>
        /// <param name="id">Id of new Trainer</param>
        /// <returns>Remapped Trainer object.</returns>
        public static Trainer MapToTrainer(this CreateTrainerRequest request, int id)
        {
            return new()
            {
                Id = id,
                Name = request.Name,
                Region = request.Region,
                Age = request.Age,
                CreatedAt = DateTime.Now,
                Wins = request.Wins,
                Losses = request.Losses
            };
        }

        /// <summary>
        /// Extension method of <see cref="Trainer"/>.
        /// Maps <see cref="Trainer"/> to <see cref="TrainerResponse"/>.
        /// </summary>
        /// <param name="trainer">Trainer object used for maping.</param>
        /// <returns>Remapped TrainerResponse object.</returns>
        public static TrainerResponse MapToResponse(this Trainer trainer)
        {
            return new()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Region = trainer.Region,
                Age = trainer.Age,
                CreatedAt = trainer.CreatedAt,
                Wins = trainer.Wins,
                Losses = trainer.Losses,
                Pokemons = [.. trainer.Pokemons.Select(MapToResponse)]
            };
        }

        /// <summary>
        /// Extension method of <see cref="UpdateTrainerRequest"/>.
        /// Maps <see cref="UpdateTrainerRequest"/> to <see cref="Trainer"/>.
        /// </summary>
        /// <param name="request">UpdateTrainerRequest object used for maping.</param>
        /// <param name="id">Id of updated Trainer</param>
        /// <returns>Remapped Trainer object.</returns>
        public static Trainer MapToTrainer(this UpdateTrainerRequest request, int id)
        {
            return new()
            {
                Id = id,
                Name = request.Name,
                Region = request.Region,
                Age = request.Age,
                Wins = request.Wins,
                Losses = request.Losses
            };
        }

        /// <summary>
        /// Extension method of <see cref="IEnumerable{Trainer}"/>.
        /// Maps IEnumerable of <see cref="Trainer"/> to <see cref="TrainerResponse"/>.
        /// </summary>
        /// <param name="trainers">Set of Trainer object used for maping.</param>
        /// <returns>Remapped TrainersResponse object.</returns>
        public static TrainersResponse MapToResponse(this IEnumerable<Trainer> trainers)
        {
            return new TrainersResponse
            {
                Trainers = trainers.Select(MapToResponse)
            };
        }

        /// <summary>
        /// Extension method of <see cref="CreatePokemonRequest"/>.
        /// Maps <see cref="CreatePokemonRequest"/> to <see cref="Pokemon"/>.
        /// </summary>
        /// <param name="request">CreatePokemonRequest object used for maping.</param>
        /// <returns>Remapped Pokemon object.</returns>
        public static Pokemon MapToPokemon(this CreatePokemonRequest request)
        {
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                Level = request.Level,
                Health = request.Health,
                TypeId = request.TypeId
            };
        }

        /// <summary>
        /// Extension method of <see cref="PokemonMove"/>.
        /// Maps <see cref="PokemonMove"/> to <see cref="PokemonMoveDetail"/>.
        /// </summary>
        /// <param name="pokemonMove">PokemonMove object used for maping.</param>
        /// <returns>Remapped PokemonMoveDetail object.</returns>
        public static PokemonMoveDetail MapToPokemonMoveDetail(this PokemonMove pokemonMove)
        {
            return new()
            {
                MoveName = pokemonMove.Move.Name,
                MoveType = pokemonMove.Move.Type.Name,
                Power = pokemonMove.Move.Power
            };
        }

        /// <summary>
        /// Extension method of <see cref="Pokemon"/>.
        /// Maps <see cref="Pokemon"/> to <see cref="PokemonResponse"/>.
        /// </summary>
        /// <param name="pokemon">Pokemon object used for maping.</param>
        /// <returns>Remapped PokemonResponse object.</returns>
        public static PokemonResponse MapToResponse(this Pokemon pokemon)
        {
            return new()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Level = pokemon.Level,
                Health = pokemon.Health,
                CaughtAt = pokemon.CaughtAt,
                OwnerId = pokemon.OwnerId,
                PokemonMoves = [.. pokemon.PokemonMoves.Select(MapToPokemonMoveDetail)],
                Type = pokemon.Type.Name
            };
        }

        /// <summary>
        /// Extension method of <see cref="IEnumerable{Pokemon}"/>.
        /// Maps IEnumerable of <see cref="Pokemon"/> to <see cref="PokemonsResponse"/>.
        /// </summary>
        /// <param name="pokemons">Set of Pokemon object used for maping.</param>
        /// <returns>Remapped PokemonsResponse object.</returns>
        public static PokemonsResponse MapToResponse(this IEnumerable<Pokemon> pokemons)
        {
            return new PokemonsResponse
            {
                Pokemons = pokemons.Select(MapToResponse)
            };
        }

        public static GenericResponse<T> MapToGenericResponse<T>(this T? data, string message, int statusCode, bool success = true)
        {
            return new GenericResponse<T>
            {
                Success = success,
                StatusCode = statusCode,
                Message = message,
                Data = data is null ? null : [data]
            };
        }

        /// <summary>
        /// Extension method of <see cref="IResult"/>.
        /// Maps any response to JSon <see cref="GenericResponse{T}"/>.
        /// </summary>
        /// <param name="data">Response data.</param>
        /// <param name="message">Message.</param>
        /// <param name="statusCode">HTTP Status code.</param>
        /// <param name="success">Indication of successful response.</param>
        /// <returns>IResult object containing generic response.</returns>
        public static IResult ToGenericResponse<T>(this T? data, string message, int statusCode, bool success = true)
        {
            var response = MapToGenericResponse(data, message, statusCode, success);
            return Results.Json(response, (JsonSerializerOptions?)null, null, statusCode);
        }

        /// <summary>
        /// Extension method of <see cref="GetAllPokemonsRequest"/>.
        /// Maps <see cref="GetAllPokemonsRequest"/> to <see cref="GetAllPokemonsOptions"/>.
        /// SortedOrder is unsorted when SortBy not used, otherwise Ascending or Descending.
        /// </summary>
        /// <param name="request">GetAllPokemonsRequest object used for maping.</param>
        /// <returns>Remapped GetAllPokemonsOptions object.</returns>
        public static GetAllPokemonsOptions MapToOptions(this GetAllPokemonsRequest request)
        {
            var sortOptions = request.SortBy?.Split(':');

            return new GetAllPokemonsOptions
            {
                Type = request.Type,
                Region = request.Region,
                SortedField = sortOptions?.Length > 0 ? sortOptions[0] : null,
                SortedOrder = sortOptions is null ? SortedOrder.Unsorted :
                sortOptions.Length == 1 ? SortedOrder.Ascending :
                sortOptions[1].Equals(DescOrder, StringComparison.Ordinal) ? SortedOrder.Descending : SortedOrder.Ascending
            };
        }
    }
}
