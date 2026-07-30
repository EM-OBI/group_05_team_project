using System.ComponentModel.DataAnnotations;

namespace fims.Models.ViewModels;
public class RegisterViewModel
{
    [Required]
    public string FullName { get; set; } = String.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = String.Empty;
}