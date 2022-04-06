using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kurs_ASP.NET_Laboration1.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeID { get; set; }
        [Required]
        public string LeaveTypeName { get; set; }
    }
}
