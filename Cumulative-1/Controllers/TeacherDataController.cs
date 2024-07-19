using System;
using System.Collections.Generic;
using System.Web.Http;
using Cumulative_1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative_1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Teacher> Listteachers()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * from teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            IList<Teacher> teachers = new List<Teacher> {};
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Teacher teacher = new Teacher {
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
            return teachers;
        }
    }
}

