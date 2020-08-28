using Microsoft.Owin.Security.OAuth;
using Seabattle.API.Models;
using Seabattle.Logic.Models;
using Seabattle.Logic.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seabattle.API.Controllers
{
    [RoutePrefix("api/games")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        [HttpPost, Route("register")]
        [Authorize]
        public IHttpActionResult Register([FromBody]CreateGameRequest request)
        {
            var r = User;
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = _gameService.CreateGame(request.Ships, request.ConnectionId);

            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPost, Route("shot")]
        [Authorize]
        public IHttpActionResult Shot([FromBody]ShotDto shot)
        {
            var result = _gameService.MakeShot(shot);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);

        }
    }
}
