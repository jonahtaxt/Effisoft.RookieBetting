namespace Effisoft.RookieBetting.Common.Models
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}