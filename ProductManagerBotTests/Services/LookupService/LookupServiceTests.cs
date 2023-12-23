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
        public void GetProductTest()
        {
            var expected = "Milka Toffee Ganznuss";

            LookupService service = new();

            var actual = service.GetProduct("7622300134532")
                                .Result?
                                .Name ?? string.Empty;


            Assert.AreEqual(expected, actual, $"Fail!: Expected is <{expected}>, but actual is <{actual}>s!");
        }
    }
}