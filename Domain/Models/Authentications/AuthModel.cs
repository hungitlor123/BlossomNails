namespace Domain.Models.Authentications
{
    public class AuthModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Boolean IsActive { get; set; }
    }
}
