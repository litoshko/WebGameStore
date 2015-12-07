﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.Controllers
{
    public class CommentsController : ApiController
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: api/Comments
        [Route("games/{id}/comments")]
        [HttpGet]
        public IQueryable<Comment> GetCommentsForGame(string id)
        {
            IQueryable<Comment> comments = db.Comments.Where(comment => comment.GameKey == id);
            return comments;
        }

        // GET: api/Comments
        [Route("games/{id}/newcomment")]
        [HttpPost]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult AddCommentForGame(string id, [FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            comment.GameKey = id;

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // GET: api/Comments
        [Route("comment/{id}/newcomment")]
        [HttpPost]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult AddCommentForComment(int id, [FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment baseComment = db.Comments.Find(id);
            if (baseComment == null)
            {
                return NotFound();
            }

            comment.ParentCommentId = id;

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // GET: api/Comments
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}