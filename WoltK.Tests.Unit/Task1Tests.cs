using System;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace WoltK.Tests.Unit
{
    public class Task1Tests
    {
        IEmployeeFactory _employeeFactoryStub;
        IEmployeeRepository _employeeRepisitory;
        Task1 _sut;

        public Task1Tests()
        {
            _employeeFactoryStub = Substitute.For<IEmployeeFactory>();
            _employeeRepisitory = Substitute.For<IEmployeeRepository>();
            _sut = new Task1(_employeeFactoryStub, _employeeRepisitory);
        }

        [Fact]
        public void CreateEmployee_Should_Return_Employee()
        {
            //arrange
            string lastName = "testName";
            Employee employee = new Employee();
            _employeeFactoryStub.Create(lastName).Returns(employee);

            //act
            Guid actual = _sut.CreateEmployee(lastName);

            //assert
            actual.Should().Be(employee.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CreateEmployee_Should_Throw_ArgumentException(string name)
        {
            //arrange
            string lastName = name;
            Employee employee = new Employee();
            _employeeFactoryStub.Create(lastName).Returns(employee);

            //act
            Action actual = () => _sut.CreateEmployee(lastName);

            //assert
            actual.Should().ThrowExactly<ArgumentException>()
                .WithMessage("Value cannot be null or empty (Parameter 'lastName')");
        }
    }
}