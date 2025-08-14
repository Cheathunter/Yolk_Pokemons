
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
            Assert.Equal(trainer.Id, result.Id);
        }

        [Fact]
        public async Task GetTrainerByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            const int id = 0;

            var trainer = await _repository.GetTrainerByIdAsync(id, CancellationToken.None);

            Assert.Null(trainer);
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
            Assert.Equal(trainer1, result.ElementAt(0));
            Assert.Equal(trainer2, result.ElementAt(1));
        }

        #endregion

        #region GetLastTrainerIdAsync

        [Fact]
        public async Task GetLastTrainerIdAsync_ShouldReturZero_WhenNoTrainerExist()
        {
            int lastTrainerId = await _repository.GetLastTrainerIdAsync(CancellationToken.None);

            Assert.Equal(0, lastTrainerId);
        }

        [Fact]
        public async Task GetLastTrainerIdAsync_ShouldReturnHighestId_WhenTrainersExist()
        {
            var trainer1 = new Trainer
            {
                Id = 3,
                Name = "Ash"
            };
            var trainer2 = new Trainer
            {
                Id = 1,
                Name = "Brok"
            };

            await _repository.CreateTrainerAsync(trainer1, CancellationToken.None);
            await _repository.CreateTrainerAsync(trainer2, CancellationToken.None);

            int lastTrainerId = await _repository.GetLastTrainerIdAsync(CancellationToken.None);

            Assert.Equal(trainer1.Id, lastTrainerId);
        }

        #endregion
    }
}