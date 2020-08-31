using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM
{
    /// <summary>
    /// creator of non-parameterized commands to sql server database
    /// </summary>
    public class SqlCommandCreator
    {
        private string _sqlString;
        private SqlConnection _connection;

        public SqlCommandCreator(string sqlString, SqlConnection connection)
        {
            _sqlString = sqlString;
            _connection = connection;
        }
        /// <summary>
        /// creates sqlCommand
        /// </summary>
        /// <returns></returns>
        public virtual SqlCommand Create()
        {
            return new SqlCommand(_sqlString, _connection);
        }
    }
}
