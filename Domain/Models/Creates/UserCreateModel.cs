namespace Application.Creates;

public class UserCreateModel
{
    public required string UserName { get; set; }
    
    public required string Password { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required int Phone  { get; set; }
    
    public required DateTime CreatedAt { get; set; }

    public Boolean isActive { get; set; } = true;
}