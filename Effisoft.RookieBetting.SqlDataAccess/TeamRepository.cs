using System.Collections.Generic;
using System.Linq;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Common.ViewModel;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        private readonly IDivisionRepository _divisionRepository;

        public TeamRepository(IDatabaseFactory databaseFactory,
            IDivisionRepository divisionRepository) : base(databaseFactory)
        {
            _divisionRepository = divisionRepository;
        }

        public List<Team> GetTeams()
        {
            var teams = DatabaseContext.ExecuteProcedure<List<Team>>("GetTeams");
            var divisions = _divisionRepository.GetDivisions();
            teams.ForEach(t => t.Division = divisions.FirstOrDefault(d => d.DivisionId == t.DivisionId));
            return teams;
        }

        public List<Team> GetTeamsByConference(int conferenceId)
        {
            var teams = DatabaseContext.ExecuteProcedure<List<Team>>("GetTeamsByConferenceId", new { ConferenceId = conferenceId });
            var divisions = _divisionRepository.GetDivisionsByConference(conferenceId);
            teams.ForEach(t => t.Division = divisions.FirstOrDefault(d => d.DivisionId == t.DivisionId));
            return teams;
        }

        public List<Team> GetTeamsByConference(string conferenceName)
        {
            var teams = DatabaseContext.ExecuteProcedure<List<Team>>("GetTeamsByConferenceName", new { ConferenceName = conferenceName });
            var divisions = _divisionRepository.GetDivisionsByConference(conferenceName);
            teams.ForEach(t => t.Division = divisions.FirstOrDefault(d => d.DivisionId == t.DivisionId));
            return teams;
        }

        public List<Team> GetTeamsByDivision(int divisionId)
        {
            var teams = DatabaseContext.ExecuteProcedure<List<Team>>("GetTeamsByDivisionId", new { DivisionId = divisionId });
            var division = _divisionRepository.GetDivisionById(divisionId);
            teams.ForEach(t => t.Division = division);
            return teams;
        }

        public List<Team> GetTeamsByDivision(string divisionName)
        {
            var teams = DatabaseContext.ExecuteProcedure<List<Team>>("GetTeamsByDivisionName", new { DivisionName = divisionName });
            var division = _divisionRepository.GetDivisionByName(divisionName);
            teams.ForEach(t => t.Division = division);
            return teams;
        }

        public Team GetTeam(int teamId)
        {
            var team = DatabaseContext.ExecuteProcedure<Team>("GetTeamById", new { TeamId = teamId });
            team.Division = _divisionRepository.GetDivisionById(team.DivisionId);
            return team;
        }

        public Team GetTeam(string teamName)
        {
            var team = DatabaseContext.ExecuteProcedure<Team>("GetTeamByName", new { TeamName = teamName });
            team.Division = _divisionRepository.GetDivisionById(team.DivisionId);
            return team;
        }

        public TeamStats GetTeamStats(int teamId)
        {
            return DatabaseContext.ExecuteQuery<TeamStats>("SELECT * FROM dbo.vStats WHERE TeamId = @TeamId",
                new { TeamId = teamId });
        }

        public TeamStats GetTeamStats(string teamName)
        {
            return DatabaseContext.ExecuteQuery<TeamStats>("SELECT * FROM dbo.vStats WHERE TeamName = @TeamName",
                new { TeamName = teamName });
        }
    }
}