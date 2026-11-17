using System.ComponentModel.DataAnnotations;

namespace Javaneh.Domain.Entities;

public class User
{
    [Key]
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }

    public required string LastName { get; set; }
    public required string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public required string PhoneNumber { get; set; }
}