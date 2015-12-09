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
using WebApi.OutputCache.V2;
using WebGameStore.BL;
using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.Controllers
{
    public class CommentsController : ApiController
    {
        ////private StoreDbContext db = new StoreDbContext();
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Game game = _gameService.GetById(id);
            if (game == null)
            {
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment baseComment = _commentService.GetById(id);
            if (baseComment == null)
            {
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
            Comment comment = _commentService.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
    }
}