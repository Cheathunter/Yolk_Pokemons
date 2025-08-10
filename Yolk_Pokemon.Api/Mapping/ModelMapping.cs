using System.Text.Json;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;

namespace Yolk_Pokemon.Api.Mapping
{
    public static class ModelMapping
    {
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

        public static Trainer MapToTrainer(this UpdateTrainerRequest request, int id)
        {
            return new()
            {
                Id = id,
                Name = request.Name,
                Region = request.Region,
                Age = request.Age,
                Wins = request.Wins,
                Losses = request.Losses,
                Pokemons = request.Pokemons
            };
        }

        public static TrainersResponse MapToResponse(this IEnumerable<Trainer> responses)
        {
            return new TrainersResponse
            {
                Trainers = responses.Select(MapToResponse)
            };
        }

        public static Pokemon MapToPokemon(this CreatePokemonRequest request)
        {
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                Level = request.Level,
                Health = request.Health,
                PokemonMoves = request.PokemonMoves,
                Type = request.Type
            };
        }

        public static PokemonMoveDetail MapToPokemonMoveDetail(this PokemonMove pokemonMove)
        {
            return new()
            {
                MoveName = pokemonMove.Move.Name,
                MoveType = pokemonMove.Move.Type.Name,
                Power = pokemonMove.Move.Power
            };
        }

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

        public static PokemonsResponse MapToResponse(this IEnumerable<Pokemon> responses)
        {
            return new PokemonsResponse
            {
                Pokemons = responses.Select(MapToResponse)
            };
        }

        public static IResult ToGenericResponse<T>(this T? data, string message, int statusCode, bool success = true)
        {
            var response = new GenericResponse<T>
            {
                Success = success,
                StatusCode = statusCode,
                Message = message,
                Data = data is null ? null : [data]
            };
            return Results.Json(response, (JsonSerializerOptions?)null, null, statusCode);
        }
    }
}
