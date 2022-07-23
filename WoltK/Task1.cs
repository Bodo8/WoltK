using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WoltK
{
    public class Task1
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public Task1(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }

        public Guid CreateEmployee(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Value cannot be null or empty", nameof(lastName));

            var employee = _employeeFactory.Create(lastName);
            _employeeRepository.Insert(employee);

            return employee.Id;
        }
    }

    
    public interface IEmployeeFactory
    {
        IEmployee Create(string lastName);
    }

    public interface IEmployeeRepository
    {
        void Insert(IEmployee employee);
    }

    public interface IEmployee
    {
        Guid Id { get; }
    }

    public class Employee : IEmployee
    {
        public Guid Id { get; private set; }

        public Employee()
        {
            Id = Guid.NewGuid();
        }
    }

    public class EmployeeFactory : IEmployeeFactory
    {
        public IEmployee Create(string lastName)
        {
            Employee employee = new Employee();

            return employee;
        }
    }
}
