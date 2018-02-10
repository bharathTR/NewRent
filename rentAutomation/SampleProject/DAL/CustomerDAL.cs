using Oracle.ManagedDataAccess.Client;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.DAL
{
    public class CustomerDAL
    {
        LoginModel Loginobj = new LoginModel();
        public List<CustomerModel> getAllTableDetails()
        {
            List<CustomerModel> regdetails = new List<CustomerModel>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            cmd = new SqlCommand("[CustomerRecordsDesc]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_id", 0);
            cmd.Parameters.AddWithValue("@first_name", "");
            cmd.Parameters.AddWithValue("@last_name", "");
            cmd.Parameters.AddWithValue("@city", "");
            cmd.Parameters.AddWithValue("@country", "");
            cmd.Parameters.AddWithValue("@phone", "");
            cmd.Parameters.AddWithValue("@H_id", 0);
            cmd.Parameters.AddWithValue("@middle_name", "");
            cmd.Parameters.AddWithValue("@mobileno", "");
            cmd.Parameters.AddWithValue("@address", "");
            cmd.Parameters.AddWithValue("@username", "");
            cmd.Parameters.AddWithValue("@password", "");
            cmd.Parameters.AddWithValue("@lastlogindate", "");
            cmd.Parameters.AddWithValue("@state", "");
            cmd.Parameters.AddWithValue("@action", "");
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(Dt);

            //GlobalConnection conn = new GlobalConnection();
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn.getConnection();
            //cmd.CommandText = "FetchCustomerRecords";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Transaction = null;
            //cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = 0;
            //cmd.Parameters.Add("p_resultset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            //DataTable Dt = new DataTable();
            //OracleDataAdapter da = new OracleDataAdapter(cmd);
            //da.Fill(Dt);

            foreach (DataRow dR in Dt.Rows)
            {
                regdetails.Add(new CustomerModel
                {
                    id = Convert.ToInt32(dR["LOGINID"]),
                    firstName = Convert.ToString(dR["FIRSTNAME"]),
                    lastName = Convert.ToString(dR["LASTNAME"]),
                    phoneNO = Convert.ToString(dR["phone"]),
                    country = Convert.ToString(dR["COUNTRY"]),
                    h_id = Convert.ToInt32(dR["h_id"]),
                    middleName = Convert.ToString(dR["MIDDLENAME"]),
                    mobileNo = Convert.ToString(dR["MOBILENO"]),
                    address = Convert.ToString(dR["country"]),
                    userName = Convert.ToString(dR["USERNAME"]),
                    password = Convert.ToString(dR["country"]),
                    lastLoginDate = Convert.ToString(dR["LASTLOGGEDDATE"]),
                    state = Convert.ToString(dR["STATE"]),
                    city = Convert.ToString(dR["CITY"]),
                    houseNo = Convert.ToString(dR["H_NUMBER"]),
                    blockNo = Convert.ToString(dR["H_BLOCK"]),
                    floorNo = Convert.ToString(dR["H_FLOOR_NO"]),
                });

            }

            return regdetails;
        }

        internal DataSet getLocation()
        {
            List<SelectListItem> listLocations = new List<SelectListItem>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            cmd = new SqlCommand("[getLocation]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dset);
            return dset;
        }

        internal DataTable FetchRecord(int id)
        {
            DataTable Dt = new DataTable();
            //SqlConnection objcon = new SqlConnection();
            //objcon.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            //SqlCommand cmd;
            //objcon.Open();
            //cmd = new SqlCommand("FetchStudentRecordsDesc", objcon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("p_id", id);
            //SqlDataAdapter sd = new SqlDataAdapter(cmd);
            //DataTable Dt = new DataTable();

            //GlobalConnection conn = new GlobalConnection();
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn.getConnection();
            //cmd.CommandText = "FetchCustomerRecords";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Transaction = null;
            //cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = id;
            //cmd.Parameters.Add("p_resultset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            //OracleDataAdapter da = new OracleDataAdapter(cmd);
            //da.Fill(Dt);
            return Dt;
        }

        internal string[] deleteRecord(string id)
        {
            string[] res = new string[3];
            //SqlConnection objcon = new SqlConnection();
            //objcon.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            //SqlCommand cmd;
            //objcon.Open();
            //cmd = new SqlCommand("DeleteCustomer", objcon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@p_id", id);
            //cmd.Parameters.AddWithValue("@p_Action", "D");
            //res[0] = cmd.ExecuteNonQuery().ToString();
            //res[0] = "Deleted";

            //GlobalConnection conn = new GlobalConnection();
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn.getConnection();
            //cmd.CommandText = "DeleteCustomer";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("p_id", OracleDbType.Long).Value = id;
            //cmd.Parameters.Add("p_Action", OracleDbType.Varchar2).Value = "D";
            //cmd.ExecuteNonQuery();
            res[0] = "Deleted";
            return res;
        }

        internal string[] SaveRecord(int id, string FirstName, string LastName, string Contact, string City, string Country)
        {

            string[] res = new string[3];

            SqlCommand cmd = null;
            GlobalConnection conn = new GlobalConnection();
            cmd.Connection = GlobalConnection.getConnection();
            //conn.Open();

            if (id == 0)
            {

                //cmd = new SqlCommand("AddNewCustomer", objcon);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@p_id", id);
                //cmd.Parameters.AddWithValue("@p_FirstName", FirstName);
                //cmd.Parameters.AddWithValue("@p_LastName", LastName);
                //cmd.Parameters.AddWithValue("@p_contact_Number", Contact);
                //cmd.Parameters.AddWithValue("@p_City", City);
                //cmd.Parameters.AddWithValue("@p_Country", Country);
                //cmd.Parameters.AddWithValue("@p_Action", "I");
                //res[0] = cmd.ExecuteNonQuery().ToString();
                //res[0] = "Saved";

                //cmd.CommandText = "AddNewCustomer";
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("p_id", OracleDbType.Long).Value = id;
                //cmd.Parameters.Add("p_FirstName", OracleDbType.Varchar2).Value = FirstName;
                //cmd.Parameters.Add("p_LastName", OracleDbType.Varchar2).Value = LastName;
                //cmd.Parameters.Add("p_contact_Number", OracleDbType.Varchar2).Value = Contact;
                //cmd.Parameters.Add("p_City", OracleDbType.Varchar2).Value =City;
                //cmd.Parameters.Add("p_Country", OracleDbType.Varchar2).Value = Country;
                //cmd.Parameters.Add("p_Action", OracleDbType.Varchar2).Value = "I";
                //cmd.ExecuteNonQuery();
                res[0] = "Saved";
            }
            else if (id != 0)
            {
                //cmd = new SqlCommand("AddNewCustomer", objcon);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@p_id", id);
                //cmd.Parameters.AddWithValue("@p_FirstName", FirstName);
                //cmd.Parameters.AddWithValue("@p_LastName", LastName);
                //cmd.Parameters.AddWithValue("@p_contact_Number", Contact);
                //cmd.Parameters.AddWithValue("@p_City", City);
                //cmd.Parameters.AddWithValue("@p_Country", Country);
                //cmd.Parameters.AddWithValue("@p_Action", "U");
                //res[0] = cmd.ExecuteNonQuery().ToString();
                //res[0] = "Updated";
                cmd.CommandText = "AddNewCustomer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id", OracleDbType.Long).Value = id;
                cmd.Parameters.Add("p_FirstName", OracleDbType.Varchar2).Value = FirstName;
                cmd.Parameters.Add("p_LastName", OracleDbType.Varchar2).Value = LastName;
                cmd.Parameters.Add("p_contact_Number", OracleDbType.Varchar2).Value = Contact;
                cmd.Parameters.Add("p_City", OracleDbType.Varchar2).Value = City;
                cmd.Parameters.Add("p_Country", OracleDbType.Varchar2).Value = Country;
                cmd.Parameters.Add("p_Action", OracleDbType.Varchar2).Value = "U";
                cmd.ExecuteNonQuery();
                res[0] = "Updated";
            }


            return res;

        }
    }
}