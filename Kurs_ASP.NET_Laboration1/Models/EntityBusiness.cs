using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurs_ASP.NET_Laboration1.Models
{
    class EntityBusiness : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-59ADV49\\SQLEXPRESS; Initial Catalog = EntityBusiness; Integrated Security = True;");
        }
    }
}
