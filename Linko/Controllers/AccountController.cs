using Linko.Application;
using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Linko.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class AccountController : MasterController
    {
        private readonly LinkoContext _context;
        private readonly ILoggerRepository _logger;
        private readonly IAccountService _accountService;
        
        public AccountController(LinkoContext context,
            ILoggerRepository logger, IAccountService accountService)
        {
            _context = context;
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Login(LoginDto data)
        {
            try
            {
                if (data.Username.IsEmpty())
                    return Response(false, Message.InvalidUsername);

                if (data.Password.IsEmpty())
                    return Response(false, Message.InvalidPassword);

                UserProfile user = await _accountService.Login(data);

                if (user is null)
                    return Response(false, Message.UsernameOrPasswordNotCorrect);

                if (!user.IsActive)
                    return Response(false, Message.UserNotActive);

                if (user.IsDeleted)
                    return Response(false, Message.UserIsDeleted);

                user.LastAccessDate = Key.DateTimeIQ;

                //_context.Update(user);

                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                UserJWTClaimDto userJWT = new()
                {
                    Id = user.Id,
                    Lang = user.Lang
                };

                string token = JsonWebToken.GenerateToken(userJWT);

                return Response(true, new { token });
            }
            catch(Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => Login => Username:" + data.Username);
                return Response(false, Message.LoginFaild);
            }
        }
    
        public async Task<IActionResult> Register(RegisterDto data)
        {
            try
            {
                if (data.Username.IsEmpty())
                    return Response(false, Message.InvalidUsername);

                if (data.Password.IsEmpty())
                    return Response(false, Message.InvalidPassword);

                if (data.Email.IsEmpty())
                    return Response(false, Message.InvalidEmail);

                if (data.Password.IsPasswordStrength())
                    return Response(false, Message.PasswordNotStrength);

                UserProfile user = await _accountService.Register(data);

                if (user is null)
                    return Response(false, Message.UsernameOrEmailAlreadyExist);

                UserProfile profile = new()
                {
                    Username = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    VerificationCode = new Random().Next(100000, 999999).ToString("D6")
                };

                await _context.UsersProfiles.AddAsync(profile);

                await _context.SaveChangesAsync();

                // TODO: Send profile.VerificationCode to profile.Email

                return Response(true);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => Register => Email:" + data.Email);
                return Response(false, Message.RegisterFaild);
            }
        }

        public async Task<IActionResult> VerificationEmail(VerificationEmailDto data)
        {
            try
            {
                if (data.Email.IsEmpty())
                    return Response(false, Message.InvalidEmail);

                if (data.VerificationCode.IsEmpty() || data.VerificationCode.Length != 6)
                    return Response(false, Message.InvalidVerificationCode);

                UserProfile user = await _accountService.VerificationEmail(data);

                if (user is null)
                    return Response(false, Message.EmailOrVerificationCodeNotCorrect);

                user.IsActive = true;
                user.VerificationCode = string.Empty;

                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Response(true);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => VerificationEmail => Email:" + data.Email);
                return Response(false, Message.VerificationFaild);
            }
        }

        public async Task<IActionResult> FindUserProfile(string userIdentity)
        {
            try
            {
                if (userIdentity.IsEmpty())
                    return Response(false, Message.InvalidIdentity);

                UserProfile user = _context.UsersProfiles
                    .Where(x => x.Username == userIdentity || x.Email == userIdentity)
                    .FirstOrDefault();

                if (user is null)
                    return Response(false, Message.UserNotExist);

                if (!user.IsActive)
                    return Response(false, Message.UserNotActive);

                if (user.IsDeleted)
                    return Response(false, Message.UserIsDeleted);

                user.VerificationCode = new Random().Next(100000, 999999).ToString("D6");

                _context.Update(user);

                await _context.SaveChangesAsync();

                // TODO: Send profile.VerificationCode to profile.Email

                return Response(true);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => FindUserProfile => UserIdentity:" + userIdentity);
                return Response(false, Message.FindUserProfileFaild);
            }
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordDto data)
        {
            // type => (Change, Forget)

            try
            {
                if (data.UserIdentity.IsEmpty())
                    return Response(false, Message.InvalidIdentity);

                if (data.VerificationCode.IsEmpty() || data.VerificationCode.Length != 6)
                    return Response(false, Message.InvalidVerificationCode);

                if (data.OldPassword.IsEmpty() || data.NewPassword.IsEmpty())
                    return Response(false, Message.InvalidPassword);

                if (data.NewPassword.IsPasswordStrength())
                    return Response(false, Message.PasswordNotStrength);

                UserProfile user = _context.UsersProfiles
                    .Where(x => x.Username == data.UserIdentity || x.Email == data.UserIdentity)
                    .FirstOrDefault();

                if (user is null)
                    return Response(false, Message.UserNotExist);

                if (data.Type == "Change" && user.Password == data.OldPassword)
                    user.Password = data.NewPassword;
                else
                    return Response(false, Message.OldPasswordNotCorrect);

                if (data.Type == "Forget" && user.VerificationCode == data.VerificationCode)
                    user.Password = data.NewPassword;
                else
                    return Response(false, Message.VerificationCodeNotCorrect);

                return Response(true);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => ChangePassword => UserIdentity:" + data.UserIdentity);
                return Response(false, Message.ChangePasswordFaild);
            }
        }
    }
}
