using SampleProject.DAL;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SampleProject.Controllers
{


    [SessionFilter.SessionExpireFilter]
    public class CustomerController : Controller
    {
        // GET: Customer
        

        CustomerDAL  objCustDAL = new CustomerDAL();
        public ActionResult ViewCustomer(CustomerModel obj)

        {
            
            return View(obj);
        }

        [SessionFilter.SessionExpireFilter]
        public ActionResult CreateCustomer()
        {
            List<SelectListItem> listCountry = new List<SelectListItem>();
            List<SelectListItem> listStates = new List<SelectListItem>();
            List<SelectListItem> listCities = new List<SelectListItem>();
            DataSet ds = new DataSet();

            //ViewBag.listval = taskStatus;

            ds = objCustDAL.getLocation();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    listCountry.Add(new SelectListItem
                    {
                        Text = Convert.ToString(dR["LOCAL_NAME"]),
                        Value = Convert.ToString(dR["DB_ID"])

                    });

                }
                foreach (DataRow dR in ds.Tables[1].Rows)
                {
                    listStates.Add(new SelectListItem
                    {
                        Text = Convert.ToString(dR["LOCAL_NAME"]),
                        Value = Convert.ToString(dR["DB_ID"])

                    });

                }
                foreach (DataRow dR in ds.Tables[2].Rows)
                {
                    listCities.Add(new SelectListItem
                    {
                        Text = Convert.ToString(dR["LOCAL_NAME"]),
                        Value = Convert.ToString(dR["DB_ID"])

                    });

                }
                ViewBag.listCO = listCountry;
                ViewBag.listST = listStates;
                ViewBag.listCi = listCities;
            }

            int id =Convert.ToInt32(TempData["Userid"]) ;
            CustomerModel objCust = new CustomerModel();
            if (id == 0)
            {
                objCust.btnValue = "Save";
            }
            else
            {
                objCust.btnValue = "Update";
                DataTable dt = new DataTable();
                dt = objCustDAL.FetchRecord(id);
               if(dt.Rows.Count> 0)
                {
                    //objCust.id = Convert.ToInt32(dt.Rows[0]["id"]);
                    //objCust.firstname = Convert.ToString(dt.Rows[0]["FirstName"]);
                    //objCust.lastname = Convert.ToString(dt.Rows[0]["lastname"]);
                    //objCust.contact = Convert.ToString(dt.Rows[0]["phone"]);
                    //objCust.City = Convert.ToString(dt.Rows[0]["City"]);
                    //objCust.country = Convert.ToString(dt.Rows[0]["country"]);

                }
            }

            return View(objCust);
        }




        [SessionFilter.SessionExpireFilter]
        public JsonResult EditCustomerDetails(string id)
        {
            TempData["Userid"] = id;
            

            return Json(JsonRequestBehavior.AllowGet);
        }

        [SessionFilter.SessionExpireFilter]
        public ActionResult DeleteCustomerDetails(string id)
        {
            string[] confirm = objCustDAL.deleteRecord(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        [SessionFilter.SessionExpireFilter]
        public ActionResult NewCustomer(int id,string FirstName, string LastName,string Contact, string City, string Country, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            
            string[] Result = objCustDAL.SaveRecord(id,FirstName, LastName, Contact, City, Country);
            if(Result[0]=="Saved")
            {
                return Json(Result);
            }
            else if (Result[0] == "Updated")
            {
                return Json(Result);
            }

            return RedirectToAction("ViewCustomer","Customer");
            
            
        }
        [SessionFilter.SessionExpireFilter]
        public ActionResult getCustomerList(jQueryDataTableParamModel param )
        {
            List<CustomerModel> listdetails = new List<CustomerModel>();
           

            var StudentList = objCustDAL.getAllTableDetails();
            TempData["Data"] = StudentList;

            

            IEnumerable<CustomerModel> filteredItems;
            
            if (!string.IsNullOrEmpty(param.sSearch))
            {
               
                var isRetSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isStoreInvSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isFromStoreSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                filteredItems = StudentList
                  .Where(c => isRetSearchable && c.firstName.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filteredItems = StudentList;
            }

            var isRetSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isStoreInvSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isFromStoreSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CustomerModel, string> orderingFunction = (c => sortColumnIndex == 2 && isRetSortable ? c.firstName :
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

            
            var displayedCompanies = filteredItems.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] {Convert.ToString(c.id), c.firstName, c.lastName, Convert.ToString(c.mobileNo),c.houseNo,c.blockNo,c.lastLoginDate, Convert.ToString(c.id) };



            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = StudentList.Count(),
                iTotalDisplayRecords = filteredItems.Count(),
                aaData = result,

            },
                        JsonRequestBehavior.AllowGet);
            
        }
        
    }
}