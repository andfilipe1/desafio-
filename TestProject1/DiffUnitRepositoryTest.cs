using Data.Repository;
using Data.Repository.Context;
using Domain.Model.Entities;
using Domain.Model.Enuns;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
    public class DiffUnitRepositoryTest
    {
        private DiffRepository _diffRepository;

        public DiffUnitRepositoryTest()
        {
            DbContextOptions<DiffContext> getDbOptions() =>
                new DbContextOptionsBuilder<DiffContext>()
                    .UseInMemoryDatabase(databaseName: "DiffDB")
                    .Options;
            var context = new DiffContext(getDbOptions());
            _diffRepository = new DiffRepository(context) ?? throw new System.Exception("Failed to create DiffRepository instance.");
        }

        [SetUp]
        public void Setup()
        {
            DbContextOptions<DiffContext> getDbOptions() =>
                new DbContextOptionsBuilder<DiffContext>()
                    .UseInMemoryDatabase(databaseName: "DiffDB")
                    .Options;
            var context = new DiffContext(getDbOptions());
            _diffRepository = new DiffRepository(context);
        }

        [Test]
        public async Task GetByIdAsync_WhenItemNotFound_ShouldReturnNull()
        {
            // Arrange

            // Act
            var result = await _diffRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAsync_WhenNoItems_ShouldReturnEmptyCollection()
        {
            // Arrange

            // Act
            var result = await _diffRepository.GetAllAsync();

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public async Task AddOrUpdateAsync_WhenItemDiffDoesNotExist_ShouldAddNewItem()
        {
            // Arrange
            var itemDiff = new ItemDiff
            {
                Id = 1,
                Left = "Left data",
                Right = "Right data"
            };

            // Act
            await _diffRepository.AddOrUpdateAsync(itemDiff);
            var result = await _diffRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(itemDiff.Id, result.Id);
            Assert.AreEqual(itemDiff.Left, result.Left);
            Assert.AreEqual(itemDiff.Right, result.Right);
        }

        [Test]
        public async Task AddOrUpdateAsync_WhenItemDiffExists_ShouldUpdateExistingItem()
        {
            // Arrange
            var itemDiff = new ItemDiff
            {
                Id = 1,
                Left = "Old left data",
                Right = "Old right data"
            };

            await _diffRepository.AddOrUpdateAsync(itemDiff);

            var updatedItemDiff = new ItemDiff
            {
                Id = 1,
                Left = "New left data",
                Right = "New right data"
            };

            // Act
            await _diffRepository.AddOrUpdateAsync(updatedItemDiff);
            var result = await _diffRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(updatedItemDiff.Id, result.Id);
            Assert.AreEqual(updatedItemDiff.Left, result.Left);
            Assert.AreEqual(updatedItemDiff.Right, result.Right);
        }

        [Test]
        public async Task PutByIdAsync_WhenItemDiffDoesNotExist_ShouldAddNewDiffItem()
        {
            // Arrange
            var id = 1;
            var side = Side.Left;
            var decodedData = "Decoded data";

            // Act
            await _diffRepository.PutByIdAsync(id, side, decodedData);
            var result = await _diffRepository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(decodedData, result.Left);
        }

        [Test]
        public async Task PutByIdAsync_WhenItemDiffExists_ShouldUpdateExistingDiffItem()
        {
            // Arrange
            var id = 1;
            var originalData = "Original data";
            var updatedData = "Updated data";

            await _diffRepository.PutByIdAsync(id, Side.Left, originalData);

            // Act
            await _diffRepository.PutByIdAsync(id, Side.Left, updatedData);
            var result = await _diffRepository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(updatedData, result.Left);
        }
    }
}
