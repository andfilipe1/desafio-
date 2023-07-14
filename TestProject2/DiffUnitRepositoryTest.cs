using Domain.Model.Enuns;

namespace TestProject2
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

        [Fact]
        public async Task GetByIdAsync_WhenItemNotFound_ShouldReturnNull()
        {
            // Act
            var result = await _diffRepository.GetByIdAsync(2021);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoItems_ShouldReturnEmptyCollection()
        {
            // Act
            var result = await _diffRepository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
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
            Assert.Equal(itemDiff.Id, result.Id);
            Assert.Equal(itemDiff.Left, result.Left);
            Assert.Equal(itemDiff.Right, result.Right);
        }

        [Fact]
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
            Assert.Equal(updatedItemDiff.Id, result.Id);
            Assert.Equal(updatedItemDiff.Left, result.Left);
            Assert.Equal(updatedItemDiff.Right, result.Right);
        }

        [Fact]
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
            Assert.Equal(id, result.Id);
            Assert.Equal(decodedData, result.Left);
        }

        [Fact]
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
            Assert.Equal(id, result.Id);
            Assert.Equal(updatedData, result.Left);
        }
    }
}
