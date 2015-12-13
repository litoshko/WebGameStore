using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;
using WebGameStore.Controllers;
using WebGameStore.Model;

namespace WebGameStore.Tests.RoutesTests
{
    [TestFixture]
    public class CommentsRouteTests
    {
        HttpConfiguration _config;

        public CommentsRouteTests()
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
        public void GetCommentsForGame_Url_IsCorrect()
        {
            var id = "2";
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://www.example.com/games/{id}/comments");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.GetCommentsForGame(id)), routeTester.GetActionName());
        }

        [Test]
        public void AddCommentForGame_Url_IsCorrect()
        {
            var id = "2";
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://www.example.com/games/{id}/newcomment");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.AddCommentForGame(id, new Comment())), routeTester.GetActionName());
        }

        [Test]
        public void AddCommentForComment_Url_IsCorrect()
        {
            var id = 2;
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://www.example.com/comment/{id}/newcomment");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.AddCommentForComment(id, new Comment())), routeTester.GetActionName());
        }

        [Test]
        public void GetComment_Url_IsCorrect()
        {
            var id = 2;
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://www.example.com/comment/{id}");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.GetComment(id)), routeTester.GetActionName());
        }
    }
}
