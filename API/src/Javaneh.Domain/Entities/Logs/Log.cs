namespace Javaneh.Domain.Entities.Logs;

public class Log
{
    public int Id { get; set; }

    public string Level { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Exception { get; set; }

    public string? UserId { get; set; }

    public string? RequestPath { get; set; }

    public string? HttpMethod { get; set; }

    public int? StatusCode { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}