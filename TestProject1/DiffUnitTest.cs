//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Text;
//using System.Text.Json;
//using Data.Repository.Context;
//using Domain.Model.Entities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Presentation.Server.Controllers;

//namespace TestProject1
//{
//    [TestClass]
//    public class DiffUnitTest
//    {
//        private readonly DiffController diffController;

//        public DiffUnitTest()
//        {
//            DbContextOptions<DiffContext> getDbOptions() =>
//                new DbContextOptionsBuilder<DiffContext>()
//                    .UseInMemoryDatabase(databaseName: "DiffDataBase")
//                    .Options;
//            var context = new DiffContext(getDbOptions());
//            diffController = new DiffController(context);
//        }

//        //[TestMethod]
//        //public async Task Step1_GetById_CheckNotFound()
//        //{
//        //    var resultGet = await diffController.GetById(1);
//        //    Assert.IsNotNull(resultGet);
//        //    Assert.IsInstanceOfType(resultGet.Result, typeof(NotFoundResult));
//        //    Assert.AreEqual(404, ((NotFoundResult)resultGet.Result).StatusCode);
//        //    Assert.IsNull(resultGet.Value);
//        //}

//        [TestMethod]
//        //public async Task Step2_PutLeft_CheckCreated()
//        //{
//        //    var resultPut = await diffController.PutById(1, "left", new DiffInput() { data = "AAAAAA==" });
//        //    Assert.IsNotNull(resultPut);
//        //    Assert.IsInstanceOfType(resultPut, typeof(CreatedResult));
//        //    Assert.AreEqual(201, ((CreatedResult)resultPut).StatusCode);
//        //    Assert.IsNull(((CreatedResult)resultPut).Value);

//        //}

//        //[TestMethod]
//        //public async Task Step3_GetById_CheckNotFound()
//        //{
//        //    var resultGet = await diffController.GetById(1);
//        //    Assert.IsNotNull(resultGet);
//        //    Assert.IsInstanceOfType(resultGet.Result, typeof(NotFoundResult));
//        //    Assert.AreEqual(404, ((NotFoundResult)resultGet.Result).StatusCode);
//        //    Assert.IsNull(resultGet.Value);

//        //}

//        [TestMethod]
//        //public async Task Step4_PutRight_CheckCreated()
//        //{
//        //    var resultPut = await diffController.PutById(1, "right", new DiffInput() { data = "AAAAAA==" });
//        //    Assert.IsNotNull(resultPut);
//        //    Assert.IsInstanceOfType(resultPut, typeof(CreatedResult));
//        //    Assert.AreEqual(201, ((CreatedResult)resultPut).StatusCode);
//        //    Assert.IsNull(((CreatedResult)resultPut).Value);

//        //}

//        //[TestMethod]
//        //public async Task Step5_GetById_CheckEquals()
//        //{
//        //    var resultGet = await diffController.GetById(1);
//        //    Assert.IsNotNull(resultGet);
//        //    Assert.IsInstanceOfType(resultGet.Result, typeof(OkObjectResult));
//        //    Assert.AreEqual(200, ((OkObjectResult)resultGet.Result).StatusCode);
//        //    Assert.IsNull(resultGet.Value);
//        //    Assert.IsNotNull((resultGet.Result as OkObjectResult).Value);
//        //    Assert.AreEqual(DiffResultType.Equals, ((resultGet.Result as OkObjectResult).Value as DiffResult).diffResultType);

//        //}

//        //[TestMethod]
//        //public async Task Step6_PutRight_CheckOverwriteCreated()
//        //{
//        //    var resultPut = await diffController.PutById(1, "right", new DiffInput() { data = "AQABAQ==" });
//        //    Assert.IsNotNull(resultPut);
//        //    Assert.IsInstanceOfType(resultPut, typeof(CreatedResult));
//        //    Assert.AreEqual(201, ((CreatedResult)resultPut).StatusCode);
//        //    Assert.IsNull(((CreatedResult)resultPut).Value);
//        //}

//        //[TestMethod]
//        //public async Task Step7_GetById_CheckNotMatch()
//        //{
//        //    var resultGet = await diffController.GetById(1);
//        //    Assert.IsNotNull(resultGet);
//        //    Assert.IsInstanceOfType(resultGet.Result, typeof(OkObjectResult));
//        //    Assert.AreEqual(200, ((OkObjectResult)resultGet.Result).StatusCode);
//        //    Assert.IsNull(resultGet.Value);
//        //    Assert.IsNotNull((resultGet.Result as OkObjectResult).Value);
//        //    Assert.AreEqual(DiffResultType.ContentDoNotMatch, ((resultGet.Result as OkObjectResult).Value as DiffResult).diffResultType);
//        //    var diffResult = ((resultGet.Result as OkObjectResult).Value as DiffResult);
//        //    Assert.AreEqual(2, diffResult.diffs.Count());
//        //    Assert.AreEqual(0, diffResult.diffs[0].offset);
//        //    Assert.AreEqual(1, diffResult.diffs[0].length);
//        //    Assert.AreEqual(2, diffResult.diffs[1].offset);
//        //    Assert.AreEqual(2, diffResult.diffs[1].length);
//        //}
//    //}
//}
