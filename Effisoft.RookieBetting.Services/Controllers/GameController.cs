using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Common.ViewModel;
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

        [Route("weeks")]
        public IEnumerable<SeasonWeek> GetGameWeeks(int season)
        {
            return _gameRepository.GetGameWeeks(season);
        }

        [Route("seasons")]
        public IEnumerable<SeasonWeek> GetSeasons()
        {
            return _gameRepository.GetAvailableSeasons();
        }

        [Route("gameresults")]
        public IEnumerable<GameResult> GetGameResults(int week)
        {
            var result = _gameRepository.GetGameResultsByWeek(week);
            result.ForEach(gr => gr.AwayTeamLogoUrl = ConfigurationManager.AppSettings["NflTeamLogosContainerUrl"] + gr.AwayTeamLogoUrl);
            result.ForEach(gr => gr.HomeTeamLogoUrl = ConfigurationManager.AppSettings["NflTeamLogosContainerUrl"] + gr.HomeTeamLogoUrl);
            return result;
        }

        [Route("add")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void AddGame(Game game)
        {
            _gameRepository.AddGame(game);
        }

        [Route("update")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void UpdateGame(Game game)
        {
            _gameRepository.UpdateGame(game);
        }

        [Route("delete")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void Delete(int gameId)
        {
            _gameRepository.DeleteGame(gameId);
        }
    }
}
