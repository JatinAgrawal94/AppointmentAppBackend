using System.ComponentModel.DataAnnotations;
namespace AppointmentAppBackend.Dtos;

public class RegisterUser
{
    [Required(ErrorMessage = "Email is required.")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Confirm Your Password.")]
    public string ConfirmPassword { get; set; }
}
