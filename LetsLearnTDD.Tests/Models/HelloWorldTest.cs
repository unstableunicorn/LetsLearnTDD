using NUnit.Framework;
using LetsLearnTDD.Models;

namespace LetsLearnTDD.Tests.Models
{
    public class Tests
    {
        [Test]
        public void HelloWorldWithGivenName()
        {
            const string helloWorldName = "Unicorn";
            var helloWorldModel = new HelloWorld() { Name = helloWorldName };
            Assert.AreEqual(helloWorldName, helloWorldModel.Name);
        }
    }
}