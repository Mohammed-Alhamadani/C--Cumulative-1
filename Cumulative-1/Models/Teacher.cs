using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative_1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public DateTime Hiredate { get; set; }
        public decimal Salary { get; set; }

    }
}