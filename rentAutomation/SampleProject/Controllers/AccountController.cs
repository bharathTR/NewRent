using SampleProject.DAL;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SampleProject.Controllers
{

    public class AccountController : Controller
    {
        LoginModel objLogin = new LoginModel();
        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
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
            string returnUrl = "~/Account/Login";
            string ApartmentID = null;
            if (ModelState.IsValid)
            {



                foreach (DataRow dR in res.Rows)
                {

                        userName = Convert.ToString(dR["userName"]);
                        pass = Convert.ToString(dR["password"]);
                        LoginID = Convert.ToString(dR["LOGINID"]);
                        permission = Convert.ToString(dR["Permissionid"]);
                        ApartmentID = Convert.ToString(dR["H_Apartment_ID"]);






                        if (userName != user && pass != password)
                        {

                            Session["userName"] = null;
                            Session["LOGINID"] = null;
                            Session["userType"] = null;
                            Session["ApartmentID"] = null;


                            ModelState.AddModelError("ErrorMessage", "The user name or password provided is incorrect.");


                        }
                        else
                        {
                            //if (/*Url.IsLocalUrl(returnUrl) && */returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            //                 && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                            //{
                            Session["userName"] = user;
                            Session["LOGINID"] = LoginID;
                            Session["userType"] = permission;
                            Session["ApartmentID"] = ApartmentID;

                            FormsAuthentication.SetAuthCookie(userName, false);

                            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(20), false, LoginID);
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            HttpContext.Response.Cookies.Add(authCookie);


                            return RedirectToAction("Index", "Dashboard");
                            ////}
                        }
                    
                }

                if (res.Rows.Count == 0)
                {

                    ModelState.AddModelError("ErrorMessage", "The user name or password provided is incorrect.");
                }


            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "The user name or password provided is incorrect.");
            }

        
            return View(logObj);

        }
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

    }

}
