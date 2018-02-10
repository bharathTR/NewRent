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
    }
}