using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test
{
	public class DataDrivenEmployeeServiceTestsWithoutCollectionFixture : IClassFixture<EmployeeServiceFixture>	
    {
		private readonly EmployeeServiceFixture _employeeServiceFixture;

		public DataDrivenEmployeeServiceTestsWithoutCollectionFixture(EmployeeServiceFixture employeeServiceFixture)
		{
			_employeeServiceFixture = employeeServiceFixture;
		}

		[Fact]
		public async Task GiveRaise_MinimunRaiseGiven_EmployeeMinimunRaiseGivenMustBeTrue()
		{
			// Arrange

			var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 5, 3000, false, 1);

			// Act
			await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 100);

			// Assert
			Assert.True(internalEmployee.MinimumRaiseGiven);
		}

		[Fact]
		public async Task GiveRaise_MoreThanMinimunRaiseGiven_EmployeeMinimunRaiseGivenMustBeTrue()
		{
			// Arrange

			var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 5, 3000, false, 1);

			// Act
			await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 200);

			// Assert
			Assert.False(internalEmployee.MinimumRaiseGiven);
		}
	}
}
