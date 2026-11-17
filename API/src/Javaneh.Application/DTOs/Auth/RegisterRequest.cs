
using System.ComponentModel.DataAnnotations;

namespace Javaneh.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
}
