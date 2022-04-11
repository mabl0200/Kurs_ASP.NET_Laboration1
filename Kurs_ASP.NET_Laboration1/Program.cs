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
            /* Använde dessa metoder i början för att lägga in lite data i tabellerna genom koden */

            //AddEmployee(); //Metod för att lägga till anställd i databasen
            //AddLeaveType(); //Metod för att lägga till ledighetstyp i databasen
            //AddLeaveApplication(); //Metod för att lägga till ledighetsansökan i databasen (har justerats)

            Console.Title = "Ledighetsansökan";
            List<Employee> employees = new List<Employee>();
            List<LeaveApplication> leaveApplications = new List<LeaveApplication>();
            List<LeaveType> leaveTypes = new List<LeaveType>();
            Employee chosenEmployee = new Employee();
            
            employees = GetEmployees();
            leaveTypes = GetLeaveTypes();
            leaveApplications = GetLeaveApplications(employees, leaveTypes);
            
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.WriteLine("Vem är du och vad vill du göra?");
                Console.WriteLine("1: Anställd som vill ansöka om ledighet");
                Console.WriteLine("2: Administratör som vill hämta ansökningar");
                Console.WriteLine("3: Avsluta");
                int choice = GetInput();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Ange ditt anställningsnummer för att ansöka om ledighet: ");
                        PrintEmployees(employees);
                        int chosenNumber = GetInput();
                        Console.Clear();
                        chosenEmployee = GetSpecificEmployee(employees, chosenNumber);
                        CreateApplication(chosenEmployee, leaveTypes);
                        break;

                    case 2:
                        bool looping = true;
                        while (looping)
                        {
                            Console.WriteLine("Välj vad du som administratör vill göra: ");
                            Console.WriteLine("1: Hämta ansökningar från en specifik person");
                            Console.WriteLine("2: Hämta alla ansökningar");
                            int val = GetInput();
                            Console.Clear();
                            if (val == 1)
                            {
                                Console.WriteLine("Ange anställningsid för den person vars ledighetsansökningar du vill se");
                                PrintEmployees(employees);
                                int employId = GetInput();
                                chosenEmployee = GetSpecificEmployee(employees, employId);
                                Console.Clear();

                                //Skriver ut alla ledighetsansökningar från vald person
                                foreach (var item in leaveApplications)
                                {
                                    if (chosenEmployee.EmployeeId == item.EmployeeId)
                                    {
                                        PrintResult(item);
                                    }
                                }
                                Console.WriteLine("Tryck Enter för att återgå till startsidan");
                                Console.ReadLine();
                                Console.Clear();
                                looping = false;
                            }
                            if (val == 2)
                            {
                                Console.WriteLine("Ange vilken månad (MM) du vill se: ");
                                int chosenMonth = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Alla ledighetsansökningar: ");
                                foreach (var item in leaveApplications)
                                {
                                    if (item.ApplyDate.Month == chosenMonth)
                                    {
                                        PrintResult(item);
                                    }
                                }
                                Console.WriteLine("Tryck Enter för att återgå till startsidan");
                                Console.ReadLine();
                                Console.Clear();
                                looping = false;
                            }
                        }
                        break;
                    case 3:
                        keepLooping = false;
                        break;
                    default:
                        Console.WriteLine("Oj något blev fel, testa igen");
                        break;
                }
            }
        }
        public static void CreateApplication(Employee chosenEmployee, List<LeaveType> leaveTypes) //Hämtar all info för ny ledighetsansökan för att sedan skicka den till metod som lägger till ansökningar i databasen
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                LeaveType chosenLeaveType = new LeaveType();
                Console.WriteLine("Ange nummer för önskad ledighetstyp: ");

                //Skriv ut en lista med alla sortes typer av ledighetsansökningar
                foreach (var item in leaveTypes)
                {
                    Console.WriteLine($"Id: {item.LeaveTypeID} Namn: {item.LeaveTypeName}");
                }
                int chosenLeaveTypeNr = GetInput();
                Console.Clear();

                //Hitta vilken typ som blev vald
                for (int i = 0; i < leaveTypes.Count; i++)
                {
                    if (leaveTypes[i].LeaveTypeID == chosenLeaveTypeNr)
                    {
                        chosenLeaveType = leaveTypes[i];
                    }
                }
                //Input för start- och slutdatum
                Console.WriteLine($"Ange startdatum: YYYY, MM, DD");
                DateTime chosenStartDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine($"Ange slutdatum: YYYY, MM, DD");
                DateTime chosenEndDate = DateTime.Parse(Console.ReadLine());

                DateTime applyDate = DateTime.Now;
                Console.Clear();

                //Sammanställning för att se om input blev rätt
                Console.WriteLine("Stämmer detta? J eller N");
                
                Console.WriteLine(new string('-', 20));
                Console.WriteLine($"Ansökningsdag: {applyDate}  ");
                Console.WriteLine($"Anställningsnummer: {chosenEmployee.EmployeeId}");
                Console.WriteLine($"Namn: {chosenEmployee.GetFullName()}");
                Console.WriteLine($"Ledighetstyp: {chosenLeaveType.LeaveTypeName}");
                Console.WriteLine($"Startdatum: {chosenStartDate}");
                Console.WriteLine($"Slutdatum: {chosenEndDate}");
                Console.WriteLine($"Antal dagar: {(chosenEndDate - chosenStartDate).Days}");
                Console.WriteLine(new string('-', 20));
                
                string choice = Console.ReadLine().ToLower();
                if (choice == "j")
                {
                    Console.Clear();
                    AddLeaveApplication(chosenEmployee, chosenLeaveType, chosenStartDate, chosenEndDate, applyDate);
                    keepLooping = false;
                }
            }
        }
        public static Employee GetSpecificEmployee(List<Employee> employees, int chosenNumber) //Hämtar info om vald anställd
        {
            foreach (var item in employees)
            {
                if (item.EmployeeId == chosenNumber)
                {
                    return item;
                }
            }
            return null;
        }
        public static void PrintEmployees(List<Employee> employees) //Skriver ut alla anställda
        {
            Console.WriteLine(new string('-', 30));
            foreach (var item in employees)
            {
                Console.WriteLine($"Anställningsid: {item.EmployeeId} Namn: {item.GetFullName()}");
                Console.WriteLine(new string('-', 30));
            }
        }
        public static void PrintResult(LeaveApplication item) //Skriver ut lista med ledighetsansökningar
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Ansökningsdag: {item.ApplyDate}  ");
            Console.WriteLine($"Anställningsnummer: {item.Employee.EmployeeId}");
            Console.WriteLine($"Namn: {item.Employee.GetFullName()}");
            Console.WriteLine($"Ledighetstyp: {item.LeaveType.LeaveTypeName}");
            Console.WriteLine($"Startdatum: {item.StartDate}");
            Console.WriteLine($"Slutdatum: {item.EndDate}");
            Console.WriteLine($"Antal dagar: {(item.EndDate - item.StartDate).Days}");
            
        } 
        public static int GetInput() //Hämta input från användare
        {
            int choice = Int32.Parse(Console.ReadLine());
            return choice;
        }
        public static void AddEmployee() //Lägg till en slumpvis namngiven anställd
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
        public static void AddLeaveApplication(Employee employee, LeaveType leaveType, DateTime start, DateTime end, DateTime apply) //Lägg till ledighetsansökan till databas
        {
            try
            {
                using EntityBusiness context = new EntityBusiness();

                LeaveApplication leaveApplication = new LeaveApplication()
                {
                    LeaveTypeID = leaveType.LeaveTypeID,
                    EmployeeId = employee.EmployeeId,
                    StartDate = start,
                    EndDate = end,
                    ApplyDate = apply
                };
                context.Add(leaveApplication);
                context.SaveChanges();

                leaveApplication.Employee = employee;
                leaveApplication.LeaveType = leaveType;
                Console.WriteLine("Ledighetsansökan skickad!");
                PrintResult(leaveApplication);
                Console.WriteLine(new string('-', 30));

                Console.WriteLine("Tryck Enter för att återgå till startsidan");
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }
        public static void AddLeaveType() //Lägg till ledighetstyp till databas
        {
            try
            {
                using EntityBusiness context = new EntityBusiness();

                LeaveType leaveType = new LeaveType()
                {
                    LeaveTypeName = "Tjänstledighet"
                };
                context.Add(leaveType);
                context.SaveChanges();
                Console.WriteLine($"Added {leaveType.LeaveTypeID} {leaveType.LeaveTypeName}");

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }

        public static List<Employee> GetEmployees() //Hämta anställda från databas
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
        
        public static List<LeaveApplication> GetLeaveApplications(List<Employee> employees, List<LeaveType> leaveTypes) //Hämta ledighetsansökningar från databas
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
        public static List<LeaveType> GetLeaveTypes() //Hämta ledighetstyper från databas
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
