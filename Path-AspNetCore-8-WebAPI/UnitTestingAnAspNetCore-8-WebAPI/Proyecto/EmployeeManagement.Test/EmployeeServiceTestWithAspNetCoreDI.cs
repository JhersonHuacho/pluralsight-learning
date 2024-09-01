using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test;

public class EmployeeServiceTestWithAspNetCoreDI : IClassFixture<EmployeeServiceWithAspNetCoreDIFixture>
{
	private readonly EmployeeServiceWithAspNetCoreDIFixture _employeeServiceWithAspNetCoreDIFixture;

	public EmployeeServiceTestWithAspNetCoreDI(EmployeeServiceWithAspNetCoreDIFixture employeeServiceWithAspNetCoreDIFixture)
	{
		_employeeServiceWithAspNetCoreDIFixture = employeeServiceWithAspNetCoreDIFixture;
	}

	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
	{
		// Arrange
		var obligatoryCourse = _employeeServiceWithAspNetCoreDIFixture
			.EmployeeManagementTestDataRepository
			.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

		// Act
		var internalEmployee = _employeeServiceWithAspNetCoreDIFixture
			.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");

		// Assert
		Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
	}
}
