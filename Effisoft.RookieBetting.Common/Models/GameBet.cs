namespace Effisoft.RookieBetting.Common.Models
{
    public class GameBet
    {
        public int GameBetId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public bool HomeWins { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
