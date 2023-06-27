using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Services
{
    public class EmployeeService
    {
        private static EmployeeService? instance;
        private static object _lock = new object();
        private List<Employee> employees;
        public static EmployeeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeService();//how to make this work with a parameter
                    }
                }

                return instance;
            }
        }

     

        public List<Employee> Employees { get { return employees; } }
        public EmployeeService()
        {
            employees = new List<Employee>
            {
                new Employee {Id = 1, Name = "Admin"},
                new Employee {Id = 2, Name = "Test"}
            };
        }

        public void AddOrUpdate(Employee e)
        {
            //null check fixes exception
            var check = e;
            if( e != null)
            {
                if (e.Id == 0)
                {
                    e.Id = LastId + 1;
                    Employees.Add(e);
                }
            }
        }

        public void RemoveEmployee(int id)
        {
            var employeeToRemove = employees.FirstOrDefault(employee => employee.Id == id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }


        public Employee? Get(int id)
        {
            return employees.FirstOrDefault(employee => employee.Id == id);
        }
        private int LastId
        {
            get
            {
                return Employees.Any() ? Employees.Select(e => e.Id).Max() : 0;
            }
        }
    }
}
