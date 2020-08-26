using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace SimpleORM
{
    public class ADOReflectionOrmDataContext : IOrmDataContext , IDisposable
    {
        private SqlConnection _connection;
        
        public ADOReflectionOrmDataContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }



        public IEnumerable<T> GetAll<T>()
        {            
            var tableAttribute = typeof(T).GetCustomAttribute(typeof(TableAttribute));

            if (tableAttribute == null)
                throw new MappingException("Class model does not have TableAtrribute");

            string sqlString = "SELECT * FROM " + ((TableAttribute)tableAttribute).Name;

            return Query<T>(sqlString);
        }

        public IEnumerable<T> Query<T>(string SqlString)
        {
            _connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(SqlString, _connection);

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
