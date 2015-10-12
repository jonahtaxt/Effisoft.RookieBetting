using System.Collections.Generic;
using System.Web.Http;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Common.ViewModel;
using Effisoft.RookieBetting.Infrastructure.Repository;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/team")]
    public class TeamController : ApiController
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teampRepository)
        {
            _teamRepository = teampRepository;
        }

        [Route("")]
        public IEnumerable<Team> GetTeams()
        {
            return _teamRepository.GetTeams();
        }

        [Route("ByConference")]
        public IEnumerable<Team> GetTeamsByConference(int conferenceId)
        {
            return _teamRepository.GetTeamsByConference(conferenceId);
        }

        [Route("ByConference")]
        public IEnumerable<Team> GetTeamsByConference(string conferenceName)
        {
            return _teamRepository.GetTeamsByConference(conferenceName);
        }

        [Route("ByDivision")]
        public IEnumerable<Team> GetTeamsByDivision(int divisionId)
        {
            return _teamRepository.GetTeamsByDivision(divisionId);
        }

        [Route("ByDivision")]
        public IEnumerable<Team> GetTeamsByDivision(string divisionName)
        {
            return _teamRepository.GetTeamsByDivision(divisionName);
        }

        [Route("")]
        public Team GetTeam(int teamId)
        {
            return _teamRepository.GetTeam(teamId);
        }

        [Route("")]
        public Team GetTeam(string teamName)
        {
            return _teamRepository.GetTeam(teamName);
        }

        [Route("Stats")]
        public TeamStats GetTeamStats(int teamId)
        {
            return _teamRepository.GetTeamStats(teamId);
        }

        [Route("Stats")]
        public TeamStats GetTeamStats(string teamName)
        {
            return _teamRepository.GetTeamStats(teamName);
        }
    }
}
