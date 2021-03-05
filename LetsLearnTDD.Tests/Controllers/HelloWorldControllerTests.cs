using NUnit.Framework;
using LetsLearnTDD.Controllers;
using LetsLearnTDD.Models;

namespace LetsLearnTDD.Tests.Controllers
{
    public class Tests
    {
        [Test]
        public void TestGet()
        {
            var controller = new HelloWorldController();
            var response = controller.Get();
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void TestDefaultNameGet()
        {
            var controller = new HelloWorldController();
            var response = controller.Get().Value as HelloWorld;
            Assert.AreEqual("Unicorn", response.Name);
        }
    }
}