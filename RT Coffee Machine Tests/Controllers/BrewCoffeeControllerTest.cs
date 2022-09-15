using RT_Coffee_Machine.Controllers;
using RT_Coffee_Machine.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT_Coffee_Machine.Client;
using RT.Coffe.Api.Test.Utils;
using RT_Coffee_Machine.Models;

namespace RT.Coffe.Api.Test
{
    [TestClass]
    public class BrewCoffeeControllerTest
    {
        [TestInitialize]
        public void Init()
        {
        }
        private BrewCoffeeController InitController(string temperature)
        {
            var service = new CountCoffeeService();
            ILogger<BrewCoffeeController> logger = Mock.Of<ILogger<BrewCoffeeController>>();
            var client = OpenWeatherHttpClientMock.FakeOWHttpClient(temperature);
            return new BrewCoffeeController(logger, service, client);
        }

        [TestMethod]
        public async Task GetBrewCoffe_Success()
        {
            // Arrange
            var controller = InitController("29.99");
            // Action
            var response = await controller.GetBrewCoffeAsync();

            // Assert
            var result = response as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task GetBrewCoffe_Success_GreaterTemperature()
        {
            // Arrange
            var controller = InitController("30.01");
            // Action
            var response = await controller.GetBrewCoffeAsync();

            // Assert
            var result = response as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var resultResponse = result.Value as GetBrewCoffeResponse;
            Assert.IsNotNull(resultResponse);
            Assert.AreEqual("Your refreshing iced coffee is ready", resultResponse.Message);
        }
        
        [TestMethod]
        public async Task GetBrewCoffe_Fail_503()
        {
            // Arrange
            var controller = InitController("29.99");
            // Action
            await controller.GetBrewCoffeAsync();
            await controller.GetBrewCoffeAsync();
            await controller.GetBrewCoffeAsync();
            await controller.GetBrewCoffeAsync();
            var response = await controller.GetBrewCoffeAsync();

            // Assert
            var result = response as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Value);
            Assert.AreEqual(503, result.StatusCode);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception),
                "Cannot get temperature of city!")]
        public async Task GetBrewCoffe_Fail_Cannot_Get_Temperature()
        {
            // Arrange
            var controller = InitController("x");
            // Action
            await controller.GetBrewCoffeAsync();
        }

        [TestMethod]
        [Ignore("Ignore to test this case due to solution of implement how to fake current datetime")]
        /// Ignore to test this case due to solution of implement how to fake current datetime
        /// Solution 1: Using Interface instead of using DateTime.Now directly
        /// Solution 2: Using library that can help on mock datetime to use instead of datetime from system
        /// or can help to replace method or attribute by custom delegate  
        public async Task GetBrewCoffe_Fail_418()
        {
            // Arrange
            //need to setup datetime now to April 1st
            //var controller = InitController("29.99");

            //// Action
            //await controller.GetBrewCoffeAsync();
            //var response = await controller.GetBrewCoffeAsync();

            //// Assert
            //var result = response as ObjectResult;
            //Assert.IsNotNull(result);
            //Assert.AreEqual("", result.Value);
            //Assert.AreEqual(418, result.StatusCode);
        }
    }
}
