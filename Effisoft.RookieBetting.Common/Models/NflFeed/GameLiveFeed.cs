namespace Effisoft.RookieBetting.Common.Models.NflFeed
{
    public class GameLiveFeed
    {
        public string GameId { get; set; }
        public TeamScore HomeScore { get; set; }
        public TeamScore AwayScore { get; set; }
    }
}
