﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Employee
    {
       
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Address { get; set; }

        public string Gender { get; set; }


        [ForeignKey("EmployeeID")]
        public ICollection<Schedule> schedules { get; set; }

       
    }
}
