using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthentication.Models;

namespace UserAuthentication.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-FH1K8VR\SHIHAB; Initial Catalog = University; Integrated Security=True";
        // GET: Login
        public ActionResult Index()
        {
            var dtblUser = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM login", sqlCon);
                sqlDa.Fill(dtblUser);
            }
            return View(dtblUser);
        }

        public ActionResult Create()
        {
            var user = new User();

            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into login values (@name, @password)";
                var sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@name", user.Name);
                sqlCmd.Parameters.AddWithValue("@password", user.Password);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var dtblUser = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from login where name = @name";
                var sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@name", id);
                sqlDa.Fill(dtblUser);
            }

            if (dtblUser.Rows.Count == 1)
            {
                var user = new User()
                {
                    Name = dtblUser.Rows[0][0].ToString(),
                    Password = dtblUser.Rows[0][1].ToString()
                };

                return View(user);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Update login set name = @name, password = @password where name = @name";
                var sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@name", user.Name);
                sqlCmd.Parameters.AddWithValue("@password", user.Password);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from login where name = @name";
                var sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@name", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}