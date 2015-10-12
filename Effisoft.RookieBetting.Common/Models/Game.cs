using System;

namespace Effisoft.RookieBetting.Common.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int AwayScore { get; set; }
        public int Season { get; set; }
        public bool MondayNight { get; set; }
    }
}
