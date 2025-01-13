using Application.Creates;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Updates;
using Domain.Models.View;

namespace Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewModel>();


        CreateMap<UserCreateModel, User>()
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(scr => Guid.NewGuid()));
        
        CreateMap<UserUpdateModel, User>()
            .ForAllMembers(opts => opts.Condition((_,_, scrMember) => scrMember != null));
    }
}