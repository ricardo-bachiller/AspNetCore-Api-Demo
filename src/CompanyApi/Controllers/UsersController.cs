using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.Contracts.Dto;
using CompanyApi.Contracts.Entities;
using CompanyApi.Controllers.Base;
using CompanyApi.Core.Errors;
using CompanyApi.Services.Authorization;
using CompanyApi.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UsersController : BaseController<UsersController>
	{
		private readonly IUserService _userService;
		private readonly IUserRepository _userRepository;

		public UsersController(IUserService userService, IUserRepository userRepository)
		{
			_userService = userService;
			_userRepository = userRepository;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody] UserDto userParam)
		{
			var user = _userService.Authenticate(userParam.Username, userParam.Password);
			if (user == null)
			{
				return BadRequest(new {message = "Username or password is incorrect"});
			}
			return Ok(user);
		}

		[AllowAnonymous]
		[HttpGet("getall")]
		public async Task<ActionResult<IList<User>>> GetAllAsync()
		{
			Logger.LogDebug("GetAllAsync");
			var users = await _userRepository.GetAllAsync().ConfigureAwait(false);
			if (!users.Any())
			{
				return NotFound(new NotFoundError("The Users list is empty"));
			}
			var result = users.Select(i => new
			{
				i.Username, 
				i.Password, 
				EmployeeFirstName = i.Employee?.FirstName, 
				EmployeeLastName = i.Employee?.LastName
			}).ToList();
			return Ok(result);
		}
	}
}