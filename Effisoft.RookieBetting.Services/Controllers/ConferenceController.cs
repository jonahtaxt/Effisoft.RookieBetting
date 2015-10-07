using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/conference")]
    public class ConferenceController : ApiController
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceController(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        [Route("")]
        public IEnumerable<Conference> Get()
        {
            var conferences = _conferenceRepository.GetConferences();
            return conferences;
        }
    }
}