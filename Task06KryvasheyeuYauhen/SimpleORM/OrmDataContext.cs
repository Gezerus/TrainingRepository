using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;


namespace SimpleORM
{
    /// <summary>
    /// orm implementation with ado.net and reflection
    /// </summary>
    public class OrmDataContext : IDisposable
    {
        private SqlConnection _connection;

        private static OrmDataContext _singleDataContext = null;

        private OrmDataContext(string connectionString)
        {
            
            _connection = new SqlConnection(connectionString);
        }

        public static OrmDataContext Initialize(string connectionString)
        {
            if (_singleDataContext == null)
                _singleDataContext = new OrmDataContext(connectionString);
            return _singleDataContext;
        }

        /// <summary>
        /// Deletes a model from a database
        /// </summary>
        /// <param name="obj">the model being deleting</param>
        /// <returns>1 if deletion was successful or 0 if not</returns>
        public int Delete(object obj)
        {
            var tableAttribute = obj.GetType().GetCustomAttribute(typeof(TableAttribute));
            if (tableAttribute == null)
                throw new MappingException("The model should have TableAttribute");

            var properties = obj.GetType().GetProperties();

            var primaryKeysNames = properties.Where(p => ((p.GetCustomAttribute(typeof(ColumnAttribute)) != null)) &&
            (((ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute))).IsPrimaryKey == true)).
            Select(p => p.Name);
            if (primaryKeysNames.Count() == 0)
                throw new MappingException("Models should have at least one Primary Key");

            var sqlStringCreator = new DeleteSqlStringCreator(((TableAttribute)tableAttribute).Name, primaryKeysNames);
            var sqlString = sqlStringCreator.Create();

            return Execute(sqlString, obj);

        }

        /// <summary>
        /// Updates a model in a database
        /// </summary>
        /// <param name="obj">the model being updating</param>
        /// <returns>1 if updation was successful or 0 if not</returns>
        public int Update(object obj)
        {
            var tableAttribute = obj.GetType().GetCustomAttribute(typeof(TableAttribute));
            if (tableAttribute == null)
                throw new MappingException("The model should have TableAttribute");

            var properties = obj.GetType().GetProperties();

            var notIdentityNames = properties.Where(p => ((p.GetCustomAttribute(typeof(ColumnAttribute)) == null)) ||
            (((ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute))).IsDbGenerated == false)).
            Select(p => p.Name);

            var primaryKeysNames = properties.Where(p => ((p.GetCustomAttribute(typeof(ColumnAttribute)) != null)) &&
            (((ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute))).IsPrimaryKey == true)).
            Select(p => p.Name);
            if (primaryKeysNames.Count() == 0)
                throw new MappingException("Models should have at least one Primary Key");

            var sqlStringCreator = new UpdateSqlStringCreator(((TableAttribute)tableAttribute).Name, notIdentityNames, primaryKeysNames);

            var sqlString = sqlStringCreator.Create();

            return Execute(sqlString, obj);
        }

        /// <summary>
        /// Inserts a model to a database
        /// </summary>
        /// <param name="obj">the model being inserting</param>
        /// <returns>1 if insertion was successful or 0 if not</returns>
        public int Insert(object obj)
        {
            var tableAttribute = obj.GetType().GetCustomAttribute(typeof(TableAttribute));
            if (tableAttribute == null)
                throw new MappingException("The model should have TableAttribute");
                
            var properties = obj.GetType().GetProperties();  

            var notIdentityNames = properties.Where(p => ((p.GetCustomAttribute(typeof(ColumnAttribute)) == null)) ||
            (((ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute))).IsDbGenerated == false)).
            Select(p => p.Name);

            var sqlStringCreator = new InsertSqlStringCreator(((TableAttribute)tableAttribute).Name, notIdentityNames);

            var sqlString = sqlStringCreator.Create();

            return Execute(sqlString, obj);
        }

        /// <summary>
        /// exequtes a query to a database
        /// </summary>
        /// <param name="sqlString">parameterized query</param>
        /// <param name="parameters">parameters</param>
        /// <returns>number of affected rows</returns>
        public int Execute(string sqlString, object parameters)
        {
            var commandCreator = new ParameterSqlCommandCreator(sqlString, _connection, parameters);
            var command = commandCreator.Create();

            if(_connection.State == ConnectionState.Closed)
                _connection.Open();
            var result = command.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        /// <summary>
        /// gets all models of type T from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>the all models of type T</returns>
        public IEnumerable<T> GetAll<T>()
        {
            var tableAttribute = typeof(T).GetCustomAttribute(typeof(TableAttribute));

            if (tableAttribute == null)
                throw new MappingException("Class model does not have TableAtrribute");

            string sqlString = "SELECT * FROM " + ((TableAttribute)tableAttribute).Name;

            return Query<T>(sqlString);
        }

        /// <summary>
        /// exeсutes a query to a database and maps the result to model of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sqlString, object parameters = null)
        {
            SqlCommandCreator commandCreator;

            if (parameters != null)            
                commandCreator = new ParameterSqlCommandCreator(sqlString, _connection, parameters);  
            else            
                commandCreator = new SqlCommandCreator(sqlString, _connection);

            var command = commandCreator.Create();
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);            
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            _connection.Close();            

            var properties = typeof(T).GetProperties();

            if (ds.Tables.Count != 1)
                throw new MappingException("Query result is empty");

            if (CheckMappability(properties, ds.Tables[0].Columns) == false)
                throw new MappingException("Model does not match the query result");

            var result = new List<T>();

            if (ds.Tables[0].Rows.Count == 0)
                return result;

            foreach (DataRow row in ds.Tables[0].Rows)
                result.Add(Mapping<T>(properties, row));

            return result;
        }
        private T Mapping<T>(PropertyInfo[] properties, DataRow row)
        {
            var model = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);

            foreach(PropertyInfo property in properties)
            {
                property.SetValue(model, row[property.Name]);
            }

            return (T)model;
        }

        private bool CheckMappability(PropertyInfo[] properties, DataColumnCollection columns)
        {
            if (properties.Count() != columns.Count)
                return false;

            var castedColumns = columns.Cast<DataColumn>();

            foreach(PropertyInfo property in properties)
                if(castedColumns.Any(column => (column.ColumnName == property.Name) && 
                ((column.DataType == property.PropertyType) || (column.DataType == typeof(int)) && (property.PropertyType.BaseType == typeof(Enum)) )) == false)                      
                    return false;            
            return true;

        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
