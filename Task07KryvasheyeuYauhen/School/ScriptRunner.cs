using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        static string _schoolConString = "Server = localhost; Integrated Security = SSPI; database=School";
        static string _masterConString = "Server=localhost;Integrated Security=SSPI; database=master";
        /// <summary>
        /// runs the script to create the school database if it does not exist
        /// </summary>
        public static void CreateSchoolDbIfNotExist()
        {
            SqlConnection conn = new SqlConnection(_masterConString);

            string chekString = "IF DB_ID ('School') IS NULL " +
                "SELECT 1 " +
                "ELSE " +
                "SELECT 0";
            SqlCommand sqlCommand = new SqlCommand(chekString, conn);
            conn.Open();

            if ((int)sqlCommand.ExecuteScalar() == 1)
            {
                sqlCommand.CommandText = "CREATE DATABASE School";
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                conn.ConnectionString = _schoolConString;
                FileInfo file = new FileInfo(@"..\..\..\School\Scripts\SchoolScript.sql");
                string script = file.OpenText().ReadToEnd();
                conn.Open();

                sqlCommand.CommandText = script;
                sqlCommand.ExecuteNonQuery();
            }
            conn.Close();
        }

        public static void DeleteSchoolDbIfExist()
        {
            SqlConnection conn = new SqlConnection(_masterConString);
            string chekString = "IF DB_ID ('School') IS NULL " +
                "SELECT 1 " +
                "ELSE " +
                "SELECT 0";

            SqlCommand sqlCommand = new SqlCommand(chekString, conn);
            conn.Open();

            if ((int)sqlCommand.ExecuteScalar() == 0)
            {
                sqlCommand.CommandText = "ALTER DATABASE School SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE School";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            conn.Close();

        }
    }
}
