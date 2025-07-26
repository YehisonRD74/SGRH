using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WebPrueba.Tests
{
    [TestClass]
    public class FloorService
    {
        [TestMethod]
        public void TestMethod1()
        {
         

            var result = await _service.GetFloor();

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.Message.Contains("Error"));
        }
    }
}
