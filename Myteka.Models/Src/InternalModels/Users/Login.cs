using System.ComponentModel.DataAnnotations;

namespace Myteka.Models.InternalModels.Users;

public class Login
{
    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
}