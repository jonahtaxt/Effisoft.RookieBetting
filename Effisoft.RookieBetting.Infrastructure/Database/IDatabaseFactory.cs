namespace Effisoft.RookieBetting.Infrastructure.Database
{
    public interface IDatabaseFactory
    {
        IDatabase Create();
    }
}
