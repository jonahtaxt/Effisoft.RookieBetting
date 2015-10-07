using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/division")]
    public class DivisionController : ApiController
    {
        private readonly IDivisionRepository _divisionRepository;

        public DivisionController(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        [Route("")]
        public IEnumerable<Division> Get()
        {
            var divisions = _divisionRepository.GetDivisions();
            return divisions;
        }

        [Route("ByConference")]
        public IEnumerable<Division> GetDivisionsByConferenceId(int conferenceId)
        {
            var divisions = _divisionRepository.GetDivisionsByConference(conferenceId);
            return divisions;
        }

        [Route("ByConference")]
        public IEnumerable<Division> GetDivisionByConferenceName(string conferenceName)
        {
            var divisions = _divisionRepository.GetDivisionsByConference(conferenceName);
            return divisions;
        }
    }
}
