using NUnit.Framework;
using LetsLearnTDD.Controllers;
using LetsLearnTDD.Models;

namespace LetsLearnTDD.Tests.Controllers
{
    public class HelloWorlControllerTests
    {
        HelloWorldController Controller;

        [SetUp]
        public void SetUp()
        {
            Controller = new HelloWorldController();
        }

        [Test]
        public void TestGet()
        {
            var response = Controller.Get();
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void TestDefaultNameGet()
        {
            var response = Controller.Get().Value as HelloWorld;
            Assert.AreEqual("Unicorn", response.Name);
        }
    }
}