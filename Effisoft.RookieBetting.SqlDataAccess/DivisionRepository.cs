using Effisoft.RookieBetting.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Effisoft.RookieBetting.Common.Models;
using Effisoft.RookieBetting.Infrastructure.Database;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public class DivisionRepository : BaseRepository, IDivisionRepository
    {
        private readonly IConferenceRepository _conferenceRepository;
        public DivisionRepository(IDatabaseFactory databaseFactory, IConferenceRepository conferenceRespository) : base(databaseFactory)
        {
            _conferenceRepository = conferenceRespository;
        }

        public Division GetDivisionById(int divisionId)
        {
            var division = DatabaseContext.ExecuteProcedure<Division>("GetDivisionById",
                new
                {
                    DivisionId = divisionId
                });
            var conferences = _conferenceRepository.GetConferences();
            division.Conference = conferences.FirstOrDefault(c => c.ConferenceId == division.ConferenceId);
            return division;
        }

        public Division GetDivisionByName(string divisionName)
        {
            var division = DatabaseContext.ExecuteProcedure<Division>("GetDivisionByName",
                new
                {
                    DivisionName = divisionName
                });
            var conferences = _conferenceRepository.GetConferences();
            division.Conference = conferences.FirstOrDefault(c => c.ConferenceId == division.ConferenceId);
            return division;
        }

        public List<Division> GetDivisions()
        {
            var divisions = DatabaseContext.ExecuteProcedure<List<Division>>("GetDivisions");
            var conferences = _conferenceRepository.GetConferences();
            divisions.ForEach(d => d.Conference = conferences.FirstOrDefault(c => c.ConferenceId == d.ConferenceId));
            return divisions;
        }

        public List<Division> GetDivisionsByConference(string conferenceName)
        {
            var divisions = DatabaseContext.ExecuteProcedure<List<Division>>("GetDivisionsByConferenceName", new { ConferenceName = conferenceName });
            var conferences = _conferenceRepository.GetConferences();
            divisions.ForEach(d => d.Conference = conferences.FirstOrDefault(c => c.ConferenceId == d.ConferenceId));
            return divisions;
        }

        public List<Division> GetDivisionsByConference(int conferenceId)
        {
            var divisions = DatabaseContext.ExecuteProcedure<List<Division>>("GetDivisionsByConferenceId", new { ConferenceId = conferenceId });
            var conferences = _conferenceRepository.GetConferences();
            divisions.ForEach(d => d.Conference = conferences.FirstOrDefault(c => c.ConferenceId == d.ConferenceId));
            return divisions;
        }
    }
}
