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
                  .Where(c => isRetSearchable && c.TicketID.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filteredItems = listdetails;
            }

            var isRetSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isStoreInvSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isFromStoreSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<TicketModel, string> orderingFunction = (c => sortColumnIndex == 2 && isRetSortable ? c.TicketID :
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
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.TicketID), c.Type, c.Description, c.Raised_Date, c.Status};



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




    }
}