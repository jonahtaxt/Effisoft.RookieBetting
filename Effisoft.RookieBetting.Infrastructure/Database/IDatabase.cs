using System.Collections.Generic;
using System.Data;

namespace Effisoft.RookieBetting.Infrastructure.Database
{
    public interface IDatabase
    {
        void ExecuteCommand(string text, CommandType commandType, Dictionary<string, object> parameters);

        TResult ExecuteCommand<TResult>(string text, CommandType commandType, Dictionary<string, object> parameters);

        void ExecuteProcedure(string text);

        void ExecuteProcedure(string text, Dictionary<string, object> parameters);

        void ExecuteProcedure(string text, object parameters);

        TResult ExecuteProcedure<TResult>(string text);

        TResult ExecuteProcedure<TResult>(string text, Dictionary<string, object> parameters);

        TResult ExecuteProcedure<TResult>(string text, object parameters, bool isDeepEntity = false);

        void ExecuteQuery(string text, object args);

        TResult ExecuteQuery<TResult>(string text, object args);
    }
}
