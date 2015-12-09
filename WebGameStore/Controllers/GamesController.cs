using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using WebGameStore.BL;
using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.Controllers
{
    public class GamesController : ApiController
    {
        IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: games
        [Route("games")]
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _gameService.GetAll();
        }

        // GET: games/5
        [Route("games/{id}", Name = "GetGame")]
        [HttpGet]
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(string id)
        {
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // POST: games/update
        [Route("games/update")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGame([FromBody]Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _gameService.Update(game);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: games/new
        [Route("games/new")]
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult PostGame([FromBody]Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _gameService.Create(game);

            return CreatedAtRoute("GetGame", new { id = game.Key }, game);
        }

        // POST: games/remove/5
        [Route("games/remove/{id}")]
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult DeleteGame(string id)
        {
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            _gameService.Delete(game);

            return Ok(game);
        }

        // GET: games/byGenre/Action
        [Route("games/bygenre/{name}")]
        [HttpGet]
        public IEnumerable<Game> GetGamesByGenre(string name)
        {
            return _gameService.GetByGenre(name);
        }

        // GET: games/byPlatform/Action
        [Route("games/byPlatform/{name}")]
        [HttpGet]
        public IEnumerable<Game> GetGamesByPlatform(string name)
        {
            return _gameService.GetByPlatform(name);
        }

        // GET: games/byPlatform/Action
        [Route("games/{id}/download")]
        [HttpGet]
        public HttpResponseMessage DownloadGame(string id)
        {
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid ID");
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(JsonConvert.SerializeObject(game));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"game_{id}.json"
            };
            result.StatusCode = HttpStatusCode.OK;

            return result;
        }
    }
}