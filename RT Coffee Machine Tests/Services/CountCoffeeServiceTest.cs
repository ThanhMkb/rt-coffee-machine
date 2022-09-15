using RT_Coffee_Machine.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RT.Coffe.Api.Test
{
    [TestClass]
    public class CountCoffeeServiceTest
    {
        [TestMethod]
        public void IncreaseCoffeCountTest()
        {
            // Arrange
            var service = new CountCoffeeService();

            // Action
            var count = service.IncreaseCoffeCount();

            // Assert
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void ResetCountTest()
        {
            // Arrange
            var service = new CountCoffeeService();

            // Action
            service.IncreaseCoffeCount();
            service.IncreaseCoffeCount();
            service.IncreaseCoffeCount();
            service.ResetCount();
            var count = service.IncreaseCoffeCount();

            // Assert
            Assert.AreEqual(count, 1);
        }

    }
}
