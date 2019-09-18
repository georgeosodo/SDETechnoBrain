using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeHierarchy.Domain
{
    public class Employees
    {
        public static bool ValidInput { get; set; }
        public static bool ValidSalaries { get; set; }
        public static bool ValidNumberOfCeos { get; set; }
        public static bool ValidManagers { get; set; }
        public static bool ValidNumberOfManagers { get; set; }
        public static bool ValidReference { get; set; }
        public Employees(string[] csvInput)
        {
            ValidInput = true;
            if (csvInput == null || csvInput.Length == 0)
            {
                ValidInput = false;
                return;
            }
            else
            {
                employees = Employee.Create(csvInput);
                ValidateNumberOfCEOs();
                ValidateAllManagersAreListedAsEmployees();
                ValidateEmployeeWithMoreThanOneManger();
                ValidateCyclicReference();
            }

        }

        public List<Employee> employees { get; set; }

        public Int64 GetManagersBudget(string managerId)
        {
            if (string.IsNullOrWhiteSpace(managerId)) throw new ArgumentException("Invalid Manager");
            Int64 total = 0;
            total += employees.FirstOrDefault(e => e.Id == managerId).Salary;
            foreach (var item in employees.Where(e => e.ManagerId == managerId))
            {
                if (isManager(item.Id))
                {
                    total += GetManagersBudget(item.Id);
                }
                else
                {
                    total += item.Salary;
                }
            }
            return Convert.ToInt32(total);
        }
        private bool isManager(string id) => employees.Where(e => e.ManagerId == id).Count() > 0;

        private void ValidateNumberOfCEOs()
        {
            ValidNumberOfCeos = true;
            if (employees.Where(e => e.ManagerId == string.Empty).Count() > 1)
                ValidNumberOfCeos = false;                
                
        }
               

        private void ValidateAllManagersAreListedAsEmployees()
        {
            
            ValidManagers = true;
            var result = employees.Where(m => m.Id=="" || employees.FirstOrDefault(e => e.Id == m.ManagerId) == null).Count();           
            if (result > 1)
            {
                ValidManagers = false;             
                
            }
            
        } 
        private void ValidateEmployeeWithMoreThanOneManger()
        {
            ValidNumberOfManagers = true;
            foreach (var _ in from employee in employees.Where(e => e.Id.Count() > 1)
                              let countManagers = employees.Where(e => e.Id == employee.Id && e.ManagerId != employee.Id).Count()
                              where countManagers > 1
                              select new { })
            {
                ValidNumberOfManagers = false;
            }
        }

        private void ValidateCyclicReference()
        {
            ValidReference = true;
            foreach (var _ in from employee in employees
                              where employees.Where(e => e.ManagerId == employee.Id && employee.ManagerId == e.Id).Count() > 0
                              select new { })
            {
                ValidReference = false;
            }
        }



        //private void ValidateCyclicReference()
        //{
        //    foreach (var _ in from employee in employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
        //                      let manager = employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
        //                .FirstOrDefault(e => e.Id == employee.ManagerId)
        //                      where manager != null
        //                      where manager.ManagerId == employee.Id
        //                      select new { })
        //    {
        //        throw new ArgumentException("Cyclic reference found");
        //    }
        //}
    }
}
