using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public long MobileNO { get; set; }
        public string Address { get; set; }


        public bool Checked { get; set; }

        //internal string[] Check(string user, string pass)
        //{
        //    string[] res = new string[10];
        //    SqlConnection objcon = new SqlConnection();
        //    objcon.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
        //    SqlCommand cmd;
        //    objcon.Open();
        //     cmd = new SqlCommand("CheckUSerExistence", objcon);   
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@User",user);
        //    cmd.Parameters.Add("@pass", pass);

        //    SqlDataAdapter sd = new SqlDataAdapter(cmd);
        //    DataTable DS = new DataTable();
        //    sd.Fill(DS);

        //    foreach (DataRow dR in DS.Rows)
        //    {

        //        userName = Convert.ToString(dR["userName"]);
        //        password = Convert.ToString(dR["password"]);

        //    }
            
        //    if((userName == user)&&(password==pass))  //when you click login
        //    {
        //        res[0] = "1";
        //    }
            
            


        //    return res;
        //}

        //internal string[] Register(LoginModel regLog)
        //{
        //    string[] status = new string[10];
        //    SqlConnection objcon = new SqlConnection();
        //    objcon.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
        //    SqlCommand cmd;
        //    objcon.Open();
        //    status = Check(regLog.userName,regLog.password);
        //    if(status[0] == "1")
        //    {
        //        status[1] = "Exists";
        //    }
        //    else
        //    {
                
        //        cmd = new SqlCommand("AddUser", objcon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@User", regLog.userName);
        //        cmd.Parameters.Add("@pass", regLog.password);
        //        cmd.ExecuteNonQuery();
        //        status[1] = "User Added";

        //    }


        //    return status;

        //}
    }
}