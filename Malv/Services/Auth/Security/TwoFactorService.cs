using System.Security.Cryptography;
using Malv.Controllers.Exceptions;
using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Malv.Models;
using Malv.Services.Email;

namespace Malv.Services.Auth.Security;

public class TwoFactorService : ITwoFactorService
{
    private readonly IMailTokenRepository _mailTokenRepository;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public TwoFactorService(IMailTokenRepository mailTokenRepository, IEmailService emailService, IUserRepository userRepository)
    {
        _mailTokenRepository = mailTokenRepository;
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task SendMailVerification(MalvUser malvUser)
    {
        string mailToken = GenerateRandomToken(32);

        await _mailTokenRepository.AddToken(new MailToken()
        {
            Token = mailToken,
            UserId = malvUser.Id,
            TokenType = MailToken.MailTokenType.VerifyMail
        });
        await _emailService.SendEmailAsync(malvUser.Email, "Verify mail", $@"
You need to verify your email address, press the link below:
http://localhost:3000/Auth/VerifyMail?token={mailToken}
");
    }

    public async Task ValidateEmailVerification(string token)
    {
        MailToken mailToken = await _mailTokenRepository.FindMailToken(token, MailToken.MailTokenType.VerifyMail);
        if (mailToken == null || mailToken.User == null)
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_MAIL_TOKEN", "The token is invalid."));

        MalvUser user = mailToken.User;
        user.MailVerified = true;
        await _userRepository.UpdateUser(user);
        await _mailTokenRepository.RemoveToken(mailToken);
    }

    #region Token Generation
    private const string TokenAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

    private static string GenerateRandomToken(int length = 16)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(length);
        var token =
            tokenBytes
                .Select(b => TokenAlphabet[b % TokenAlphabet.Length])
                .ToArray();

        return new string(token);
    }
    #endregion
}