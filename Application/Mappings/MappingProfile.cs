using Application.Creates;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Updates;
using Domain.Models.View;

namespace Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // Data type
        CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

        CreateMap<User, AuthModel>();
        
        
        CreateMap<User, UserViewModel>();
        CreateMap<UserCreateModel, User>()
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(scr => Guid.NewGuid()));
        
        CreateMap<UserUpdateModel, User>()
            .ForAllMembers(opts => opts.Condition((_,_, scrMember) => scrMember != null));
    }
}