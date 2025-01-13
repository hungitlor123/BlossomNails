namespace Domain.Models.Updates;

public class UserUpdateModel
{
    public string Firstname { get; set; }
    
    public string LastName { get; set; }
    
    public int Phone { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

}