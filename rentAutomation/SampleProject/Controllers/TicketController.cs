using SampleProject.DAL;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SampleProject.Controllers
{
    [SessionFilter.SessionExpireFilter]
    public class TicketController : Controller
    {
        // GET: Ticket
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
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.TicketID),c.TicketNo, c.Type, c.Description, c.Raised_Date, c.Status};



            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listdetails.Count(),
                iTotalDisplayRecords = filteredItems.Count(),
                aaData = result,

            },
                        JsonRequestBehavior.AllowGet);

        }
        ///////////////////////////////////////////////////////////////////////////////////////New Ticket//////////////////
        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketCreate()
        {

            string id = Convert.ToString(Session["LOGINID"]);
            List<SelectListItem> lstServiceTypes = new List<SelectListItem>();
            DataSet ds = new DataSet();
            ds=objgetTicket.getServiceTypes();
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
        [SessionFilter.SessionExpireFilter]
        public JsonResult TicketCreateNew(string Type, string Desc)
        {
            int id = Convert.ToInt32(Session["LoginID"]);
            int result=objgetTicket.SaveNewTicket(Type,Desc,id);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [SessionFilter.SessionExpireFilter]
        public ActionResult OwnerTCKView()
        {
            int a_id = Convert.ToInt32(Session["ApartmentID"]);
            return View();
        }
        [SessionFilter.SessionExpireFilter]
        public ActionResult TicketViewGridOwner(jQueryDataTableParamModel param)
        {

            List<TicketModel> listdetails = new List<TicketModel>();
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
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.TicketID),c.TicketNo, c.FIRSTNAME,c.LASTNAME,c.MOBILENO,c.H_Number,c.H_BLOCK,c.Type, c.Description, c.Raised_Date, c.Status };



            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listdetails.Count(),
                iTotalDisplayRecords = filteredItems.Count(),
                aaData = result,

            },
                        JsonRequestBehavior.AllowGet);

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
            int data = Convert.ToInt32(TempData["tickID"]);
            int a_id = Convert.ToInt32(Session["ApartmentID"]);
            TicketModel t = new TicketModel();
            DataTable dt = objgetTicket.FetchTicketDetail(Convert.ToInt32(data), a_id);
            t.TicketID= Convert.ToString(dt.Rows[0]["TICKET_ID"]);
            t.FIRSTNAME = Convert.ToString(dt.Rows[0]["FIRSTNAME"]);
            t.LASTNAME = Convert.ToString(dt.Rows[0]["LASTNAME"]);
            t.MOBILENO = Convert.ToString(dt.Rows[0]["MOBILENO"]);
            t.H_BLOCK = Convert.ToString(dt.Rows[0]["H_BLOCK"]);
            t.H_Number = Convert.ToString(dt.Rows[0]["H_Number"]);
            t.Type = Convert.ToString(dt.Rows[0]["TICKET_TYPE"]);
            t.Description = Convert.ToString(dt.Rows[0]["TICKET_DESC"]);
            t.Raised_Date = Convert.ToString(dt.Rows[0]["TICKET_RAISED_DATE"]);
            List<SelectListItem> lstTimeSlots = new List<SelectListItem>();
            lstTimeSlots=objgetTicket.getSlots();
            ViewBag.TimeSlots = lstTimeSlots;
            return View(t);
        }
        [SessionFilter.SessionExpireFilter]
        public JsonResult TicketStatusUpdate(string Ticketid,string time,string  response, string Expectedrosolvedate, string progress)
        {
            int a_id = Convert.ToInt32(Session["ApartmentID"]);
            int result = objgetTicket.TicketStatusUpdate(Convert.ToInt32(Ticketid), a_id, time, response, Expectedrosolvedate, progress);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}