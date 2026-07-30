using System.ComponentModel.DataAnnotations;

namespace fims.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
}