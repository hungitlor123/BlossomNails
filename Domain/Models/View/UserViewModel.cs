namespace Domain.Models.View;

public class UserViewModel
{
    public Guid UserID { get; set; }
    public string UserName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int Phone { get; set; }
    
    public Boolean isActive { get; set; } 
}