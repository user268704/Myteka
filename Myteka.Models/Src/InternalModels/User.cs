namespace Myteka.Models.InternalModels;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int AccessLevel { get; set; }
}