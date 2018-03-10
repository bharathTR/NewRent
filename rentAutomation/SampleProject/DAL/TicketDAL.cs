﻿using log4net;
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
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            cmd = new SqlCommand("[getTicketList]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_LoginId", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
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
        
        public DataTable FetchTicketDetail(int id,int a_id)

        {
            List<TicketModel> objTicketModel = new List<TicketModel>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            try
            {
                cmd = new SqlCommand("[FetchTicketDetail]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ticket_id", id);
                cmd.Parameters.AddWithValue("@Apart_id", a_id);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
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

            SqlCommand cmd;
            string MobileNumber = string.Empty;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            cmd = new SqlCommand("[GetPhoneNumber]", con);
            cmd.Parameters.AddWithValue("@loginId", loginID);
            cmd.Parameters.Add("@Phone", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@FIRSTNAME", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@LASTNAME", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dset);
            if (dset != null)
            {


                Dt = dset.Tables[0];

            }
            return Dt;
        }
        internal DataTable GetTicketNumber(int loginID)
        {

            SqlCommand cmd;
            string MobileNumber = string.Empty;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            cmd = new SqlCommand("[GetTicketNumber]", con);
            cmd.Parameters.AddWithValue("@loginId", loginID);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dset);
            if (dset != null)
            {


                Dt = dset.Tables[0];

            }
            return Dt;
        }



        public List<TicketModel> getAllTableDetailsOwner(int id)

        {
            List<TicketModel> objTicketModel = new List<TicketModel>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            cmd = new SqlCommand("[getTicketListOwner]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_ApartmentID", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
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

        internal int SaveNewTicket(string Type,string Desc,int id)
        {
            
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            cmd = new SqlCommand("[createNewTicket]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_TICKET_TYPE", Type);
            cmd.Parameters.AddWithValue("@p_TICKET_DESC", Desc);
            cmd.Parameters.AddWithValue("@p_TICKET_STATUS","Pending");
            cmd.Parameters.AddWithValue("@p_RAISED_BY_LOGIN_ID", id);
            cmd.Parameters.AddWithValue("@p_TICKET_FLAG", "0");
            int res=cmd.ExecuteNonQuery();
            return res;
        }

        internal TicketModel getTicketCout( int id)
        {

            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            TicketModel lstTicketCount = new TicketModel();
            cmd = new SqlCommand("[sp_TicketCount]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Apartment_ID", id);
            cmd.Parameters.Add("@Processing", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pending", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Completed", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Total", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            int p_processing = (int)cmd.Parameters["@Processing"].Value;
            int p_completed = (int)cmd.Parameters["@Completed"].Value;
            int p_pending = (int)cmd.Parameters["@Pending"].Value;
            int p_total = (int)cmd.Parameters["@Total"].Value;


            lstTicketCount.completed = p_completed;
            lstTicketCount.pending = p_pending;
            lstTicketCount.processing = p_processing;
            lstTicketCount.total = p_total;

             
        
            
            return lstTicketCount;
        }

        internal int TicketStatusUpdate(int Ticketid, int a_id, string time, string response, string ExpectedClosedate, string progress)
        {
           
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            cmd = new SqlCommand("[sp_TicketStatusupdate]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Ticketid", Ticketid);
            cmd.Parameters.AddWithValue("@p_ApartmentID", a_id);
            cmd.Parameters.AddWithValue("@p_TICKET_FLAG", 1);
            cmd.Parameters.AddWithValue("@p_TICKET_SLOT", time);
            cmd.Parameters.AddWithValue("@p_TICKET_EXP_CLOSURE_DATE", ExpectedClosedate);
            cmd.Parameters.AddWithValue("@p_TICKET_RESPONSE", response);
            cmd.Parameters.AddWithValue("@p_TICKET_STATUS", progress);
            int res = cmd.ExecuteNonQuery();
            return res;
        }

        internal DataSet getServiceTypes()
        {
            List<SelectListItem> listLocations = new List<SelectListItem>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            cmd = new SqlCommand("[sp_getServiceTypes]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dset);
            return dset;
        }

        internal List<SelectListItem> getSlots()
        {
            List<SelectListItem> listTimeSlots = new List<SelectListItem>();
            SqlCommand cmd;
            SqlConnection con = GlobalConnection.getConnection();
            DataTable Dt = new DataTable();
            DataSet dset = new DataSet();
            cmd = new SqlCommand("[sp_Timeslots]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
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
    }
}