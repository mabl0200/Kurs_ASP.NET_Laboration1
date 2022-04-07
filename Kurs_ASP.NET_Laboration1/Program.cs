using Kurs_ASP.NET_Laboration1.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Kurs_ASP.NET_Laboration1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            List<LeaveApplication> leaveApplications = new List<LeaveApplication>();
            List<LeaveType> leaveTypes = new List<LeaveType>();
            employees = GetEmployees();
            leaveTypes = GetLeaveTypes();
            leaveApplications = GetLeaveApplications(employees, leaveTypes);
            
            Employee chosenEmployee = new Employee();
            LeaveType chosenLeaveType = new LeaveType();


            Console.Title = "Ledighetsansökan";
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.WriteLine("Hej! Vem är du och vad vill du göra?");
                Console.WriteLine("1: Anställd som vill ansöka om ledighet");
                Console.WriteLine("2: Administratör som vill hämta ansökningar");
                int choice = Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Du är anställd som vill ansöka om ledighet");
                        Console.WriteLine("Ange ditt nummer: ");
                        foreach (var item in employees)
                        {
                            Console.WriteLine($"{item.EmployeeId} {item.GetFullName()}");
                        }
                        int chosenNumber = Int32.Parse(Console.ReadLine());
                        Console.Clear();
                        //int chosenEmployeeId = 0;
                        for (int i = 0; i < employees.Count; i++)
                        {
                            if (employees[i].EmployeeId == chosenNumber)
                            {
                                chosenEmployee = employees[i];
                                Console.WriteLine($"Chosen employee:{chosenEmployee.EmployeeId} {chosenEmployee.GetFullName()}");
                                //chosenEmployeeId = employees[i].EmployeeId;
                                //Console.WriteLine($"Chosen employee: {chosenEmployee.EmployeeId} {chosenEmployee.GetFullName()}");
                            }
                        }
                        Console.WriteLine("Välj ledighetstyp: ");
                        foreach (var item in leaveTypes)
                        {
                            Console.WriteLine($"Id: {item.LeaveTypeID} Namn: {item.LeaveTypeName}");
                        }
                        int chosenLeaveTypeNr = Int32.Parse(Console.ReadLine());
                        Console.Clear();
                        for (int i = 0; i < leaveTypes.Count; i++)
                        {
                            if (leaveTypes[i].LeaveTypeID == chosenLeaveTypeNr)
                            {
                                chosenLeaveType = leaveTypes[i];
                                Console.WriteLine($"Ansöka om ledigthet för {chosenLeaveType.LeaveTypeName}");
                            }
                        }
                        
                        Console.WriteLine($"Ange startdatum: YYYY, MM, DD");
                        DateTime chosenStartDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine(chosenStartDate);
                        Console.WriteLine($"Ange slutdatum: YYYY, MM, DD");
                        DateTime chosenEndDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine(chosenEndDate);
                        DateTime applyDate = DateTime.Now;
                        Console.Clear();
                        Console.WriteLine("Stämmer detta?");
                        Console.WriteLine($"Anställdnr: {chosenEmployee.EmployeeId} {chosenEmployee.GetFullName()} söker ledigt för {chosenLeaveType.LeaveTypeName} med start: {chosenStartDate} och slut: {chosenEndDate}. Ansökan skickad: {applyDate} ");
                        AddLeaveApplication(chosenLeaveType.LeaveTypeID, chosenEmployee.EmployeeId, chosenStartDate, chosenEndDate, applyDate);

                        break;
                    case 2:
                        Console.WriteLine("Du är administratör som vill hämta ledighetansökningar");
                        Console.WriteLine("Ange vilken månad du vill se: ");
                        int chosenMonth = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Alla ansökningar gjorda");
                        foreach (var item in leaveApplications)
                        {
                            //item.ApplyDate.Month
                            if (item.ApplyDate.Month == chosenMonth)
                            {
                                Console.WriteLine($"Namn: {item.Employee.GetFullName()} Apply day: {item.ApplyDate} Type: {item.LeaveType.LeaveTypeName}");
                            }
                        }
                        
                        break;
                    default:
                        Console.WriteLine("Oj något blev fel, testa igen");
                        break;
                }

            }
            
            //AddEmployee(); //Metod för att lägga till anställd i databasen
            //AddLeaveType(); //Metod för att lägga till ledighetstyp i databasen
            //AddLeaveApplication(); //Metod för att lägga till ledighetsansökan i databasen
        }
        public static void AddEmployee()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
            
        }
        public static void AddLeaveApplication(int leaveType, int employee, DateTime start, DateTime end, DateTime apply)
        {
            try
            {
                //Add Leave Application to db
                using EntityBusiness context = new EntityBusiness();

                LeaveApplication leaveApplication = new LeaveApplication()
                {
                    LeaveTypeID = leaveType,
                    EmployeeId = employee,
                    StartDate = start,
                    EndDate = end,
                    ApplyDate = apply

                };
                context.Add(leaveApplication);
                context.SaveChanges();
                
                Console.WriteLine($"Added on: {leaveApplication.ApplyDate} Employee: {leaveApplication.EmployeeId} Leave: {leaveApplication.LeaveTypeID} From: {leaveApplication.StartDate} To: {leaveApplication.EndDate} ");

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
            

        }
        public static void AddLeaveType()
        {
            try
            {
                //Add Leave Type to db
                using EntityBusiness context = new EntityBusiness();

                LeaveType leaveType = new LeaveType()
                {
                    LeaveTypeName = "Tjänstledighet"
                };
                context.Add(leaveType);
                context.SaveChanges();
                Console.WriteLine($"Added {leaveType.LeaveTypeID} {leaveType.LeaveTypeName}");

            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public static List<Employee> GetEmployees()
        {
            try
            {
                List<Employee> listToReturn = new List<Employee>();
                using (var db = new EntityBusiness())
                {
                    var listOfEmployees = from e in db.Employees 
                                          where e.EmployeeId > 0 
                                          select e;
                    foreach (var employee in listOfEmployees)
                    {
                        var employeeData = new Employee
                        {
                            EmployeeId = employee.EmployeeId,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName
                        };
                        listToReturn.Add(employeeData);
                        //Console.WriteLine($"{employee.EmployeeId} {employee.GetFullName()}");
                    }
                    return listToReturn;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }

        public static List<LeaveApplication> GetLeaveApplications(List<Employee> employees, List<LeaveType> leaveTypes)
        {
            try
            {
                List<LeaveApplication> listToReturn = new List<LeaveApplication>();
                using (var db = new EntityBusiness())
                {
                    var listOfApplications = from e in db.LeaveApplications
                                          where e.ApplicationId > 0
                                          select e;
                    foreach (var application in listOfApplications)
                    {
                        var applicationData = new LeaveApplication
                        {
                            ApplicationId = application.ApplicationId,
                            LeaveTypeID = application.LeaveTypeID,
                            EmployeeId = application.EmployeeId,
                            StartDate = application.StartDate,
                            EndDate = application.EndDate,
                            ApplyDate = application.ApplyDate,
                            Employee = application.Employee,
                            LeaveType = application.LeaveType
                            
                        };
                        //var listOfEmployees = from e in db.Employees
                        //                      where e.EmployeeId > 0
                        //                      select e;
                        foreach (var person in employees)
                        {
                            if (applicationData.EmployeeId == person.EmployeeId)
                            {
                                applicationData.Employee = person;
                                
                            }
                        }
                        foreach (var type in leaveTypes)
                        {
                            if (applicationData.LeaveTypeID == type.LeaveTypeID)
                            {
                                applicationData.LeaveType = type;
                            }
                        }
                        listToReturn.Add(applicationData);
                        //Console.WriteLine(applicationData.Employee.GetFullName());
                        //var emp = new LeaveApplication
                        //{
                        //    Employee = application.EmployeeId;
                        //}
                        //Console.WriteLine($"{application.ApplicationId} {application.LeaveTypeID} {application.EmployeeId} {application.StartDate} {application.EndDate} {application.ApplyDate}");
                    }
                    return listToReturn;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }

        }
        public static List<LeaveType> GetLeaveTypes()
        {
            try
            {
                List<LeaveType> listToReturn = new List<LeaveType>();
                using (var db = new EntityBusiness())
                {
                    var listOfLeaveTypes = from e in db.LeaveTypes
                                          where e.LeaveTypeID > 0
                                          select e;
                    foreach (var leaveType in listOfLeaveTypes)
                    {
                        var leaveTypeData = new LeaveType
                        {
                            LeaveTypeID = leaveType.LeaveTypeID,
                            LeaveTypeName = leaveType.LeaveTypeName
                        };
                        listToReturn.Add(leaveTypeData);
                        //Console.WriteLine($"Id: {leaveType.LeaveTypeID} Name: {leaveType.LeaveTypeName}");
                    }
                    return listToReturn;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }

        }
        
            

    }
}
