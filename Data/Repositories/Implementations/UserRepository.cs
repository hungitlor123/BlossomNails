using Data.Repositories.Interfaces;
using Domain.Context;
using Domain.Entities;

namespace Data.Repositories.Implementations;

public class UserRepository: Repository<User>, IUserRepository
{
    public UserRepository(BlossomNailsContext context) : base(context)
    {
        
    }
    
}