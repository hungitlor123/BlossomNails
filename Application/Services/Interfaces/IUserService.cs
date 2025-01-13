using Application.Creates;
using Domain.Entities;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IUserService
{
    IActionResult GetUsers(UserFilterModel filter, PaginationRequestModel pagination);

    IActionResult GetUserById(Guid id);

    Task<IActionResult> CreateUser(UserCreateModel model);

    Task<IActionResult> UpdateUser(Guid id,UserUpdateModel model);

    Task<IActionResult> DeleteUser(Guid id);
}