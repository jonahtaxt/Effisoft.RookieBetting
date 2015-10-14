using System;
using System.Collections.Generic;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Common.ViewModel;

namespace Effisoft.RookieBetting.Infrastructure.Repository
{
    public interface IGameRepository
    {
        List<Game> GetGames();
        Game GetGame(DateTime gameDate, string homeTeam, string awayTeam);
        void AddGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(int gameId);
        List<SeasonWeek> GetGameWeeks(int season);
        List<SeasonWeek> GetAvailableSeasons();
        List<GameResult> GetGameResultsByWeek(int week);
    }
}
