using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effisoft.RookieBetting.Common.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int ScheduleId { get; set; }
        public bool MondayNight { get; set; }
    }
}
