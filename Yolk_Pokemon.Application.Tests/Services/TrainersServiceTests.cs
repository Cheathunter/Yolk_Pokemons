
using Moq;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Application.Tests.Services
{
    public class TrainersServiceTests
    {
        #region Private Variables

        private readonly Mock<ITrainersRepository> _trainersRepoMock = new();

        #endregion
        
        #region GetTrainerByIdAsync

        [Fact]
        public async Task GetTrainerByIdAsync_ShouldReturnNull_WhenTrainerNotFound()
        {
            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Trainer?)null);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.GetTrainerByIdAsync(0);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetTrainerByIdAsync_ShouldReturnTrainer_WhenFound()
        {
            var trainer = new Trainer
            {
                Id = 0,
                Name = "Ash"
            };

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.GetTrainerByIdAsync(trainer.Id);

            Assert.Equal(result, trainer);
        }

        #endregion

        #region GetAllTrainersAsync

        [Fact]
        public async Task GetAllTrainersAsync_ShouldReturnListOfTrainers_WhenTrainersArePresent()
        {
            var trainers = new List<Trainer> {
                new() {
                    Id = 0,
                    Name = "Ash"
                },
                new()
                {
                    Id = 1,
                    Name = "Brok"
                }
            };

            _trainersRepoMock
                .Setup(r => r.GetAllTrainersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainers);
            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.GetAllTrainersAsync();

            Assert.Equal(result, trainers);
        }

        #endregion

        #region UpdateTrainerAsync

        [Fact]
        public async Task UpdateTrainerAsync_ShouldReturnNull_WhenTrainerNotFound()
        {
            var trainerMock = new Mock<Trainer>();

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Trainer?)null);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.UpdateTrainerAsync(trainerMock.Object);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTrainerAsync_ShouldReturnNull_WhenUpdatingFails()
        {
            var trainer = new Trainer
            {
                Id = 0,
                Name = "Ash"
            };

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer);
            _trainersRepoMock
                .Setup(r => r.UpdateTrainerAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.UpdateTrainerAsync(trainer);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTrainerAsync_ShouldReturnUpdatedTrainer_WhenTrainerFound()
        {
            var trainer = new Trainer
            {
                Id = 0,
                Name = "Ash"
            };

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer);
            _trainersRepoMock
                .Setup(r => r.UpdateTrainerAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.UpdateTrainerAsync(trainer);

            Assert.Equal(result, trainer);
        }

        [Fact]
        public async Task UpdateTrainerAsync_ShouldReturnUpdatedTrainerWithPokemons_WhenTrainerFoundAndHasPokemons()
        {
            var trainer = new Trainer
            {
                Id = 0,
                Name = "Ash"
            };
            var previousTrainer = new Trainer
            {
                Id = 0,
                Name = "Brok",
                Pokemons = [new Pokemon
                {
                    Id = 0,
                    Name = "Pikachu",
                    Health = 20,
                    Level = 1,
                    TypeId = 4
                }]
            };

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(previousTrainer);
            _trainersRepoMock
                .Setup(r => r.UpdateTrainerAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.UpdateTrainerAsync(trainer);

            Assert.NotNull(result);
            Assert.Equal(previousTrainer.Id, result.Id);
            Assert.NotEqual(previousTrainer.Name, result.Name);
            Assert.Equal(previousTrainer.Pokemons, result.Pokemons);
        }

        #endregion

        #region DeleteByIdAsync

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task DeleteByIdAsync_ShouldReturnBool_WhenTrainerRemovalIsProcessed(bool removed)
        {
            _trainersRepoMock
                .Setup(r => r.DeleteByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(removed);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.DeleteByIdAsync(0);

            Assert.Equal(result, removed);
        }

        #endregion

        #region GetNewTrainerIdAsync

        [Fact]
        public async Task GetNewTrainerIdAsync_ShouldReturnIncrementedNumberOfLastTrainer()
        {
            int lastTrainerId = 5;
            _trainersRepoMock
                .Setup(r => r.GetLastTrainerIdAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(lastTrainerId);

            var service = new TrainersService(_trainersRepoMock.Object);

            var result = await service.GetNewTrainerIdAsync();

            Assert.Equal(result, lastTrainerId + 1);
        }

        #endregion
    }
}