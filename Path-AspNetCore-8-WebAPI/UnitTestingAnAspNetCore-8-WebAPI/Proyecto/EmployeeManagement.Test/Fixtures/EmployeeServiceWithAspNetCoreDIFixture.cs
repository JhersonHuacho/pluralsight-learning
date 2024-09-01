using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test.Fixtures
{
	public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
	{
		private readonly ServiceProvider _serviceProvider;

		public IEmployeeManagementRepository EmployeeManagementTestDataRepository
		{
			get
			{
#pragma warning disable CS8603 // Possible null reference return.
				return _serviceProvider.GetService<IEmployeeManagementRepository>();
#pragma warning restore CS8603 // Possible null reference return.
			}
		}

		public IEmployeeService EmployeeService
		{
			get
			{
				return _serviceProvider.GetService<IEmployeeService>();
			}
		}
		public EmployeeServiceWithAspNetCoreDIFixture()
        {
			// nuestra colección de servicios
			var services = new ServiceCollection();
			services.AddScoped<EmployeeFactory>();
			services.AddScoped<IEmployeeManagementRepository, EmployeeManagementRepository>();
			services.AddScoped<IEmployeeService, EmployeeService>();
			// lo que sigue es construir el ServiceProvider
			// para eso se necesitamos llamar a BuildServiceProvider en la colección de servicios
			_serviceProvider = services.BuildServiceProvider();

		}
		public void Dispose()
		{
			//throw new NotImplementedException();
			// clean up the setup code, if required
		}
	}
}
