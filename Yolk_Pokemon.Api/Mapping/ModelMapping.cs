using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Requests;
using Yolk_Pokemon.Application.Responses;

namespace Yolk_Pokemon.Api.Mapping
{
    public static class ModelMapping
    {
        public static Trainer MapToTrainer(this CreateTrainerRequest request)
        {
            return new()
            {
                Id = 0,
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
    }
}
