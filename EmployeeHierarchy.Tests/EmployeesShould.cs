using EmployeeHierarchy.Domain;
using System;
using Xunit;
using Shouldly;

namespace EmployeeHierarchy.Tests
{
    public class EmployeesShould
    {
        [Fact]
        public void ReturnTrueIfValidInputString()
        {
            string employee1 = "Employee2,Employee1,2";
            string employee2 = "Employee2,Employee1,500";           
            string[] CsvInput = { employee1, employee2 };              
            var employees = new Employees(CsvInput);
            Assert.True(Employees.ValidInput);
           
        }
        [Fact]
        public void ReturnTrueIfSalariesAreValidIntegers()
        {
            string employee1 = "Employee2,Employee1,2";
            string employee2 = "Employee2,Employee1,500";
            //Uncomment for invalid integer test
            //employee1 = "Employee2,Employee1,-200";
            //employee2 = "Employee2,Employee1,";

            string[] CsvInput= { employee1, employee2 };            
            Employee.Create(CsvInput);
            Assert.True(Employees.ValidSalaries);                       
           
        }

        [Fact]
        public void ReturnTrueIfThereIsOnlyOneCEO()
        {
            string employee1 = "Employee2,Employee3,200";
            string employee2 = "Employee1,,500";
            //Uncomment for failing test
            //employee1 = "Employee5,,500";
            string[] CsvInput = { employee1, employee2 };
            var employees = new Employees(CsvInput);
            Assert.True(Employees.ValidNumberOfCeos);
            
        }

        [Fact]
        public void ReturnTrueIfManagerListedAsEmployee()
        {
            string employee1 = "Employee2,Employee3,200";
            string employee2 = "Employee3,,500";
            //Uncomment for failing test
            // employee2 = ",,500";
            //employee2 = "Employee5,Employee7,500";
            string[] CsvInput = { employee1, employee2 };
            var employee = new Employees(CsvInput);
            Assert.True(Employees.ValidManagers);            
        }

        [Fact]
        public void ReturnTrueIfEmployeesHaveOneManager()
        {
            string employee1 = "Employee2,Employee3,200";
            string employee2 = "Employee3,Employee4,500";
            //Uncomment for failing test
            //employee2 = "Employee2,Employee4,500";
            string[] CsvInput = { employee1, employee2 };
            var employees = new Employees(CsvInput);
            Assert.True(Employees.ValidNumberOfManagers);           
        }

        [Fact]
        public void ReturmTrueIfEmployeesHaveNoCyslicReference()
        {
            string employee1 = "Employee2,Employee3,200";
            string employee2 = "Employee5,Employee2,500";
            //Uncomment for failing test
            //employee2 = "Employee3,Employee2,500";
            string[] CsvInput = { employee1, employee2 };
            var employees = new Employees(CsvInput);
            Assert.True(Employees.ValidReference);
            
        }

        [Fact]
        public void ReturnCorrectManagerBudget()
        {
            string employee1 = "Employee2,,800";
            string employee2 = "Employee1,Employee2,500";
            string employee3 = "Employee3,Employee1,100";
            string[] CsvInput = { employee1, employee2, employee3 };
            Int64 ExpectedBudget=1400;
            Int64 TotalBudget;
            Employees employees = new Employees(CsvInput);
            TotalBudget = employees.GetManagersBudget("Employee2");
            Assert.Equal(ExpectedBudget, TotalBudget);
                      
        }


    }
}
