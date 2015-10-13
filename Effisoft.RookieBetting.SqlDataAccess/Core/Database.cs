using Effisoft.RookieBetting.Infrastructure.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Effisoft.RookieBetting.SqlDataAccess.Core
{
    public class Database<TConnection> : IDatabase
        where TConnection : IDbConnection, new()
    {
        private static readonly Dictionary<Type, PropertyInfo[]> _typePropertyInfoCache =
            new Dictionary<Type, PropertyInfo[]>();

        private static readonly object _typePropertyCacheSyncRoot = new object();

        public Database(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public void ExecuteQuery(string text, object args)
        {
            var parameters =
                GetParametersAsDictionary(args);

            ExecuteCommand(text, CommandType.Text, parameters);
        }

        public TResult ExecuteQuery<TResult>(string text, object args)
        {
            var parameters =
                GetParametersAsDictionary(args);

            var result = ExecuteCommand<TResult>(text, CommandType.Text, parameters);
            return result;
        }

        public TResult ExecuteProcedure<TResult>(string text)
        {
            var result = ExecuteCommand<TResult>(text, CommandType.StoredProcedure, null);
            return result;
        }

        public void ExecuteProcedure(string text)
        {
            ExecuteCommand(text, CommandType.StoredProcedure, null);
        }

        public void ExecuteProcedure(string text, Dictionary<string, object> parameters)
        {
            ExecuteCommand(text, CommandType.StoredProcedure, parameters);
        }

        public TResult ExecuteProcedure<TResult>(string text, object parameters, bool isDeepEntity = false)
        {
            Dictionary<string, object> parametersAsDictionary = null;
            if (parameters != null)
                parametersAsDictionary = GetParametersAsDictionary(parameters);

            var result = ExecuteProcedure<TResult>(text, parametersAsDictionary, isDeepEntity);

            return result;
        }

        public void ExecuteProcedure(string text, object parameters)
        {
            Dictionary<string, object> parametersAsDictionary = null;
            if (parameters != null)
            {
                parametersAsDictionary = GetParametersAsDictionary(parameters);
            }

            ExecuteProcedure(text, parametersAsDictionary);
        }

        public void ExecuteCommand(string text, CommandType commandType, Dictionary<string, object> parameters)
        {
            ExecuteCommandInternal(text, commandType, parameters, command => command.ExecuteNonQuery());
        }

        public TResult ExecuteCommand<TResult>(string text, CommandType commandType,
            Dictionary<string, object> parameters)
        {
            return ExecuteCommand<TResult>(text, commandType, parameters, false);
        }

        public TResult ExecuteProcedure<TResult>(string text, Dictionary<string, object> parameters)
        {
            return ExecuteProcedure<TResult>(text, parameters);
        }

        public TResult ExecuteProcedure<TResult>(string text, Dictionary<string, object> parameters,
            bool isDeepEntity = false)
        {
            var result = ExecuteCommand<TResult>(text, CommandType.StoredProcedure, parameters, isDeepEntity);
            return result;
        }

        public TResult ExecuteCommand<TResult>(string text, CommandType commandType,
            Dictionary<string, object> parameters, bool idDeepEntity = false)
        {
            var results = default(TResult);

            ExecuteCommandInternal(
                text, commandType, parameters,
                command => { results = ReadCommandResults<TResult>(command, idDeepEntity); });

            return results;
        }

        protected virtual void PrepareCommand(IDbCommand command)
        {
        }

        protected virtual void ExecuteCommandInternal(IDbCommand command, Action<IDbCommand> executeCommand)
        {
            executeCommand(command);
        }

        protected virtual object ReadValueFromDbReader(IDataReader reader, int ordinal)
        {
            var value = reader.GetValue(ordinal);
            return value;
        }

        private void ExecuteCommandInternal(string text, CommandType commandType, Dictionary<string, object> parameters,
            Action<IDbCommand> executeCommand)
        {
            using (var connection = NewConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = commandType;
                    command.CommandText = text;

                    if (parameters != null)
                    {
                        AddCommandParameters(command, parameters);
                    }

                    PrepareCommand(command);

                    connection.Open();

                    ExecuteCommandInternal(command, executeCommand);

                    UpdateParameterValues(command, parameters);
                }
            }
        }

        private TResult ReadCommandResults<TResult>(IDbCommand command, bool isDeepEntity = false)
        {
            using (var reader = command.ExecuteReader())
            {
                var isList = IsList(typeof(TResult));
                if (isList)
                {
                    var results = ReadResultsIntoList<TResult>(reader);
                    return results;
                }
                var entity = ReadResultIntoType<TResult>(reader, isDeepEntity);
                return entity;
            }
        }

        private TResult ReadResultsIntoList<TResult>(IDataReader reader)
        {
            var listElementType = GetListElementType(typeof(TResult));
            var resultList = (IList)Activator.CreateInstance(typeof(TResult));

            while (reader.Read())
            {
                var entity = ReadResultAsType(listElementType, reader);
                resultList.Add(entity);
            }

            return (TResult)resultList;
        }

        private Dictionary<string, object> GetParametersAsDictionary(object parameters)
        {
            var parametersAsDictionary = new Dictionary<string, object>();

            if (parameters != null)
            {
                if (parameters.GetType().IsArray)
                {
                    var index = 1;

                    Array.ForEach((object[])parameters, o => parametersAsDictionary.Add("@p" + (index++), o));
                }
                else
                {
                    foreach (var pInfo in GetPropertiesFromType(parameters.GetType()))
                    {
                        var value = pInfo.GetValue(parameters, null);
                        parametersAsDictionary.Add(pInfo.Name, value);
                    }
                }
            }

            return parametersAsDictionary;
        }

        private void UpdateParameterValues(IDbCommand command, Dictionary<string, object> parameters)
        {
            if (command.CommandType == CommandType.StoredProcedure)
            {
                foreach (IDataParameter parameter in command.Parameters)
                {
                    if (parameters.ContainsKey(parameter.ParameterName) ||
                        parameters.ContainsKey("output" + parameter.ParameterName))
                    {
                        parameters[parameter.ParameterName] = parameter.Value;
                    }
                }
            }
        }

        private static Dictionary<string, object> CreateParametersForCommand(object[] args)
        {
            var parameters = new Dictionary<string, object>();
            for (var i = 0; i < args.Length; i++)
            {
                parameters.Add("@p" + (i + 1), args[i]);
            }
            return parameters;
        }

        private static Type GetListElementType(Type type)
        {
            if (typeof(IList).IsAssignableFrom(type) && type.IsGenericType)
            {
                return type.GetGenericArguments()[0];
            }

            throw new ArgumentException("El tipo de resultado no es válido.");
        }

        private bool IsList(Type type)
        {
            var isList = !IsPrimitive(type) && typeof(IEnumerable).IsAssignableFrom(type) && !type.IsArray;
            return isList;
        }

        private void AddCommandParameters(IDbCommand command, Dictionary<string, object> parameters)
        {
            foreach (var parameterName in parameters.Keys)
            {
                var parameter = command.CreateParameter();

                var procedureParameterName = parameterName;

                if (parameterName.StartsWith("output"))
                {
                    parameter.Direction = ParameterDirection.Output;
                    procedureParameterName = procedureParameterName.Replace("output", "");
                }

                parameter.ParameterName = procedureParameterName;
                parameter.Value = parameters[parameterName];

                command.Parameters.Add(parameter);
            }
        }

        private TResult ReadResultIntoType<TResult>(IDataReader reader, bool isDeepEntity = false)
        {
            if (reader.Read())
            {
                var entity = ReadResultAsType(typeof(TResult), reader, isDeepEntity);
                return (TResult)Convert.ChangeType(entity, typeof(TResult));
            }

            return default(TResult);
        }

        private object ReadResultAsType(Type entityType, IDataReader reader, bool isDeepEntity = false)
        {
            if (IsPrimitive(entityType) || IsArrayOfPrimivite(entityType))
            {
                var primitiveResult = ReadValueFromDbReader(reader, 0);
                var dotNetTypeValue = primitiveResult is DBNull ? null : primitiveResult;
                return dotNetTypeValue;
            }
            if (entityType == typeof(ExpandoObject))
            {
                var entityResult = ReadResultAsDynamic(reader);
                return entityResult;
            }
            else
            {
                var entityResult = isDeepEntity
                    ? ReadResultAsDeepEntity(reader, entityType)
                    : ReadResultAsEntity(reader, entityType);
                return entityResult;
            }
        }

        private static bool IsPrimitive(Type entityType)
        {
            var isPrimitive = entityType.IsPrimitive || entityType.IsEnum || IsFrameworkType(entityType);
            return isPrimitive;
        }

        private static bool IsArrayOfPrimivite(Type entityType)
        {
            if (entityType.IsArray)
            {
                var isPrimitiveElement = IsPrimitive(entityType.GetElementType());
                return isPrimitiveElement;
            }
            return false;
        }

        private static bool IsFrameworkType(Type entityType)
        {
            var isFrameworkType =
                Type.GetTypeCode(entityType) == TypeCode.String || Type.GetTypeCode(entityType) == TypeCode.DateTime ||
                Type.GetTypeCode(entityType) == TypeCode.Decimal;

            return isFrameworkType;
        }

        private object ReadResultAsEntity(IDataReader reader, Type entityType)
        {
            var entity = Activator.CreateInstance(entityType);
            var properties = GetPropertiesFromType(entityType).Where(p => p.CanWrite);

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var pInfo = properties.FirstOrDefault(p => string.Compare(p.Name, name, true) == 0);

                if (pInfo != null)
                {
                    var fieldValue = ReadValueFromDbReader(reader, i);

                    var dotNetTypeValue = fieldValue is DBNull
                        ? null
                        : ConvertToValueType(fieldValue, pInfo.PropertyType);

                    pInfo.SetValue(entity, dotNetTypeValue, null);
                }
            }

            return entity;
        }

        private object ReadResultAsDeepEntity(IDataReader reader, Type entityType)
        {
            var properties = GetDeepPropertiesFromType(entityType);

            var foundObjects = new Dictionary<Type, object>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);

                PropertyInfo pinfo = null;

                var fullType = properties.FirstOrDefault(p =>
                {
                    pinfo = p.First(name);
                    return pinfo != null;
                });

                if (fullType != null)
                {
                    object temp = null;

                    if (!foundObjects.ContainsKey(fullType.EntityType))
                        temp = Activator.CreateInstance(fullType.EntityType);
                    else
                        temp = foundObjects[fullType.EntityType];

                    if (pinfo != null)
                    {
                        var fieldValue = ReadValueFromDbReader(reader, i);

                        var dotNetTypeValue = fieldValue is DBNull
                            ? null
                            : ConvertToValueType(fieldValue, pinfo.PropertyType);

                        pinfo.SetValue(temp, dotNetTypeValue, null);
                    }

                    if (!foundObjects.ContainsKey(fullType.EntityType))
                        foundObjects.Add(fullType.EntityType, temp);
                }
            }

            var entity = MapDeepObjects(entityType, foundObjects);
            return entity;
        }

        private object ConvertToValueType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Convert.ChangeType(value, type.GetGenericArguments()[0]);

            return Convert.ChangeType(value, type);
        }

        private dynamic ReadResultAsDynamic(IDataReader reader)
        {
            dynamic dynamicEntity = new ExpandoObject();

            for (var i = 0; i < reader.FieldCount; i++)
                ((IDictionary<string, object>)dynamicEntity).Add(reader.GetName(i), reader.GetValue(i));

            return dynamicEntity;
        }

        private IDbConnection NewConnection()
        {
            var connection = new TConnection();
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        private static PropertyInfo[] GetPropertiesFromType(Type type)
        {
            PropertyInfo[] propertiesInType;
            if (!_typePropertyInfoCache.TryGetValue(type, out propertiesInType))
            {
                lock (_typePropertyCacheSyncRoot)
                {
                    if (!_typePropertyInfoCache.TryGetValue(type, out propertiesInType))
                    {
                        propertiesInType = type.GetProperties();
                        _typePropertyInfoCache.Add(type, propertiesInType);
                    }
                }
            }
            return propertiesInType;
        }

        private static List<TypeProperties> GetDeepPropertiesFromType(Type type)
        {
            var resultList = new List<TypeProperties>();
            var propertyList = new List<PropertyInfo>();

            foreach (var prop in type.GetProperties())
            {
                if (!IsPrimitive(prop.PropertyType))
                {
                    var properties = GetDeepPropertiesFromType(prop.PropertyType);

                    resultList.AddRange(properties);
                }
                else
                {
                    propertyList.Add(prop);
                }
            }

            resultList.Add(new TypeProperties
            {
                EntityType = type,
                Properties = propertyList.ToArray()
            });

            return resultList;
        }

        private static object MapDeepObjects(Type type, Dictionary<Type, object> foundObjects)
        {
            if (foundObjects.ContainsKey(type))
            {
                var currentInstance = foundObjects[type];

                foreach (var prop in type.GetProperties())
                {
                    if (!IsPrimitive(prop.PropertyType))
                    {
                        var value = MapDeepObjects(prop.PropertyType, foundObjects);
                        prop.SetValue(currentInstance, value);
                    }
                }

                return currentInstance;
            }

            return null;
        }
    }

    internal class TypeProperties
    {
        public Type EntityType { get; set; }
        public PropertyInfo[] Properties { get; set; }

        public PropertyInfo First(string propertyName)
        {
            if (Properties != null)
                return Properties.FirstOrDefault(p => string.Compare(p.Name, propertyName, true) == 0);

            return null;
        }
    }
}
