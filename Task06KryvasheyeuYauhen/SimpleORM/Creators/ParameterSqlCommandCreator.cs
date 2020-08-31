using System.Data.SqlClient;
using System.Reflection;

namespace SimpleORM
{
    /// <summary>
    /// creator of parameterized commands to sql server database
    /// </summary>
    public class ParameterSqlCommandCreator : SqlCommandCreator
    {
        private object _parameters;
        public ParameterSqlCommandCreator(string sqlString, SqlConnection connection, object parameters) : base(sqlString, connection)
        {
            _parameters = parameters;         
        }
        /// <summary>
        /// creates parameterized sqlCommand
        /// </summary>
        /// <returns></returns>
        public override SqlCommand Create()
        {
            var properties = _parameters.GetType().GetProperties();

            var command = base.Create();

            foreach(PropertyInfo property in properties)
            {
                var parameter = new SqlParameter("@" + property.Name, property.GetValue(_parameters));
                command.Parameters.Add(parameter);
            }
            return command;
        }
    }

}
