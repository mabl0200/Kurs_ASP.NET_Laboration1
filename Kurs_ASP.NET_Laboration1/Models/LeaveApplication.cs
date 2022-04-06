using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration1.Models
{
    public class LeaveApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        [Required]
        public int LeaveTypeID { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime ApplyDate { get; set; }

        public Employee Employee { get; set; }

        public LeaveType LeaveType { get; set; }
        
    }
}
