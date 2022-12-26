using Data.Repository;
using Data.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Assert = Xunit.Assert;

namespace TestProject1
{
    [TestClass]
    public class DiffUnitTest
    {
        private readonly DiffRepository _diffRepository;

        public DiffUnitTest()
        {
            DbContextOptions<DiffContext> getDbOptions() =>
                new DbContextOptionsBuilder<DiffContext>()
                    .UseInMemoryDatabase(databaseName: "DiffDB")
                    .Options;
            var context = new DiffContext(getDbOptions());
            _diffRepository = new DiffRepository(context);
        }


        [TestMethod]
        public async Task Step1_GetById_CheckNotFound()
        {
            var resultGet = await _diffRepository.GetById(1);
            Assert.Null(resultGet);
        }

        [TestMethod]
        public async Task Step2_GetById_CheckNotFound()
        {
            var resultGet = await _diffRepository.GetAll();
            Assert.Collection(resultGet);
        }

    }
}