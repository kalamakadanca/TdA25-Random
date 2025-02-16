using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TourDeApp.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Email je povinný")]
    [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Heslo je povinné")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Minimální počet znaků: 6")]
    public string Password { get; set; }
}