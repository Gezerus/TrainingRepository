using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    /// <summary>
    /// scripts runner
    /// </summary>
    public static class ScriptRunner
    {
        static string _sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";
        /// <summary>
        /// runs the script to create the school database if it does not exist
        /// </summary>
        public static void CreateSchoolDbIfNotExist()
        {
            SqlConnection conn = new SqlConnection(_sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            string chekString = "IF DB_ID ('School') IS NULL " +
                "SELECT 1 " +
                "ELSE " +
                "SELECT 0";

            if ((int)server.ConnectionContext.ExecuteScalar(chekString) == 1)
            {
                FileInfo file = new FileInfo(@"..\..\..\School\Scripts\SchoolScript.sql");

                string script = file.OpenText().ReadToEnd();

                server.ConnectionContext.ExecuteNonQuery(script);
            }
            conn.Close();
        }

        public static void DeleteSchoolDbIfExist()
        {
            SqlConnection conn = new SqlConnection(_sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            string chekString = "IF DB_ID ('School') IS NULL " +
                "SELECT 1 " +
                "ELSE " +
                "SELECT 0";

            if((int)server.ConnectionContext.ExecuteScalar(chekString) == 0)
                server.ConnectionContext.ExecuteNonQuery("DROP DATABASE School");
            conn.Close();
        }
    }
}
