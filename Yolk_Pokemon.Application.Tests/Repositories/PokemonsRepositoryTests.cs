
using Microsoft.EntityFrameworkCore;
using Moq;
using Yolk_Pokemon.Application.Context;
using Yolk_Pokemon.Application.Exceptions;
using Yolk_Pokemon.Application.Models;
using Yolk_Pokemon.Application.Repositories;

namespace Yolk_Pokemon.Application.Tests.Repositories
{
    public class PokemonsRepositoryTests
    {
        #region Private Data

        private readonly PokemonDbContext _context;
        private readonly PokemonsRepository _repository;

        #endregion

        #region Before Test

        public PokemonsRepositoryTests()
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

        #region CreatePokemonAsync

        [Fact]
        public async Task CreatePokemonAsync_ShouldThrow_WhenPokemonAlreadyExists()
        {
            await CreateDummyPokemonType(1, "Electric");

            var pokemon = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon, CancellationToken.None);

            await Assert.ThrowsAsync<DuplicateRecordException>(() => _repository.CreatePokemonAsync(pokemon, CancellationToken.None));
        }

        [Fact]
        public async Task CreatePokemonAsync_ShouldThrow_WhenPokemonTypeDoesntExists()
        {
            var pokemon = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.CreatePokemonAsync(pokemon, CancellationToken.None));
        }

        #endregion

        #region GetPokemonByIdAsync

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnPokemon_WhenExists()
        {
            await CreateDummyPokemonType(1, "Electric");

            var pokemon = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon, CancellationToken.None);

            var result = await _repository.GetPokemonByIdAsync(pokemon.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(pokemon.Id, result.Id);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            const int id = 0;

            var pokemon = await _repository.GetPokemonByIdAsync(id, CancellationToken.None);

            Assert.Null(pokemon);
        }

        #endregion

        #region GetAllPokemonsAsync

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnEmptySet_WhenNoPokemonsExist()
        {
            var options = new Mock<GetAllPokemonsOptions>();

            var result = await _repository.GetAllPokemonsAsync(options.Object, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnPokemons_WhenExist()
        {
            var options = new Mock<GetAllPokemonsOptions>();
            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options.Object, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(0).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(1).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnAscSortedPokemons_ByName_WhenExist()
        {
            const string sortedField = "name";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Ascending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(0).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(1).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnDescSortedPokemons_ByName_WhenExist()
        {
            const string sortedField = "name";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Descending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(1).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(0).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnAscSortedPokemons_ByLevel_WhenExist()
        {
            const string sortedField = "level";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Ascending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(0).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(1).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnDescSortedPokemons_ByLevel_WhenExist()
        {
            const string sortedField = "level";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Descending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(1).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(0).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnAscSortedPokemons_ByCaughtAt_WhenExist()
        {
            const string sortedField = "caughtAt";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Ascending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1,
                CaughtAt = DateTime.MinValue
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1,
                CaughtAt = DateTime.MaxValue
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(0).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(1).Id);
        }

        [Fact]
        public async Task GetAllPokemonsAsync_ShouldReturnDescSortedPokemons_ByCaughtAt_WhenExist()
        {
            const string sortedField = "caughtAt";
            var options = new GetAllPokemonsOptions
            {
                SortedField = sortedField,
                SortedOrder = SortedOrder.Descending
            };

            await CreateDummyPokemonType(1, "Electric");

            var pokemon1 = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1,
                CaughtAt = DateTime.MinValue
            };
            var pokemon2 = new Pokemon
            {
                Id = 2,
                Name = "Zapdos",
                Health = 25,
                Level = 2,
                TypeId = 1,
                CaughtAt = DateTime.MaxValue
            };

            await _repository.CreatePokemonAsync(pokemon1, CancellationToken.None);
            await _repository.CreatePokemonAsync(pokemon2, CancellationToken.None);

            var result = await _repository.GetAllPokemonsAsync(options, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal(pokemon1.Id, result.ElementAt(1).Id);
            Assert.Equal(pokemon2.Id, result.ElementAt(0).Id);
        }

        #endregion

        #region UpdatePokemonsOwnerAsync
        /*
        [Fact]
        public async Task UpdatePokemonsOwnerAsync_ShouldReturnPokemon_WhenExists()
        {
            await CreateDummyPokemonType();

            var pokemon = new Pokemon
            {
                Id = 1,
                Name = "Pikachu",
                Health = 20,
                Level = 1,
                TypeId = 1
            };
            const int trainerId = 0;

            await _repository.CreatePokemonAsync(pokemon, CancellationToken.None);

            var result = await _repository.GetPokemonByIdAsync(pokemon.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Null(result.OwnerId);

            await _repository.UpdatePokemonsOwnerAsync(pokemon.Id, trainerId, CancellationToken.None);

            Assert.Equal(trainerId, result.OwnerId);
        }
        */
        #endregion

        #region Private Methods

        private async Task CreateDummyPokemonType(int typeId, string typeName)
        {
            var type = new Element
            {
                Id = typeId,
                Name = typeName
            };

            await _context.Elements.AddAsync(type);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}