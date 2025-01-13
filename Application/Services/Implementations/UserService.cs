
using Application.Creates;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data.Repositories.Interfaces;
using Data.UnitOfWork.Interfaces;
using Domain.Entities;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _autoMapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService( IUnitOfWork unitOfWork, IMapper mapper )
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.User;
        _autoMapper = mapper;
        
    }

    public IActionResult GetUsers(UserFilterModel filter, PaginationRequestModel pagination)
    {
        var query = _userRepository.GetAll();

        if (filter.Username != null)
        {
            query = query.Where(x => x.Username.Contains(filter.Username));
        }
        var totalRows = query.Count();
        var users = query
            .OrderByDescending(x => x.CreatedAt)
            .Paginate(pagination)
            .ProjectTo<UserViewModel>(_autoMapper.ConfigurationProvider)
            .ToList();
        return new OkObjectResult(users.ToPaged(pagination, totalRows));
    }

    public IActionResult GetUserById(Guid id)
    {
        var user = _userRepository.Where(x => x.UserID.Equals(id))
            .ProjectTo<UserViewModel>(_autoMapper.ConfigurationProvider)
            .FirstOrDefault();

        if (user == null)
        {
            return new NotFoundResult();
        }
        return new ObjectResult(user);
    }

    public async Task<IActionResult> CreateUser(UserCreateModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return new BadRequestResult();
        }
        
        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

        if (_userRepository.Any(x => x.Username.ToUpper().Equals(model.UserName.ToUpper())))
        {
            return new BadRequestResult();
        }
        var user = _autoMapper.Map<User>(model);
        
        _userRepository.Add(user);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return GetUserById(user.UserID);
        }
        return new BadRequestResult();
    }

    public async Task<IActionResult> UpdateUser(Guid id,UserUpdateModel model)
    {
        var user = await _userRepository.Where(x => x.UserID.Equals(id)).FirstAsync();

        if (user == null )
        {
            return new NotFoundResult();
        }

        var update = _autoMapper.Map(model, user);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0) 
        {
            return GetUserById(user.UserID);
        }
        return new BadRequestResult();
    }

    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = _userRepository.Where(x => x.UserID.Equals(id)).FirstOrDefault();
        if (user == null)
        {
            return new NotFoundResult();
        }
        _userRepository.Delete(user);
        var result = await _unitOfWork.SaveChangesAsync();
        return new NoContentResult();
    }
    


}