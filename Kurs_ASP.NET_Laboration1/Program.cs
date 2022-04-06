using Kurs_ASP.NET_Laboration1.Models;
using System;
using System.Collections.Generic;

namespace Kurs_ASP.NET_Laboration1
{
    class Program
    {
        static void Main(string[] args)
        {
            AddEmployee(); //Metod för att lägga till anställd i databasen
        }
        public static void AddEmployee()
        {
            //Generate a random name
            List<string> FirstNames = new List<string>() { "Anna", "Frans", "Therese", "Sune", "Sara", "Björn", "Sigrid", "Theodor", "Ellen", "Henrik", "Linnea", "Oliver" };
            List<string> LastNames = new List<string>() { "Andersson", "Frisk", "Skog", "Eriksson", "Nordström", "Blank", "Blom", "Öberg", "Sjögren", "Eliasson", "Holm", "Berg" };
            int randomFirstName = new Random().Next(0, 12);
            int randomLastName = new Random().Next(0, 12);

            //Add employees to db
            using EntityBusiness context = new EntityBusiness();

            Employee employee = new Employee()
            {
                FirstName = FirstNames[randomFirstName],
                LastName = LastNames[randomLastName]
            };

            context.Add(employee);
            context.SaveChanges();
            Console.WriteLine($"Added {employee.FirstName} {employee.LastName}");
        }

    }
}
