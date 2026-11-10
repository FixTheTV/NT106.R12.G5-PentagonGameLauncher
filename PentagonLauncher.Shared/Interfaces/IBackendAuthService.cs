using PentagonLauncher.Shared.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Interfaces;

public interface IBackendAuthService
{
    (bool IsValid, string Message) ValidateRegistrationInput(
        string username,
        string email,
        string password);

    Task<(bool Success, string Message)> RegisterAsync(
        string username,
        string email,
        string password);

    Task<RegisterResult> RegisterAsync(RegisterRequest request);

    Task<(bool Success, string Message, int UserId, string Username)> LoginAsync(
        string username,
        string password);

    Task<LoginResult> LoginAsync(LoginRequest request);

    (bool Success, string Message) Logout(string username);

    Task<(bool Success, string Message)> RequestPasswordResetAsync(
        string email);

    Task<ForgotPasswordResult> RequestPasswordResetAsync(
        ForgotPasswordRequest request);

    Task<(bool Success, string Message)> ResetPasswordAsync(
        string email,
        string otpCode,
        string newPassword);

    Task<ResetPasswordResult> ResetPasswordAsync(
        ResetPasswordRequest request);
}
