using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Interfaces;
using Application.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data.Repositories.Interfaces;
using Data.UnitOfWork.Implementations;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations;

public class AuthService(UnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings) : BaseService(unitOfWork, mapper), IAuthService

{
    private readonly IUserRepository _userRepository = unitOfWork.User;
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task<IActionResult> Authenticate(CertificateModel certificate)
    {
        try
        {
            if (_userRepository.Any(x =>
                    x.Username.Equals(certificate.Username) && x.Password.Equals(certificate.Password)))
            {
                return await AuthenticateUser(certificate);
            }

            return AppErrors.INVALID_CERTIFICATE.BadRequest();
        } 
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<IActionResult> AuthenticateUser(CertificateModel certificate)
    {
        var user = await _userRepository
            .Where(x => x.Username.Equals(certificate.Username) && x.Password.Equals(certificate.Password))
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return AppErrors.INVALID_CERTIFICATE.BadRequest();
        }

        if (user.IsActive.Equals(CustomerStatuses.INACTIVE))
        {
            return AppErrors.INVALID_USER_UNACTIVE.NotAcceptable();
        }
        var auth = _mapper.Map<AuthModel>(user);
        var accessToken = GenerateJwtToken(auth);
        return new AuthViewModel
        {
            AccessToken = accessToken,
            User = new UserViewModel
            {
                UserID = user.UserID,
                UserName = user.Username,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                isActive = user.IsActive,
                CreateAt = user.CreatedAt
                
                
            }
        }.Ok();
    }

    public async Task<IActionResult> GetInformation(Guid id)
    {
        if (_userRepository.Any(x => x.UserID.Equals(id)))
        {
            var user = await _userRepository.Where(st => st.UserID.Equals(id))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return user.Ok();
            }
        }

        return AppErrors.RECORD_NOT_FOUND.NotFound();
    }

    public async Task<AuthModel> GetUser(Guid id)
    {
        try
        {
            if (_userRepository.Any(x => x.UserID.Equals(id)))
            {
                var user = await _userRepository.Where(st => st.UserID.Equals(id))
                    .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    return user;
                }
            }

            return null!;
        }
        catch (Exception )
        {
            throw;
        }
    }
    
    // API use JWT token
    private string GenerateJwtToken(AuthModel auth)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
            // Bo cai gi vao token de sau lay ra xac thuc
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", auth.UserId.ToString()),
                }),
                Expires = DateTime.Now.AddDays(7),
                // Lay secrect key tu appsetings.json de ma hoa token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}