using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Reg_User.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;

namespace MVC_Reg_User.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginClass lc)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select EmailAddress,Password from [dbo].[MVCregUser] where EmailAddress=@EmailAddress and Password=@Password";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@EmailAddress", lc.EmailAddress);
            sqlcomm.Parameters.AddWithValue("@Password",lc.Password);
            SqlDataReader sqr = sqlcomm.ExecuteReader();
            if(sqr.Read())
            {
                FormsAuthentication.SetAuthCookie(lc.EmailAddress, true);
                Session["emailid"] = lc.EmailAddress.ToString();
                return RedirectToAction("welcome","Login");
            }
            else
            {
                ViewData["message"] = "Username & Password are wrong !";
            }
            sqlconn.Close();
            return View(); 
        }
        public ActionResult welcome()
        {
            return View();
        }
    }
}