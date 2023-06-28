using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Reg_User.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace MVC_Reg_User.Controllers
{
    public class NewRegController : Controller
    {
        // GET: NewReg
        public ActionResult Index()
        {
            return View();
        }

        // Post: NewReg
        [HttpPost]
        public ActionResult Index(UserClass uc,HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand sqlCommand = new SqlCommand("sp_InsertMVCregUser", sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
           
            sqlCommand.Parameters.AddWithValue("@FirstName", uc.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", uc.LastName);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", uc.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Gender", uc.Gender);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", uc.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", uc.EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Address", uc.Address);
            sqlCommand.Parameters.AddWithValue("@State", uc.State);
            sqlCommand.Parameters.AddWithValue("@City", uc.City);
            sqlCommand.Parameters.AddWithValue("@Username", uc.Username);
            sqlCommand.Parameters.AddWithValue("@Password", uc.Password);
            sqlCommand.Parameters.AddWithValue("@ConfirmPassword", uc.ConfirmPassword);

            if (file!= null && file.ContentLength>0)
            {

                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/User-Images/"),filename);
                file.SaveAs(imgpath);
                sqlCommand.Parameters.AddWithValue("@Uimg", "~/User-Images/" + filename);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Uimg", DBNull.Value);
            }
            sqlconn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlconn.Close();

            ViewData["Message"] = "User Record " + uc.FirstName + " Is Saved Successfully!";
            return View();


        }
    }
}