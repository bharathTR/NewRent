using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class TicketModel
    {

public string TicketID { get; set; }
public string Type { get; set; }
public string Description { get; set; }
public string Raised_Date { get; set; }
public string Status { get; set; }
public int? Raised_By_Login_Id { get; set; }

    }
}