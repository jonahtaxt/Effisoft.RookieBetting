using System;
using System.Collections.Generic;
using System.Linq;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private readonly ITeamRepository _teamRepository;
        public GameRepository(IDatabaseFactory databaseFactory, ITeamRepository teamRepository) : base(databaseFactory)
        {
            _teamRepository = teamRepository;
        }

        public List<Game> GetGames()
        {
            var games = DatabaseContext.ExecuteProcedure<List<Game>>("GetGames");
            var teams = _teamRepository.GetTeams();
            games.ForEach(g=> g.HomeTeam = teams.FirstOrDefault(t=>t.TeamId==g.HomeTeamId));
            games.ForEach(g => g.AwayTeam = teams.FirstOrDefault(t => t.TeamId == g.AwayTeamId));
            return games;
        }

        public Game GetGame(DateTime gameDate, string homeTeam, string awayTeam)
        {
            var teams = _teamRepository.GetTeams();
            var game = DatabaseContext.ExecuteProcedure<Game>("GetGame", new
            {
                GameDate = gameDate,
                HomeTeamCode = homeTeam,
                AwayTeamCode = awayTeam
            });
            game.HomeTeam = teams.FirstOrDefault(t => t.TeamId == game.HomeTeamId);
            game.AwayTeam = teams.FirstOrDefault(t => t.TeamId == game.AwayTeamId);
            return game;
        }

        public void AddGame(Game game)
        {
            DatabaseContext.ExecuteProcedure("AddGame", new
            {
                game.GameDate,
                game.HomeTeamId,
                game.AwayTeamId,
                game.Season,
                game.MondayNight
            });
        }

        public void UpdateGame(Game game)
        {
            DatabaseContext.ExecuteProcedure("UpdateGame", new
            {
                game.GameId,
                game.GameDate,
                game.Week,
                game.HomeTeamId,
                game.HomeScore,
                game.AwayTeamId,
                game.AwayScore,
                game.Season,
                game.MondayNight
            });
        }
    }
}
