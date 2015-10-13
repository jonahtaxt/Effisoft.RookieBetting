using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public class GameBetRepository : BaseRepository, IGameBetRepository
    {
        public GameBetRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public void AddGameBet(GameBet gameBet)
        {
            DatabaseContext.ExecuteProcedure("AddGameBet",new
            {
                gameBet.UserId,
                gameBet.GameId,
                gameBet.HomeWins,
                gameBet.HomeScore,
                gameBet.AwayScore
            });
        }

        public void UpdateGameBet(GameBet gameBet)
        {
            DatabaseContext.ExecuteProcedure("UpdateGameBet", new
            {
                gameBet.GameBetId,
                gameBet.UserId,
                gameBet.GameId,
                gameBet.HomeWins,
                gameBet.HomeScore,
                gameBet.AwayScore
            });
        }
    }
}
