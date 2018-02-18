using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class TicketModel
    {

public string TicketID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string MOBILENO { get; set; }
        public string H_FLOOR_NO { get; set; }
        public string H_Number { get; set; }
        public string H_BLOCK { get; set; }
        public string Type { get; set; }
public string Description { get; set; }
public string Raised_Date { get; set; }
public string Status { get; set; }
public int? Raised_By_Login_Id { get; set; }
        public string TimeSlot { get; set; }
        public string Response { get; set; }
        public string ExpCloseDate { get; set; }

        public string TicketNo { get; set; }








    }
}