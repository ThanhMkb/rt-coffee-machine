using RT_Coffee_Machine.Controllers;
using RT_Coffee_Machine.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RT.Coffe.Api.Test
{
    [TestClass]
    public class BrewCoffeeControllerTest
    {
        private BrewCoffeeController? _controller;
        [TestInitialize]
        public void Init()
        {
            InitController();
        }
        private void InitController()
        {
            var service = new CountCoffeeService();
            ILogger<BrewCoffeeController> logger = Mock.Of<ILogger<BrewCoffeeController>>();
            _controller = new BrewCoffeeController(logger, service);
        }

        [TestMethod]
        public void GetBrewCoffe_Success()
        {
            // Action
            var response = _controller?.GetBrewCoffe();

            // Assert
            var result = response as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void GetBrewCoffe_Fail_503()
        {
            // Action
            _controller?.GetBrewCoffe();
            _controller?.GetBrewCoffe();
            _controller?.GetBrewCoffe();
            _controller?.GetBrewCoffe();
            var response = _controller?.GetBrewCoffe();

            // Assert
            var result = response as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Value);
            Assert.AreEqual(503, result.StatusCode);
        }

        [TestMethod]
        [Ignore("Ignore to test this case due to solution of implement how to fake current datetime")]
        /// Ignore to test this case due to solution of implement how to fake current datetime
        /// Solution 1: Using Interface instead of using DateTime.Now directly
        /// Solution 2: Using library that can help on mock datetime to use instead of datetime from system
        /// or can help to replace method or attribute by custom delegate  
        public void GetBrewCoffe_Fail_418()
        {
            // Arrange
            //need to setup datetime now to April 1st

            // Action
            _controller?.GetBrewCoffe();
            var response = _controller?.GetBrewCoffe();

            // Assert
            var result = response as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Value);
            Assert.AreEqual(418, result.StatusCode);
        }
    }
}
