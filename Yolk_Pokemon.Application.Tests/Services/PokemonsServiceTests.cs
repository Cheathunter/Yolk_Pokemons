
using Moq;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;
using Yolk_Pokemon.Application.Services;

namespace Yolk_Pokemon.Application.Tests.Services
{
    public class PokemonsServiceTests
    {
        #region Private Variables

        private readonly Mock<IPokemonsRepository> _pokemonsRepoMock = new();
        private readonly Mock<ITrainersRepository> _trainersRepoMock = new();

        #endregion

        #region GetPokemonByIdAsync

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnNull_WhenPokemonNotFound()
        {
            _pokemonsRepoMock
                .Setup(r => r.GetPokemonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Pokemon?)null);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.GetPokemonByIdAsync(0);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnPokemon_WhenFound()
        {
            var pokemon = new Pokemon
            {
                Id = 0,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 4
            };

            _pokemonsRepoMock
                .Setup(r => r.GetPokemonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.GetPokemonByIdAsync(pokemon.Id);

            Assert.Equal(result, pokemon);
        }

        #endregion
        
        #region GetAllPokemonsAsync

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnListOfPokemons_WhenPokemonsArePresent()
        {
            var trainers = new List<Pokemon> {
                new() {
                    Id = 0,
                    Name = "Pikachu",
                    Health = 20,
                    Level = 1,
                    TypeId = 4
                },
                new()
                {
                    Id = 0,
                    Name = "Bulbasaur",
                    Health = 22,
                    Level = 2,
                    TypeId = 3
                }
            };
            var options = new Mock<GetAllPokemonsOptions>();

            _pokemonsRepoMock
                .Setup(r => r.GetAllPokemonsAsync(It.IsAny<GetAllPokemonsOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainers);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.GetAllPokemonsAsync(options.Object);

            Assert.Equal(result, trainers);
        }

        #endregion
        
        #region AddPokemonToTrainer

        [Fact]
        public async Task AddPokemonToTrainer_ShouldReturnNull_WhenTrainerNotFound()
        {
            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Trainer?)null);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.AddPokemonToTrainer(0, 0);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddPokemonToTrainer_ShouldReturnNull_WhenPokemonNotFound()
        {
            var trainer = new Mock<Trainer>();

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer.Object);
            _pokemonsRepoMock
                .Setup(r => r.GetPokemonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Pokemon?)null);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.AddPokemonToTrainer(0, 0);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddPokemonToTrainer_ShouldThrow_WhenPokemonWasAlreadyAdded()
        {
            int trainerId = 0;
            var trainer = new Mock<Trainer>();
            var pokemon = new Mock<Pokemon>();
            pokemon.Object.OwnerId = trainerId;

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer.Object);
            _pokemonsRepoMock
                .Setup(r => r.GetPokemonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon.Object);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);
            
            await Assert.ThrowsAsync<DuplicateRecordException>(() => service.AddPokemonToTrainer(0, trainerId));
        }

        [Fact]
        public async Task AddPokemonToTrainer_ShouldReturnUpdatedTrainer_WhenPokemonWasAdded()
        {
            var trainer = new Trainer
            {
                Id = 0,
                Name = "Ash"
            };
            var pokemon = new Mock<Pokemon>();

            _trainersRepoMock
                .Setup(r => r.GetTrainerByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainer);
            _pokemonsRepoMock
                .Setup(r => r.GetPokemonByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pokemon.Object);

            var service = new PokemonsService(_pokemonsRepoMock.Object, _trainersRepoMock.Object);

            var result = await service.AddPokemonToTrainer(0, 0);
            trainer.Pokemons.Add(pokemon.Object);

            Assert.Equal(trainer, result);
        }

        #endregion
    }
}