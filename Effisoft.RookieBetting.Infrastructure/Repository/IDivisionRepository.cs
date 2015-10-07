using Effisoft.RookieBetting.Common.Models;
using System.Collections.Generic;

namespace Effisoft.RookieBetting.Infrastructure.Repository
{
    public interface IDivisionRepository
    {
        List<Division> GetDivisions();
        List<Division> GetDivisionsByConference(int conferenceId);
        List<Division> GetDivisionsByConference(string conferenceName);
        Division GetDivisionByName(string divisionName);
        Division GetDivisionById(int divisionId);
    }
}
