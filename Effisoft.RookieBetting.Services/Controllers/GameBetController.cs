using System.Web.Http;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/gamebet")]
    [Authorize]
    public class GameBetController : ApiController
    {
        private readonly IGameBetRepository _gameBetRepository;

        public GameBetController(IGameBetRepository gameBetRepository)
        {
            _gameBetRepository = gameBetRepository;
        }

        [Route("addgamebet")]
        [HttpPost]
        public IHttpActionResult AddGameBet(GameBet gameBet)
        {
            _gameBetRepository.AddGameBet(gameBet);
            return Ok();
        }

        [Route("updategamebet")]
        [HttpPost]
        public IHttpActionResult UpdateGameBet(GameBet gameBet)
        {
            _gameBetRepository.UpdateGameBet(gameBet);
            return Ok();
        }
    }
}
