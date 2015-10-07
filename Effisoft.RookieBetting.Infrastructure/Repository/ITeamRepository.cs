using System.Collections.Generic;
using Effisoft.RookieBetting.Common.Models;

namespace Effisoft.RookieBetting.Infrastructure.Repository
{
    public interface ITeamRepository
    {
        List<Team> GetTeams();
        List<Team> GetTeamsByConference(int conferenceId);
        List<Team> GetTeamsByConference(string conferenceName);
        List<Team> GetTeamsByDivision(int divisionId);
        List<Team> GetTeamsByDivision(string divisionName);
        Team GetTeam(int teamId);
        Team GetTeam(string teamName);
    }
}
