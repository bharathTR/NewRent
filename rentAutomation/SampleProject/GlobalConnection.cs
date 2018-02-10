using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SampleProject
{
    
    public class GlobalConnection
    {
        //public static OracleConnection conn = null;
        public static SqlConnection conn;

        public static SqlConnection getConnection()
        {
            //OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
            conn.Open();

            return conn;
        }
    }
}