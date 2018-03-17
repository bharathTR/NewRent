using SampleProject.DAL;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace SampleProject.Controllers
{
    [SessionFilter.SessionExpireFilter]
    public class TicketController : Controller 
    {
        // GET: Ticket
        public static readonly log4net.ILog log =
       log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        TicketDAL objgetTicket = new TicketDAL();
        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketView()
        {
            string id=Convert.ToString(Session["LOGINID"]);
            TempData.Remove("TicketList");

            return View();
        }

        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketViewGrid(jQueryDataTableParamModel param)
        {
            
            List<TicketModel> listdetails = new List<TicketModel>();
            try
            {
                if (TempData.Peek("TicketList") == null)
                {
                    listdetails = objgetTicket.getAllTableDetails(Convert.ToInt32(Session["LoginID"]));
                    TempData["TicketList"] = listdetails;
                }

                else
                {
                    listdetails = TempData.Peek("TicketList") as List<TicketModel>;
                }


                IEnumerable<TicketModel> filteredItems;

                if (!string.IsNullOrEmpty(param.sSearch))
                {

                    var isRetSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                    var isStoreInvSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                    var isFromStoreSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                    filteredItems = listdetails
                      .Where(c => isRetSearchable && c.TicketNo.ToLower().Contains(param.sSearch.ToLower()));

                }
                else
                {
                    filteredItems = listdetails;
                }

                var isRetSortable = Convert.ToBoolean(Request["bSortable_1"]);
                var isStoreInvSortable = Convert.ToBoolean(Request["bSortable_2"]);
                var isFromStoreSortable = Convert.ToBoolean(Request["bSortable_3"]);
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Func<TicketModel, string> orderingFunction = (c => sortColumnIndex == 2 && isRetSortable ? c.TicketNo :
                   "");

                var sortDirection = Request["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                {
                    filteredItems = filteredItems.OrderBy(orderingFunction);
                }
                else
                {
                    filteredItems = filteredItems.OrderByDescending(orderingFunction);
                }

                //var temp= filteredItems.Select(m=>m.)
                var displayedCompanies = filteredItems.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                var result = from c in displayedCompanies select new[] { Convert.ToString(c.TicketID), c.TicketNo, c.Type, c.Description, c.Raised_Date, c.Status };



                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = listdetails.Count(),
                    iTotalDisplayRecords = filteredItems.Count(),
                    aaData = result,

                },
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }

        }
        ///////////////////////////////////////////////////////////////////////////////////////New Ticket//////////////////
        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketCreate()
        {

            string id = Convert.ToString(Session["LOGINID"]);
            List<SelectListItem> lstServiceTypes = new List<SelectListItem>();
            DataSet ds = new DataSet();
            try
            {
                ds = objgetTicket.getServiceTypes();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dR in ds.Tables[0].Rows)
                    {
                        lstServiceTypes.Add(new SelectListItem
                        {
                            Text = Convert.ToString(dR["ServiceType"]),
                            Value = Convert.ToString(dR["Service_ID"])

                        });

                    }
                    ViewBag.listServices = lstServiceTypes;
                }
                return View();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }
        }
        [SessionFilter.SessionExpireFilter]
        public JsonResult TicketCreateNew(string Type, string Desc)
        {
            int id = Convert.ToInt32(Session["LoginID"]);
            DataTable dt = new DataTable();
            DataTable dtCustomerData = new DataTable();
            try
            {
                int result = objgetTicket.SaveNewTicket(Type, Desc, id);
                string res = string.Empty;
                //if (result == 1)
                // {
                //     dtCustomerData = objgetTicket.GetPhoneNumber(id);
                //     DataRow row = dtCustomerData.Rows[0];
                //     string phoneNumber = row["Phone"].ToString();
                //     string Fname = row["FIRSTNAME"].ToString();
                //     string Lname = row["LASTNAME"].ToString();

                //     dt = objgetTicket.GetTicketNumber(id);
                //     DataRow dr = dt.Rows[0];
                //     if (phoneNumber != "")
                //     {

                //         string sms = "Hello" + " " + Fname + " " + Lname + " " + "This is a notification that Ticket No:" + dr["TICKET_NUMBER"].ToString() + " was created.";
                //         String message = HttpUtility.UrlEncode(sms);
                //         using (var wb = new WebClient())
                //         {
                //             byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                //     {
                //     {"apikey" , "h9iDVofhwqM-SxRs1zOpbwMXjhCaIdf0bWYHmsGZld"},
                //     {"numbers" , phoneNumber},
                //     {"message" , message},
                //     {"sender" , "TXTLCL"}
                //     });
                //             res = System.Text.Encoding.UTF8.GetString(response);

                //         }
                //     }
                // }

                //if (result == 1)
                //{
                //    TicketModel ticmodobj = new TicketModel();
                //    ticmodobj.To = "Bluepebbles.94@gmail.com";
                    
                //        MailMessage mm = new MailMessage();
                //        mm.Subject = "TEst mail";//model.Subject;
                //        mm.Body = "THis is a test mail ....Reply back if you reccieve it";
                //        mm.To.Add(ticmodobj.To);//mm.IsBodyHtml = false;
                //    using (SmtpClient smtp = new SmtpClient())
                //        {
                //            smtp.Send(mm);
                //            ViewBag.Message = "Email sent.";
                //        }
                //}

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }
        }



        [SessionFilter.SessionExpireFilter]
        public ActionResult OwnerTCKView()
        {
            int a_id = Convert.ToInt32(Session["ApartmentID"]);
            TicketModel lstTcount;
            try
            {
                lstTcount = objgetTicket.getTicketCout(a_id);
                return View(lstTcount);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }
        }
        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketViewGridOwner(jQueryDataTableParamModel param)
        {

            List<TicketModel> listdetails = new List<TicketModel>();
            try
            {
                if (TempData.Peek("TicketListOwner") == null)
                {
                    listdetails = objgetTicket.getAllTableDetailsOwner(Convert.ToInt32(Session["ApartmentID"]));
                    TempData["TicketListOwner"] = listdetails;
                }

                else
                {
                    listdetails = TempData.Peek("TicketListOwner") as List<TicketModel>;
                }


                IEnumerable<TicketModel> filteredItems;

                if (!string.IsNullOrEmpty(param.sSearch))
                {

                    var isRetSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                    var isStoreInvSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                    var isFromStoreSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                    filteredItems = listdetails
                      .Where(c => isRetSearchable && c.TicketNo.ToLower().Contains(param.sSearch.ToLower()));

                }
                else
                {
                    filteredItems = listdetails;
                }

                var isRetSortable = Convert.ToBoolean(Request["bSortable_1"]);
                var isStoreInvSortable = Convert.ToBoolean(Request["bSortable_2"]);
                var isFromStoreSortable = Convert.ToBoolean(Request["bSortable_3"]);
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Func<TicketModel, string> orderingFunction = (c => sortColumnIndex == 2 && isRetSortable ? c.TicketNo :
                   "");

                var sortDirection = Request["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                {
                    filteredItems = filteredItems.OrderBy(orderingFunction);
                }
                else
                {
                    filteredItems = filteredItems.OrderByDescending(orderingFunction);
                }

                //var temp= filteredItems.Select(m=>m.)
                var displayedCompanies = filteredItems.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                var result = from c in displayedCompanies select new[] { Convert.ToString(c.TicketID), c.TicketNo, c.FIRSTNAME, c.LASTNAME, c.MOBILENO, c.H_Number, c.H_BLOCK, c.Type, c.Description, c.Raised_Date, c.Status };



                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = listdetails.Count(),
                    iTotalDisplayRecords = filteredItems.Count(),
                    aaData = result,

                },
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }

        }
        [SessionFilter.SessionExpireFilter]
        public JsonResult getTicketID(string id)
        {
            TempData["tickID"] = id;
            return Json(JsonRequestBehavior.AllowGet);
        }
       

        [SessionFilter.SessionExpireFilter]
        public ActionResult bindTicketDetails()
        {

            TicketModel t = new TicketModel();
            try
            {
                int data = Convert.ToInt32(TempData["tickID"]);
                int a_id = Convert.ToInt32(Session["ApartmentID"]);
                
                DataTable dt = objgetTicket.FetchTicketDetail(Convert.ToInt32(data), a_id);
                t.TicketID = Convert.ToString(dt.Rows[0]["TICKET_ID"]);
                t.FIRSTNAME = Convert.ToString(dt.Rows[0]["FIRSTNAME"]);
                t.LASTNAME = Convert.ToString(dt.Rows[0]["LASTNAME"]);
                t.MOBILENO = Convert.ToString(dt.Rows[0]["MOBILENO"]);
                t.H_BLOCK = Convert.ToString(dt.Rows[0]["H_BLOCK"]);
                t.H_Number = Convert.ToString(dt.Rows[0]["H_Number"]);
                t.Type = Convert.ToString(dt.Rows[0]["TICKET_TYPE"]);
                t.Description = Convert.ToString(dt.Rows[0]["TICKET_DESC"]);
                t.Raised_Date = Convert.ToString(dt.Rows[0]["TICKET_RAISED_DATE"]);
                List<SelectListItem> lstTimeSlots = new List<SelectListItem>();
                lstTimeSlots = objgetTicket.getSlots();
                ViewBag.TimeSlots = lstTimeSlots;
                return View(t);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time",JsonRequestBehavior.AllowGet);
            }
        }
        [SessionFilter.SessionExpireFilter]
        public JsonResult TicketStatusUpdate(string Ticketid,string time,string  response, string Expectedrosolvedate, string progress)
        {
            int a_id = Convert.ToInt32(Session["ApartmentID"]);
            try
            {
                int result = objgetTicket.TicketStatusUpdate(Convert.ToInt32(Ticketid), a_id, time, response, Expectedrosolvedate, progress);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("Something went wrong..Try after some Time", JsonRequestBehavior.AllowGet);
            }
        }
    }
}