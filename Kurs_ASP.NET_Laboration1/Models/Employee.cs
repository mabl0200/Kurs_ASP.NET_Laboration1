using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kurs_ASP.NET_Laboration1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }

        public string GetFullName()
        {

            return $"{FirstName} {LastName}";
        }
    }
}
