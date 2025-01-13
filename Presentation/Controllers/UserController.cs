using Application.Creates;
using Application.Services.Interfaces;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetUsers([FromQuery] UserFilterModel filter,[FromQuery] PaginationRequestModel pagination)
    {
        return _userService.GetUsers(filter, pagination);
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUserById ([FromRoute] Guid id)
    {
        return _userService.GetUserById(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser( [FromBody] UserCreateModel model)
    {
        return await _userService.CreateUser(model);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateModel model)
    {
        return await _userService.UpdateUser(id, model);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        return await _userService.DeleteUser(id);
    }
}