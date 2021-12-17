using NUnit.Framework;
using WeatherSvc.Services;
using Moq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherSvc.Tests
{

    [TestFixture]
    public class WeatherSvcTest
    {
        [SetUp]
        public void Setup()
        {
            //  var handlerMock = new Mock<HttpMessageHandler>();
            //  WeatherService ab = new WeatherService(handlerMock);
        }

        [Test]
        public void Test1()
        {

            // WeatherSvc.WeatherForecastController obj = WeatherSvc.WeatherForecastController();
            Assert.Pass();
        }
    }
}