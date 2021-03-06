﻿using System;
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

        public int processing { get; set; }
        public int completed { get; set; }
        public int total { get; set; }
        public int pending { get; set; }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }






    }

}