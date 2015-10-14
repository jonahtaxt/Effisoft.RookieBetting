using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Effisoft.RookieBetting.Common.ViewModel
{
    public class GameResult
    {
        public string GameDate { get; set; }
        public string AwayTeamLogoUrl { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayScore { get; set; }
        public string HomeTeamLogoUrl { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeScore { get; set; }
    }
}
