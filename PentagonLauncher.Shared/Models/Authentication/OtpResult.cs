namespace PentagonLauncher.Shared.Models.Authentication;

public sealed class OtpResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
}
