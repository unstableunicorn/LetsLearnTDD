using NUnit.Framework;
using LetsLearnTDD.Controllers; 

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
    }
}