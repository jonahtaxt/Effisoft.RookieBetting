using Effisoft.RookieBetting.Infrastructure.Repository;
using System.Collections.Generic;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Database;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public class ConferenceRepository : BaseRepository, IConferenceRepository
    {
        public ConferenceRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public List<Conference> GetConferences()
        {
            return DatabaseContext.ExecuteProcedure<List<Conference>>("GetConferences");
        }

        public Conference GetConference(int conferenceId)
        {
            return DatabaseContext.ExecuteProcedure<Conference>("GetConferenceById", new { ConferenceId = conferenceId });
        }
    }
}
