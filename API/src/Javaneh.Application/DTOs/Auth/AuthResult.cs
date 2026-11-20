namespace Javaneh.Application.DTOs.Auth;

public class AuthResult
{
    public bool Succeeded { get; set; }

    public List<string> Errors { get; set; } = new();

    public string? Token { get; set; }
}