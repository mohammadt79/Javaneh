using System.ComponentModel.DataAnnotations;

namespace Javaneh.Domain.Entities;

public class User
{
    [Key]
    public required Guid Guid { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAtUtc { get; set; }

}