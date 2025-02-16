using System.ComponentModel.DataAnnotations;

namespace TourDeApp.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Email je povinný")]
    [EmailAddress(ErrorMessage = "Zadejte platnou emailovou adresu")]
    public string Email { get; set; }

    
    [Required(ErrorMessage = "Heslo je povinné")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Minimální počet znaků: 6")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Potvrzení hesla je povinné")]
    [Compare(nameof(Password), ErrorMessage = "Hesla se neshodují")]
    public string ConfirmPassword { get; set; }
}