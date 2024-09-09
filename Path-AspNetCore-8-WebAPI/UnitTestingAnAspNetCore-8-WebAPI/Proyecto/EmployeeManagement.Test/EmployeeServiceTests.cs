using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.Fixtures;
using Xunit.Abstractions;

namespace EmployeeManagement.Test;

/*
 Esta interfaz IClassFixture se utiliza para compartir el mismo objeto de la clase de prueba entre todas las pruebas en la clase de prueba.
 */
[Collection("EmployeeServiceCollection")]
public class EmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>	
{
	private readonly EmployeeServiceFixture _employeeServiceFixture;
	private readonly ITestOutputHelper _testOutputHelper;

	public EmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture, ITestOutputHelper testOutputHelper)
	{
		_employeeServiceFixture = employeeServiceFixture;
		_testOutputHelper = testOutputHelper;
	}

	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
	{
		// Arrange		
		var obligatoryCourse = _employeeServiceFixture.EmployeeManagementTestDataRepository
			.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
		
		// Act
		var internalEmployee = _employeeServiceFixture.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");

		_testOutputHelper.WriteLine($"Employee after Act: " + $"{internalEmployee.FirstName} {internalEmployee.LastName}");
		internalEmployee.AttendedCourses.ForEach(course =>
		{
			_testOutputHelper.WriteLine($"Attended course: {course.Id} {course.Title}");
		});

		// Assert
		Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
	}

	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
	{
		// Arrange

		// Act
		var internalEmployee = _employeeServiceFixture.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");

		// Assert
		Assert.Contains(internalEmployee.AttendedCourses, 
			course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
	}


	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedSecondObligatoryCourse_WithPredicate()
	{
		// Arrange		

		// Act
		var internalEmployee = _employeeServiceFixture.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");

		// Assert
		Assert.Contains(internalEmployee.AttendedCourses,
			course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
	}

	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
	{
		// Arrange
		
		var obligatoryCourses = _employeeServiceFixture.EmployeeManagementTestDataRepository
			.GetCourses(
				Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
				Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

		// Act
		var internalEmployee = _employeeServiceFixture.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");

		// Assert
		Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
	}

	[Fact]
	public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
	{
		//*** Arrange

		//*** Act
		var internalEmployee = _employeeServiceFixture.EmployeeService
			.CreateInternalEmployee("Brooklyn", "Cannon");
		//internalEmployee.AttendedCourses[0].IsNew = true;

		//*** Assert
		
		// Prima forma
		//foreach (var course in internalEmployee.AttendedCourses)
		//{
		//	Assert.False(course.IsNew);
		//}

		// Segunda Forma
		Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));
	}

	[Fact]
	public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
	{
		// Arrange
		var obligatoryCourses = await _employeeServiceFixture.EmployeeManagementTestDataRepository
			.GetCoursesAsync(
				Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
				Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

		// Act
		var internalEmployee = await _employeeServiceFixture.EmployeeService
			.CreateInternalEmployeeAsync("Brooklyn", "Cannon");

		// Assert
		Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
	}

	[Fact]
	public async Task GiveRaise_RaiseBelowMinimunGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
	{
		// Arrange

		var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 2, 3000, false, 2);
		
		// Act && Assert
		await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
			async () => await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 50));
	}

	// Forma incorrecta de hacer el test
	//[Fact]
	//public void GiveRaise_RaiseBelowMinimunGiven_EmployeeInvalidRaiseExceptionMustBeThrown_Mistake()
	//{
	//	// Arrange
	//	var employeeService = new EmployeeService(
	//		new EmployeeManagementTestDataRepository(),
	//		new EmployeeFactory());

	//	var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 2, 3000, false, 2);

	//	// Act && Assert
	//	Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
	//		async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));
	//}

	[Fact]
	public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
	{
		// Arrange		
		var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 5, 3000, false, 1);

		// Act && Assert
		Assert.Raises<EmployeeIsAbsentEventArgs>(
			handler => _employeeServiceFixture.EmployeeService.EmployeeIsAbsent += handler,
			handler => _employeeServiceFixture.EmployeeService.EmployeeIsAbsent -= handler,
			() => _employeeServiceFixture.EmployeeService.NotifyOfAbsence(internalEmployee));
	}
}

