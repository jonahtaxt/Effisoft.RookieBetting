namespace Effisoft.RookieBetting.Common.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public int TeamWins { get; set; }
        public int TeamLoss { get; set; }
        public int TeamTies { get; set; }
        public string TeamLogoUrl { get; set; }
        public string TeamCode { get; set; }
        public Division Division { get; set; }
    }
}
