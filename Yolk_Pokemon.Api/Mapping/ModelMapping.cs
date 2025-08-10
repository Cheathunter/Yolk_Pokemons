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
                Pokemons = trainer.Pokemons
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
                CreatedAt = request.CreatedAt,
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

        public static PokemonResponse MapToResponse(this Pokemon pokemon)
        {
            return new()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Level = pokemon.Level,
                Health = pokemon.Health,
                CaughtAt = pokemon.CaughtAt,
                Owner = pokemon.Owner,
                PokemonMoves = pokemon.PokemonMoves,
                Type = pokemon.Type
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
