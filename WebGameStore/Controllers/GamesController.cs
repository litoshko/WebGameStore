using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using Newtonsoft.Json;
using WebApi.OutputCache.V2;
using WebGameStore.BL;
using WebGameStore.DAL;
using WebGameStore.Filters;
using WebGameStore.Model;

namespace WebGameStore.Controllers
{
    public class GamesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: games
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games")]
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            Log.Debug("GET all games request traced");
            return _gameService.GetAll();
        }

        // GET: games/5
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/{id}", Name = "GetGame")]
        [HttpGet]
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(string id)
        {
            Log.Debug($"GET games by id = {id} request traced");
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                Log.Error($"GET games by id = {id} game not found");
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
            Log.Debug("POST update game request traced");
            if (!ModelState.IsValid)
            {
                Log.Error("POST update model is not valid");
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
            Log.Debug("POST create game request traced");
            if (!ModelState.IsValid)
            {
                Log.Error("POST create model is not valid");
                return BadRequest(ModelState);
            }

            _gameService.Create(game);

            return CreatedAtRoute("GetGame", new { id = game.Key }, game);
        }

        // POST: games/remove/5
        [Route("games/remove")]
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult DeleteGame(Game gameFromPost)
        {
            Log.Debug("POST delete game request traced");
            Game game = _gameService.GetById(gameFromPost.Key);
            if (game == null)
            {
                Log.Error($"POST delete: game not found");
                return NotFound();
            }

            _gameService.Delete(game);

            return Ok(game);
        }

        // GET: games/byGenre/Action
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/bygenre/{name}")]
        [HttpGet]
        public IEnumerable<Game> GetGamesByGenre(string name)
        {
            Log.Debug("GET game by genre request traced");
            return _gameService.GetByGenre(name);
        }

        // GET: games/byPlatform/Action
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/byPlatform/{name}")]
        [HttpGet]
        public IEnumerable<Game> GetGamesByPlatform(string name)
        {
            Log.Debug("GET game by platform request traced");
            return _gameService.GetByPlatform(name);
        }

        // GET: games/byPlatform/Action
        [Route("games/{id}/download")]
        [HttpGet]
        public HttpResponseMessage DownloadGame(string id)
        {
            Log.Debug("Download game request traced");
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                Log.Error("Download game: game not found");
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