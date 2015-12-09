using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using WebApi.OutputCache.V2;
using WebGameStore.BL;
using WebGameStore.DAL;
using WebGameStore.Filters;
using WebGameStore.Model;

namespace WebGameStore.Controllers
{
    public class CommentsController : ApiController
    {
        ////private StoreDbContext db = new StoreDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ICommentService _commentService;
        IGameService _gameService;

        public CommentsController(ICommentService commentService, IGameService gameService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        

        // GET: games/1/comments
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/{id}/comments")]
        [HttpGet]
        public IEnumerable<Comment> GetCommentsForGame(string id)
        {
            Log.Debug("GET all comments for game Request traced");
            var comments = _commentService.GetCommentsForGame(id);
            return comments;
        }

        // GET: games/1/newcomment
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/{id}/newcomment")]
        [HttpPost]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult AddCommentForGame(string id, [FromBody]Comment comment)
        {
            Log.Debug("POST add comment for game request traced");
            if (!ModelState.IsValid)
            {
                Log.Error("POST add comment for game: Model is not valid");
                return BadRequest(ModelState);
            }

            Game game = _gameService.GetById(id);
            if (game == null)
            {
                Log.Error("POST add comment for game: game not found");
                return NotFound();
            }

            comment.GameKey = id;

            _commentService.Create(comment);

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // GET: comment/1/newcomment
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("comment/{id}/newcomment")]
        [HttpPost]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult AddCommentForComment(int id, [FromBody]Comment comment)
        {
            Log.Debug("POST add comment for comment request traced");
            if (!ModelState.IsValid)
            {
                Log.Error("POST add comment for comment: Model is not valid");
                return BadRequest(ModelState);
            }

            Comment baseComment = _commentService.GetById(id);
            if (baseComment == null)
            {
                Log.Error("POST add comment for comment: comment not found");
                return NotFound();
            }

            comment.ParentCommentId = id;

            _commentService.Create(comment);

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }
        
        // GET: api/Comments/5
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Log.Debug("GET comment by id request traced");
            Comment comment = _commentService.GetById(id);
            if (comment == null)
            {
                Log.Error("GET comment: comment not found");
                return NotFound();
            }

            return Ok(comment);
        }
    }
}