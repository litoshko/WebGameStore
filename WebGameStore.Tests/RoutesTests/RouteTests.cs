using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using WebGameStore.Controllers;

namespace WebGameStore.Tests
{
    [TestFixture]
    public class RouteTests
    {
        HttpConfiguration _config;

        public RouteTests()
        {
            _config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            WebGameStore.WebApiConfig.Register(_config);
            _config.EnsureInitialized();
        }

        //TESTS METHODS GO HERE

        [Test]
        public void UrlControllerGetIsCorrect()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.strathweb.com/games");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGames()), routeTester.GetActionName());
        }
    }
}
