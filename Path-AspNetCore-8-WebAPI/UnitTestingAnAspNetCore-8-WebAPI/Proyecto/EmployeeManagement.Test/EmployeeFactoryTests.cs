﻿using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
	public class EmployeeFactoryTests : IDisposable
	{
		private EmployeeFactory _employeeFactory;
		
		public EmployeeFactoryTests()
		{
			_employeeFactory = new EmployeeFactory();
		}

		public void Dispose()
		{
			// Este método se ejecuta después de cada prueba
			// Esto se puede utilizar para limpiar cualquier estado que se haya creado en el constructor
			//throw new NotImplementedException();
			// clean up the setup code, if required
			Console.WriteLine(_employeeFactory.GetType().Name + " is disposed.");
		}

		// CreateEmployee => la descripción de la unidad que estamos probando
		// ConstructInternalEmployee => el escenario de la prueba / para la acción que probamos
		// SalaryMustBe2500 => el resultado esperado de la prueba
		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
		{
			// Arrange

			// Act
			var employee = (InternalEmployee)_employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.Equal(2500, employee.Salary);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
		{
			// Arrange			

			// Act
			var employee = (InternalEmployee)_employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "Salary not in acceptable range.");
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
		{
			// Arrange

			// Act
			var employee = (InternalEmployee)_employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.True(employee.Salary >= 2500);
			Assert.True(employee.Salary <= 3500);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
		{
			// Arrange

			// Act
			var employee = (InternalEmployee)_employeeFactory.CreateEmployee("John", "Doe");

			// Assert
			Assert.InRange(employee.Salary, 2500, 3500);
		}

		[Fact]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
		{
			// Arrange

			// Act
			var employee = (InternalEmployee)_employeeFactory.CreateEmployee("John", "Doe");
			employee.Salary = 2500.123m;

			// Assert
			Assert.Equal(2500, employee.Salary, 0);
		}

		[Fact]
		public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
		{
			// Arrange

			// Act
			var employee = _employeeFactory.CreateEmployee("John", "Doe", "Marvin", isExternal: true);

			// Assert
			Assert.IsType<ExternalEmployee>(employee);
			//Assert.IsAssignableFrom<Employee>(employee);
		}		
	}
}
