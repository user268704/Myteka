using System.ComponentModel.DataAnnotations;

namespace Myteka.Models.ExternalModels.RegisterModels;

public class UserRegisterModel
{
    [Required(ErrorMessage = "Name is required")]
    public string UserName { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}