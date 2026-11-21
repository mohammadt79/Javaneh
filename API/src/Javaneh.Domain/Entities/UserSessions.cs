
using System.ComponentModel.DataAnnotations;

namespace Javaneh.Domain.Entities;

public class UserSessions
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid SessionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

}
