using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cumulative_1.Models;

namespace Cumulative_1.Controllers
{
    public class TeachersController : Controller
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string name)
        {
                MySqlConnection Conn = School.AccessDatabase();
                Conn.Open();
                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT * from teachers";
                MySqlDataReader ResultSet = cmd.ExecuteReader();
                IList<Teacher> teachers = new List<Teacher> { };

                while (ResultSet.Read())
                {
                    //Access Column information by the DB column name as an index
                    Teacher teacher = new Teacher
                    {
                        TeacherId = (int)ResultSet["teacherid"],
                        Name = (string)ResultSet["teacherfname"] + (string)ResultSet["teacherlname"],
                        Hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                        Salary = (Decimal)ResultSet["salary"]

                    };
                    teachers.Add(teacher);
                }

                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

            //Return the final list of teachers
            ViewBag.Message = "Hello " + name;
            ViewBag.Teachers = teachers;
            return View();
        }


        public ActionResult Show(int teacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * from teachers WHERE teacherid=?teacherid";
            cmd.Parameters.Add(new MySqlParameter("teacherid", teacherId));
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Teacher teacherData = new Teacher { };
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                teacherData = new Teacher
                {
                    TeacherId = (int)ResultSet["teacherid"],
                    Name = (string)ResultSet["teacherfname"] + (string)ResultSet["teacherlname"],
                    Hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                    Salary = (Decimal)ResultSet["salary"]

                };
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            ViewBag.singleTeacher = teacherData;
            return View();
        }

    }


}