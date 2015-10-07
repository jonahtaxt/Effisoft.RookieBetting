using Effisoft.RookieBetting.Common.Models;
using System.Collections.Generic;

namespace Effisoft.RookieBetting.Infrastructure.Repository
{
    public interface IConferenceRepository
    {
        List<Conference> GetConferences();
        Conference GetConference(int conferenceId);
    }
}
