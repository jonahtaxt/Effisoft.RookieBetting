using System.Collections.Generic;
using System.Web.Http;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Route("")]
        public IEnumerable<Game> GetGames()
        {
            return _gameRepository.GetGames();
        }

        [Route("")]
        [HttpPost]
        public void AddGame(Game game)
        {
            _gameRepository.AddGame(game);
        }
    }
}
