
using Microsoft.EntityFrameworkCore;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Tests.Repositories
{
    public class TrainersRepositoryTests
    {
        #region Private Data

        private readonly PokemonDbContext _context;
        private readonly TrainersRepository _repository;

        #endregion

        #region Before Test

        public TrainersRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PokemonDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _repository = new(_context);
        }

        #endregion

        #region GetTrainerByIdAsync

        [Fact]
        public async Task GetTrainerByIdAsync_ShouldReturnTrainer_WhenExists()
        {
            var trainer = new Trainer
            {
                Id = 1,
                Name = "Ash"
            };

            await _repository.CreateTrainerAsync(trainer, CancellationToken.None);

            var result = await _repository.GetTrainerByIdAsync(trainer.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(trainer.Id, trainer.Id);
        }

        [Fact]
        public async Task GetTrainerByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            const int id = 0;

            var document = await _repository.GetTrainerByIdAsync(id, CancellationToken.None);

            Assert.Null(document);
        }

        #endregion

        #region GetAllTrainersAsync

        [Fact]
        public async Task GetAllTrainersAsync_ShouldReturnEmptySet_WhenNoTrainersExist()
        {
            var result = await _repository.GetAllTrainersAsync(CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTrainersAsync_ShouldReturnTrainers_WhenExist()
        {
            var trainer1 = new Trainer
            {
                Id = 1,
                Name = "Ash"
            };
            var trainer2 = new Trainer
            {
                Id = 2,
                Name = "Ash"
            };

            await _repository.CreateTrainerAsync(trainer1, CancellationToken.None);
            await _repository.CreateTrainerAsync(trainer2, CancellationToken.None);

            var result = await _repository.GetAllTrainersAsync(CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(result.ElementAt(0), trainer1);
            Assert.Equal(result.ElementAt(1), trainer2);
        }

        #endregion
    }
}