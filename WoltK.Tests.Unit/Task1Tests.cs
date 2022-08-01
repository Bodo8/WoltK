using System;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace WoltK.Tests.Unit
{
    public class Task1Tests
    {
        IEmployeeFactory _employeeFactoryMock;
        IEmployeeRepository _employeeRepisitoryMock;
        Task1 _sut;

        public Task1Tests()
        {
            _employeeFactoryMock = Substitute.For<IEmployeeFactory>();
            _employeeRepisitoryMock = Substitute.For<IEmployeeRepository>();
            _sut = new Task1(_employeeFactoryMock, _employeeRepisitoryMock);
        }

        [Fact]
        public void CreateEmployee_Should_Return_Employee()
        {
            //arrange
            string lastName = "testName";
            Employee employee = new Employee();
            _employeeFactoryMock.Create(lastName).Returns(employee);

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
            Employee employee = new Employee();
            _employeeFactoryMock.Create(name).Returns(employee);

            //act
            Action actual = () => _sut.CreateEmployee(name);

            //assert
            actual.Should().ThrowExactly<ArgumentException>()
                .WithMessage("Value cannot be null or empty (Parameter 'lastName')");
        }
    }
}