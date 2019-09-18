using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeHierarchy.Domain
{
    public class Employee
    {
        public string Id { get; set; }
        public string ManagerId { get; set; }
        public Int16 Salary { get; set; }
        
        private Employee(string id, string managerId, Int16 salary)
        {         
           
            Id = id;
            ManagerId = managerId;
            Salary = salary;
        }
        public static List<Employee> Create(string[] CsvInput)
        {
            Employees.ValidSalaries = true;
            List<Employee> employees = new List<Employee>();
            foreach (var employeeInput in CsvInput )
            {
                var values = employeeInput.Split(',');
                Int16 EmployeeSalary;
               
               if(!Int16.TryParse(values[2], out EmployeeSalary) || EmployeeSalary < 0)               
                    Employees.ValidSalaries = false;
                                                
                employees.Add(new Employee(values[0], values[1], EmployeeSalary));
            }

            return employees;
        }
    }
}
