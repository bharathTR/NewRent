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

    public class LoginController : Controller
    {
        LoginModel objLogin = new LoginModel();
        // GET: Login
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginModel logObj)
        {
            var user = logObj.userName;
            var password = logObj.password;
            DataTable res = new DataTable(); ;
            LoginDAL logDAL = new LoginDAL();
            res = logDAL.Check(user, password);
            string userName = string.Empty;
            string pass = string.Empty;
            string LoginID = null;
            string permission = null;
            foreach (DataRow dR in res.Rows)
            {

                userName = Convert.ToString(dR["userName"]);
                pass = Convert.ToString(dR["password"]);
                LoginID = Convert.ToString(dR["LOGINID"]);
                permission = Convert.ToString(dR["Permissionid"]);

            }
            if (userName == user && pass == password)
            {
                Session["userName"] = user;
                Session["LOGINID"] = LoginID;
                Session["userType"] = permission;
                return RedirectToAction("Index", "Dashboard");
            }
            else {
                Session["userName"] = null;
                Session["LOGINID"] = null;
                return RedirectToAction("Login", "Login");
            }
           

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult RegisterUser()
        {
            return View();
        }
       
    }

}
