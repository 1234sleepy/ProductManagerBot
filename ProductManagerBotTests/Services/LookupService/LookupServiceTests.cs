using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagerBot.Services.LookupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.LookupService.Tests
{
    [TestClass()]
    public class LookupServiceTests
    {
        [TestMethod()]
        public void GetProductNameTest()
        {
            var expected = "Milka Toffee Ganznuss";

            LookupService service = new();

            var actual = service.GetProduct("7622300134532")
                                .Result?
                                .Name ?? string.Empty;


            Assert.AreEqual(expected, actual, $"Fail!: Expected is <{expected}>, but actual is <{actual}>s!");
        }
        [TestMethod()]
        public void GetProductManufactureTest()
        {
            var expected = "Milka";

            LookupService service = new();

            var actual = service.GetProduct("7622300134532")
                                .Result?
                                .Manufacture?.Name ?? string.Empty;


            Assert.AreEqual(expected, actual, $"Fail!: Expected is <{expected}>, but actual is <{actual}>s!");
        }
        [TestMethod()]
        public void GetProductCategoryTest()
        {
            var expected = "Food, Beverages & Tobacco";

            LookupService service = new();

            var actual = service.GetProduct("7622300134532")
                                .Result?
                                .Manufacture?.Name ?? string.Empty;


            Assert.AreEqual(expected, actual, $"Fail!: Expected is <{expected}>, but actual is <{actual}>s!");
        }
        [TestMethod()]
        public void GetProductEnergyTest()
        {
            var expected = 560;

            LookupService service = new();

            var actual = service.GetProduct("7622300134532")
                                .Result?
                                .Calories ?? 0;


            Assert.AreEqual(expected, actual, $"Fail!: Expected is <{expected}>, but actual is <{actual}>s!");
        }
    }
}