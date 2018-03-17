using log4net;
using MySql.Data.MySqlClient;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.DAL
{
   
    public class TicketDAL
    {
         public static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //ILog log=log4net.LogManager.GetLogger("MyAppender");
        public List<TicketModel> getAllTableDetails(int id)

        {
            List<TicketModel> objTicketModel = new List<TicketModel>();
            MySqlCommand cmd;
            try
            {


                MySqlConnection con = GlobalConnection.getConnection();
                DataTable Dt = new DataTable();
                cmd = new MySqlCommand("getTicketList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_p_LoginId", id);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(Dt);


                foreach (DataRow dR in Dt.Rows)
                {
                    objTicketModel.Add(new TicketModel
                    {
                        TicketID = Convert.ToString(dR["TICKET_ID"]),
                        Type = Convert.ToString(dR["TICKET_TYPE"]),
                        Description = Convert.ToString(dR["TICKET_DESC"]),
                        Raised_Date = Convert.ToString(dR["TICKET_RAISED_DATE"]),
                        Status = Convert.ToString(dR["TICKET_STATUS"]),
                        TicketNo = Convert.ToString(dR["TICKET_NUMBER"]),

                    });

                }

                return objTicketModel;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return objTicketModel;
            }
        }
        
        public DataTable FetchTicketDetail(int id,int a_id)

        {
            List<TicketModel> objTicketModel = new List<TicketModel>();
            MySqlCommand cmd;
            MySqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            try
            {
                cmd = new MySqlCommand("FetchTicketDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_Ticket_id", id);
                cmd.Parameters.AddWithValue("?p_Apart_id", a_id);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(Dt);
                return Dt;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return Dt;
            }
        }
        internal DataTable GetPhoneNumber(int loginID)
        {

            MySqlCommand cmd;
            string MobileNumber = string.Empty;
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            try
            {
            
            MySqlConnection con = GlobalConnection.getConnection();
            cmd = new MySqlCommand("GetPhoneNumber", con);
            cmd.Parameters.AddWithValue("?p_loginId", loginID);
            cmd.Parameters.AddWithValue("?p_Phone", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("?p_FIRSTNAME", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("?p_LASTNAME", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            sd.Fill(dset);
            if (dset != null)
            {


                Dt = dset.Tables[0];

            }
            return Dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Dt;
            }

        }
        internal DataTable GetTicketNumber(int loginID)
        {

            MySqlCommand cmd;
            string MobileNumber = string.Empty;
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            try
            {


                MySqlConnection con = GlobalConnection.getConnection();
                cmd = new MySqlCommand("GetTicketNumber", con);
                cmd.Parameters.AddWithValue("?p_loginId", loginID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(dset);
                if (dset != null)
                {


                    Dt = dset.Tables[0];

                }
                return Dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Dt;
            }
        }



        public List<TicketModel> getAllTableDetailsOwner(int id)

        {
            List<TicketModel> objTicketModel = new List<TicketModel>();
            MySqlCommand cmd;
            DataTable Dt = new DataTable();

            try
            {
                MySqlConnection con = GlobalConnection.getConnection();
                cmd = new MySqlCommand("getTicketListOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_p_ApartmentID", id);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(Dt);
                foreach (DataRow dR in Dt.Rows)
                {
                    objTicketModel.Add(new TicketModel
                    {

                        TicketID = Convert.ToString(dR["TICKET_ID"]),
                        Type = Convert.ToString(dR["TICKET_TYPE"]),
                        Description = Convert.ToString(dR["TICKET_DESC"]),
                        Raised_Date = Convert.ToString(dR["TICKET_RAISED_DATE"]),
                        Status = Convert.ToString(dR["TICKET_STATUS"]),
                        FIRSTNAME = Convert.ToString(dR["FIRSTNAME"]),
                        LASTNAME = Convert.ToString(dR["LASTNAME"]),
                        MOBILENO = Convert.ToString(dR["MOBILENO"]),
                        H_FLOOR_NO = Convert.ToString(dR["H_FLOOR_NO"]),
                        H_Number = Convert.ToString(dR["H_Number"]),
                        H_BLOCK = Convert.ToString(dR["H_BLOCK"]),
                        TicketNo = Convert.ToString(dR["TICKET_NUMBER"]),

                    });

                }

                return objTicketModel;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return objTicketModel;
            }
        }

        internal int SaveNewTicket(string Type, string Desc, int id)
        {

            MySqlCommand cmd;
            DataTable Dt = new DataTable();
            int res=0;
            try
            {


                MySqlConnection con = GlobalConnection.getConnection();
                cmd = new MySqlCommand("createNewTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_p_TICKET_TYPE", Type);
                cmd.Parameters.AddWithValue("?p_p_TICKET_DESC", Desc);
                cmd.Parameters.AddWithValue("?p_p_TICKET_STATUS", "Pending");
                cmd.Parameters.AddWithValue("?p_p_RAISED_BY_LOGIN_ID", id);
                cmd.Parameters.AddWithValue("?p_p_TICKET_FLAG", "0");

                res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return res;
            }


        }

        internal TicketModel getTicketCout( int id)
        {

            MySqlCommand cmd;
            DataTable Dt = new DataTable();
            TicketModel lstTicketCount = new TicketModel();
            MySqlConnection con = GlobalConnection.getConnection();
            try
            {
                cmd = new MySqlCommand("sp_TicketCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_Apartment_ID", id);
                cmd.Parameters.AddWithValue("?p_Processing", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?p_Pending", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?p_Completed", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?p_Total", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                int p_processing = (int)cmd.Parameters["?p_Processing"].Value;
                int p_completed = (int)cmd.Parameters["?p_Completed"].Value;
                int p_pending = (int)cmd.Parameters["?p_Pending"].Value;
                int p_total = (int)cmd.Parameters["?p_Total"].Value;


                lstTicketCount.completed = p_completed;
                lstTicketCount.pending = p_pending;
                lstTicketCount.processing = p_processing;
                lstTicketCount.total = p_total;
                return lstTicketCount;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return lstTicketCount;
            }

        }

        internal int TicketStatusUpdate(int Ticketid, int a_id, string time, string response, string ExpectedClosedate, string progress)
        {
           
            MySqlCommand cmd;
            DataTable Dt = new DataTable();
            int res = 0;
            try
            {
                MySqlConnection con = GlobalConnection.getConnection();

                cmd = new MySqlCommand("sp_TicketStatusupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_p_Ticketid", Ticketid);
                cmd.Parameters.AddWithValue("?p_p_ApartmentID", a_id);
                cmd.Parameters.AddWithValue("?p_p_TICKET_FLAG", 1);
                cmd.Parameters.AddWithValue("?p_p_TICKET_SLOT", time);
                cmd.Parameters.AddWithValue("?p_p_TICKET_EXP_CLOSURE_DATE", ExpectedClosedate);
                cmd.Parameters.AddWithValue("?p_p_TICKET_RESPONSE", response);
                cmd.Parameters.AddWithValue("?p_p_TICKET_STATUS", progress);
                 res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return res;
            }
        }

        internal DataSet getServiceTypes()
        {
            List<SelectListItem> listLocations = new List<SelectListItem>();
            MySqlCommand cmd;
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            try
            {
                MySqlConnection con = GlobalConnection.getConnection();

                cmd = new MySqlCommand("sp_getServiceTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(dset);
                return dset;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return dset;
            }
        }

        internal List<SelectListItem> getSlots()
        {
            List<SelectListItem> listTimeSlots = new List<SelectListItem>();
            MySqlCommand cmd;
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            try
            {
                MySqlConnection con = GlobalConnection.getConnection();

                cmd = new MySqlCommand("sp_Timeslots", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                sd.Fill(Dt);
                foreach (DataRow dR in Dt.Rows)
                {
                    listTimeSlots.Add(new SelectListItem
                    {

                        Value = Convert.ToString(dR["SERVICE_ID"]),
                        Text = Convert.ToString(dR["SERVICETYPE"]),


                    });

                }
                return listTimeSlots;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return listTimeSlots;
            }

        }
    }
}