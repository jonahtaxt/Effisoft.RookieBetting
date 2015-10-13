using Effisoft.RookieBetting.Common.Models;

namespace Effisoft.RookieBetting.Infrastructure.Repository
{
    public interface IGameBetRepository
    {
        void AddGameBet(GameBet gameBet);
        void UpdateGameBet(GameBet gameBet);
    }
}
