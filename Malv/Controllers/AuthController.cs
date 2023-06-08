using Malv.Controllers.Exceptions;
using Malv.Controllers.Helpers;
using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Malv.Filters;
using Malv.Models;
using Malv.Models.AuthController;
using Malv.Services.Auth;
using Malv.Services.Auth.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

[Controller]
public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly ITwoFactorService _twoFactorService;

    public AuthController(IAuthRepository authRepository, IJwtService jwtService, IUserRepository userRepository, ITwoFactorService twoFactorService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _twoFactorService = twoFactorService;
    }

    [HttpPost]
    [Transaction]
    public async Task<IActionResult> LogIn([FromBody] Auth_LogIn_Req req)
    {
        if (string.IsNullOrEmpty(req.Email))
            throw new BusinessMalvException(new Error_Res().AddError("EMAIL_NOT_SET",
                "Email can't be empty"));
        if (string.IsNullOrEmpty(req.Password))
            throw new BusinessMalvException(new Error_Res().AddError("PASSWORD_NOT_SET",
                "Password can't be empty"));

        MalvUser malvUser = await _userRepository.FindUserByEmail(req.Email);
        if (malvUser == null)
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_LOGIN",
                "Invalid username or password"));
        
        if (!malvUser.MailVerified)
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_LOGIN",
                "Email is not verified!"));
        
        if (malvUser.Password != req.Password)
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_LOGIN",
                "Invalid username or password"));

        UserToken_Res userToken = _jwtService.GenerateUserToken(malvUser);
        malvUser.UserData.RefreshToken = userToken.RefreshToken;
        await _authRepository.UpdateUserData(malvUser.UserData);
        return Ok(userToken);
    }

    [HttpPost]
    [Transaction]
    public async Task Register([FromBody] Auth_Register_Req req)
    {
        if (string.IsNullOrEmpty(req.Email))
            throw new BusinessMalvException(new Error_Res().AddError("EMAIL_NOT_SET",
                "Email can't be empty"));
        if (string.IsNullOrEmpty(req.Password))
            throw new BusinessMalvException(new Error_Res().AddError("PASSWORD_NOT_SET",
                "Password can't be empty"));
        if (string.IsNullOrEmpty(req.ConfirmPassword))
            throw new BusinessMalvException(new Error_Res().AddError("CONFIRM_PASSWORD_NOT_SET",
                "Confirm password can't be empty"));
        if (string.IsNullOrEmpty(req.FirstName))
            throw new BusinessMalvException(new Error_Res().AddError("FIRSTNAME_NOT_SET",
                "Firstname can't be empty"));
        if (string.IsNullOrEmpty(req.LastName))
            throw new BusinessMalvException(new Error_Res().AddError("LASTNAME_NOT_SET",
                "Lastname can't be empty"));

        if (await _authRepository.EmailExists(req.Email.Trim()))
            throw new BusinessMalvException(new Error_Res().AddError("EMAIL_IN_USE  ",
                "Email is in use"));

        MalvUser malvUser = new MalvUser()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Email = req.Email,
            Password = req.Password,
            UserData = new UserData()
            {
                RefreshToken = null
            },
            MailVerified = false
        };
        await _userRepository.CreateUser(malvUser);
        await _authRepository.UpdateUserData(malvUser.UserData);
        await _twoFactorService.SendMailVerification(malvUser);
    }
    
    [HttpPost]
    [Transaction]
    public async Task<IActionResult> ValidateRefreshToken([FromBody] Auth_ValidateRefreshToken_Req req)
    {
        string refreshToken = req.RefreshToken;
        if (refreshToken == null)
            throw new BusinessMalvException(new Error_Res().AddError("REFRESH_TOKEN_NOT_SET",
                "Refresh token can't be null"));
        MalvUser malvUser = await _authRepository.FindUserByRefreshToken(refreshToken);
        if (malvUser == null)
            throw new BusinessMalvException(new Error_Res().AddError("REFRESH_TOKEN_INVALID",
                "Refresh token isn't valid"));

        bool validRefreshToken = malvUser.UserData.RefreshToken == refreshToken;
        if (!validRefreshToken || string.IsNullOrWhiteSpace(malvUser.UserData.RefreshToken))
            throw new BusinessMalvException(new Error_Res().AddError("REFRESH_TOKEN_INVALID",
                "Refresh token isn't valid"));

        UserToken_Res userTokenRes = _jwtService.GenerateUserToken(malvUser);
        malvUser.UserData.RefreshToken = userTokenRes.RefreshToken;
        await _authRepository.UpdateUserData(malvUser.UserData);
        return Ok(userTokenRes);
    }

    [HttpPost]
    [Transaction]
    public async Task VerifyMail([FromBody] Auth_VerifyMail_Req req)
    {
        if (string.IsNullOrWhiteSpace(req.Token))
            throw new BusinessMalvException(new Error_Res().AddError("INVALID_MAIL_TOKEN", "Token can't be null!"));
        await _twoFactorService.ValidateEmailVerification(req.Token);
    }
    
    [HttpPost]
    [Transaction]
    [Authorize]
    public async Task Logout()
    {
        MalvUser malvUser = await _userRepository.FindUserById(User.GetUserId());
        UserData userData = malvUser.UserData;
        userData.RefreshToken = null;
        await _authRepository.UpdateUserData(userData);
    }
}