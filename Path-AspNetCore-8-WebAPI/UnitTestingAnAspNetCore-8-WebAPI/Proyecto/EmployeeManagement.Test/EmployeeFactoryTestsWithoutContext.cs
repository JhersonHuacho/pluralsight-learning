﻿using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
	public class EmployeeFactoryTestsWithoutContext
	{
		// CreateEmployee => la descripción de la unidad que estamos probando
		// ConstructInternalEmployee => el escenario de la prueba / para la acción que probamos
		// SalaryMustBe2500 => el resultado esperado de la prueba
		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory(); 

			// Act
			var employee = (InternalEmployee)employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.Equal(2500, employee.Salary);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory();

			// Act
			var employee = (InternalEmployee)employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "Salary not in acceptable range.");
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory();

			// Act
			var employee = (InternalEmployee)employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.True(employee.Salary >= 2500);
			Assert.True(employee.Salary <= 3500);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory();

			// Act
			var employee = (InternalEmployee)employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.InRange(employee.Salary, 2500, 3500);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory();

			// Act
			var employee = (InternalEmployee)employeeFactory.CreateEmployee("John", "Doe");
			employee.Salary = 2500.123m;

			// Assert
			Assert.Equal(2500, employee.Salary, 0);
		}

		[Fact]
		public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
		{
			// Arrange
			var employeeFactory = new EmployeeFactory();

			// Act
			var employee = employeeFactory.CreateEmployee("John", "Doe", "Marvin", isExternal: true);

			// Assert
			Assert.IsType<ExternalEmployee>(employee);
			//Assert.IsAssignableFrom<Employee>(employee);
		}
	}
}
