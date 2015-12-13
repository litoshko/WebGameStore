using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;
using WebGameStore.Controllers;
using WebGameStore.Model;

namespace WebGameStore.Tests.RoutesTests
{
    [TestFixture]
    public class GamesRouteTests
    {
        HttpConfiguration _config;

        public GamesRouteTests()
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
        public void GetGames_Url_IsCorrect()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/games");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGames()), routeTester.GetActionName());
        }

        [Test]
        public void GetGame_Url_IsCorrect()
        {
            var id = "2";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/games/" + id);

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGame(id)), routeTester.GetActionName());
        }

        [Test]
        public void PutGame_Url_IsCorrect()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www.example.com/games/update");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.PutGame(new Game())), routeTester.GetActionName());
        }

        [Test]
        public void PostGame_Url_IsCorrect()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www.example.com/games/new");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.PostGame(new Game())), routeTester.GetActionName());
        }

        [Test]
        public void DeleteGame_Url_IsCorrect()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www.example.com/games/remove");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.DeleteGame(new Game())), routeTester.GetActionName());
        }

        [Test]
        public void GeteGamesByGenre_Url_IsCorrect()
        {
            var id = "Strategy";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/games/byGenre/" + id);

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGamesByGenre(id)), routeTester.GetActionName());
        }


        [Test]
        public void GeteGamesByPlatform_Url_IsCorrect()
        {
            var id = "mobile";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/games/byPlatform/" + id);

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGamesByPlatform(id)), routeTester.GetActionName());
        }



        [Test]
        public void DownloadGame_Url_IsCorrect()
        {
            var id = "2";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com/games/" + id + "/download");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.DownloadGame(id)), routeTester.GetActionName());
        }
    }
}
