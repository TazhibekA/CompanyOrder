using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOrders
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - New employee");
                Console.WriteLine("3 - Exit");
                Console.Write("Choose: ");

                string choose = Console.ReadLine();
                int chooseInt;

                bool success = Int32.TryParse(choose, out chooseInt);
                int IDCustomer = 0;
                int IDDepartment = 0;
                if (success)
                {
                    switch (chooseInt)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Enter name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter lastname: ");
                            string lastName = Console.ReadLine();

                            var departmentList = company.Departments.ToList();
                            foreach (var department in departmentList)
                            {
                                Console.WriteLine("{0}. {1}", department.Id, department.Name);
                            }
                            Console.WriteLine("Choose department: ");
                            string chooseD = Console.ReadLine();
                            int chooseDInt;

                            bool successD = Int32.TryParse(chooseD, out chooseDInt);
                            if (successD && chooseDInt <= departmentList.Count)
                            {
                                if (CheckEmployee(company, name, lastName, chooseDInt))
                                {
                                    bool inCorrect = true;
                                    while (inCorrect)
                                    {
                                        IDCustomer = GetIdEmployee(company, name, lastName, chooseDInt);

                                        Console.Clear();
                                        Console.WriteLine("1 - Perform task");
                                        Console.WriteLine("2 - Give task");
                                        Console.WriteLine("3 - All tasks");
                                        Console.WriteLine("4 - All completed tasks");
                                        Console.WriteLine("5 - Back to menu");
                                        string chooseT = Console.ReadLine();
                                        int chooseTInt;

                                        bool successT = Int32.TryParse(chooseT, out chooseTInt);
                                        switch (chooseTInt)
                                        {
                                            case 1:
                                                var orders = company.Tasks.ToList();
                                                int count = 0;
                                                foreach (var order in orders)
                                                {
                                                    if (order.DepartmentId == chooseDInt && order.Completed == false)
                                                    {
                                                        /**/
                                                        Console.WriteLine("{0}. {1}  {2}", order.Id, order.Content, order.SendDateTime);
                                                        /**/
                                                        count++;
                                                    }
                                                }
                                                if (count == 0)
                                                {
                                                    Console.WriteLine("No orders!");
                                                    System.Threading.Thread.Sleep(1000);

                                                    break;
                                                }
                                                Console.WriteLine("Choose: ");
                                                string chooseO = Console.ReadLine();
                                                int chooseOInt;
                                                int countT = 0;
                                                bool successO = Int32.TryParse(chooseO, out chooseOInt);
                                                try
                                                {
                                                    if (successO)
                                                    {
                                                        foreach (var order in orders)
                                                        {
                                                            if (order.Id == chooseOInt )
                                                            {
                                                                order.Completed = true;
                                                            }
                                                        }
                                                       
                                                        CompletedTask completedTask = new CompletedTask
                                                        {
                                                            TaskId = chooseOInt,
                                                            CompletedDateTime = DateTime.Now
                                                            
                                                        };
                                                        company.CompletedTasks.Add(completedTask);
                                                      
                                                        company.SaveChanges();
                                                        countT++;


                                                    }

                                                }
                                                catch (Exception exc)
                                                {
                                                    Console.WriteLine(exc.Message);
                                                }

                                                break;
                                            case 2:


                                                Console.WriteLine("Enter text: ");
                                                string contentOrder = Console.ReadLine();

                                                foreach (var department in departmentList)
                                                {
                                                    Console.WriteLine("{0}. {1}", department.Id, department.Name);
                                                }

                                                Console.WriteLine("Choose department: ");
                                                string chooseDep = Console.ReadLine();
                                                int chooseDepInt;


                                                bool successDep = Int32.TryParse(chooseDep, out chooseDepInt);
                                                if (successDep && chooseDepInt <= departmentList.Count)
                                                {
                                                    Task task = new Task
                                                    {
                                                        DepartmentId = chooseDepInt,
                                                        Content = contentOrder,
                                                        SendDateTime = DateTime.Now,
                                                        Completed = false

                                                    };
                                                    company.Tasks.Add(task);
                                                    company.SaveChanges();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Enter number 1-{0}!", departmentList.Count);
                                                    System.Threading.Thread.Sleep(1000);
                                                }
                                                break;
                                            case 3:
                                                var allOrders = company.Tasks.ToList();
                                                int countOfOrders = 0;
                                                foreach (var order in allOrders)
                                                {
                                                    if (order.DepartmentId == chooseDInt && order.Completed == false)
                                                    {
                                                        Console.WriteLine("{0}. {1}  {2}", order.Id, order.Content, order.SendDateTime);
                                                        countOfOrders++;
                                                    }
                                                }
                                                if (countOfOrders == 0)
                                                {
                                                    Console.WriteLine("No orders!");
                                                }
                                                Console.Read();
                                                break;
                                            case 4:
                                                var completedTasks = company.CompletedTasks.ToList();
                                                var orderss = company.Tasks.ToList();
                                                int countCompTask = 0;
                                                foreach (var task in completedTasks)
                                                {
                                                    foreach (var taskItem in orderss)
                                                    {
                                                        if (task.TaskId == taskItem.Id && taskItem.DepartmentId == chooseDInt)
                                                        {
                                                            Console.WriteLine("{0}. {1} {2}", task.Id, taskItem.Content, task.CompletedDateTime);
                                                            countCompTask++;

                                                        }
                                                    }

                                                }

                                                if (countCompTask == 0)
                                                {
                                                    Console.WriteLine("No completed tasks!");
                                                    System.Threading.Thread.Sleep(1000);
                                                }
                                                Console.ReadLine();
                                                break;
                                            case 5:
                                                inCorrect = false;
                                                break;

                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect information!");
                                    System.Threading.Thread.Sleep(1000);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Enter number 1-{0}!", departmentList.Count);
                                System.Threading.Thread.Sleep(1000);
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Enter name: ");
                            string secondName = Console.ReadLine();
                            Console.WriteLine("Enter lastname: ");
                            string secondLastName = Console.ReadLine();

                            var secondDepartmentList = company.Departments.ToList();
                            foreach (var department in secondDepartmentList)
                            {
                                Console.WriteLine("{0}. {1}", department.Id, department.Name);
                            }
                            Console.WriteLine("Choose department: ");
                            string secondChooseD = Console.ReadLine();
                            int secondChooseDInt;

                            bool secondsuccessD = Int32.TryParse(secondChooseD, out secondChooseDInt);
                            if (secondsuccessD && secondChooseDInt <= secondDepartmentList.Count)
                            {
                                Employee employee = new Employee
                                {
                                    Name = secondName,
                                    LastName = secondLastName,
                                    DepartmentId = secondChooseDInt
                                };
                                company.Employees.Add(employee);
                                company.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Enter number 1-{0}!", secondDepartmentList.Count);
                                System.Threading.Thread.Sleep(1000);
                            }

                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Enter number 1-3!");
                    System.Threading.Thread.Sleep(1000);
                }
            }


        }

        public static bool CheckEmployee(Company company, string name, string lastName, int chooseDInt)
        {
            var employeers = company.Employees.ToList();
            foreach (var employee in employeers)
            {
                if (employee.Name == name && employee.LastName == lastName && employee.DepartmentId == chooseDInt)
                {
                    return true;


                }
            }
            return false;
        }

        public static int GetIdEmployee(Company company, string name, string lastName, int chooseDInt)
        {
            var employeers = company.Employees.ToList();
            foreach (var employee in employeers)
            {
                if (employee.Name == name && employee.LastName == lastName && employee.DepartmentId == chooseDInt)
                {
                    return employee.Id;


                }
            }
            return 0;
        }


    }
}
