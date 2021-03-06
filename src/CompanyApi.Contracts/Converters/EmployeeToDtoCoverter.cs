﻿using System.Collections.Generic;
using System.Linq;
using CompanyApi.Contracts.Dto;
using CompanyApi.Contracts.Entities;
using Microsoft.Extensions.Logging;

namespace CompanyApi.Contracts.Converters
{
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
	public class EmployeeToDtoCoverter : IConverter<Employee, EmployeeDto>, IConverter<IList<Employee>, IList<EmployeeDto>>
	{
		private readonly ILogger<EmployeeToDtoCoverter> _logger;

		public EmployeeToDtoCoverter(ILogger<EmployeeToDtoCoverter> logger)
		{
			_logger = logger;
		}

		public EmployeeDto Convert(Employee employee)
		{
			_logger.LogDebug("Convert");
			var employeeDto = new EmployeeDto
			{
				EmployeeId = employee.EmployeeId,
				FirstName = employee.FirstName,
				LastName = employee.LastName,
                Company = employee.Company == null ? "N/A" : employee.Company.Name,
                Address = employee.EmployeeAddress == null ? "N/A" : employee.EmployeeAddress.Address,
                Age = employee.Age,
                BirthDate = employee.BirthDate,
                Department = employee.Department == null ? "N/A" : employee.Department.Name,
                Username = employee.User == null ? "N/A" : employee.User.Username
			};

			return employeeDto;
		}

		public IList<EmployeeDto> Convert(IList<Employee> employees)
		{
			_logger.LogDebug("ConvertList");
			return employees.Select(cmp =>
			{
				var employeeDto = Convert(cmp);
				return employeeDto;
			}).ToList();
		}
	}
}
