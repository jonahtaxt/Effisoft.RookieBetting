using Effisoft.RookieBetting.Infrastructure.Database;

namespace Effisoft.RookieBetting.SqlDataAccess
{
    public abstract class BaseRepository
    {
        protected IDatabase DatabaseContext { get; set; }

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseContext = databaseFactory.Create();
        }
    }
}
