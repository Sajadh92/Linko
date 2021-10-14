using Linko.Application;
using Linko.Domain;
using Linko.Domain.General;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Linko.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class SecurityController : MasterController
    {
        #region Readonly
        private readonly LinkoContext _context;
        private readonly ILoggerRepository _logger;
        private readonly ISecurityService _accountService;
        #endregion

        #region Const
        public SecurityController(
            LinkoContext context,
            ILoggerRepository logger,
            ISecurityService accountService)
        {
            _context = context;
            _logger = logger;
            _accountService = accountService;
        }
        #endregion

        #region Login
        public async Task<IActionResult> Login(LoginDto data)
        {
            try
            {
                ResObj res = await _accountService.Login(data);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => Login => Username:" + data.Username);
                return Response(false, Message.LoginFaild);
            }
        }
        #endregion

        #region Register
        public async Task<IActionResult> Register(RegisterDto data)
        {
            try
            {
                ResObj res = await _accountService.Register(data);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => Register => Email:" + data.Email);
                return Response(false, Message.RegisterFaild);
            }
        }
        #endregion

        #region VerificationEmail
        public async Task<IActionResult> VerificationEmail(VerificationEmailDto data)
        {
            try
            {
                if (data.Email.IsEmpty())
                    return Response(false, Message.InvalidEmail);

                if (data.VerificationCode.IsEmpty() || data.VerificationCode.Length != 6)
                    return Response(false, Message.InvalidVerificationCode);

                UserProfile user = await _accountService
                    .GetByIdentity("VerifiCode", data.Email, data.VerificationCode);

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
        #endregion

        #region FindUserProfile
        public async Task<IActionResult> FindUserProfile(string userIdentity)
        {
            try
            {
                if (userIdentity.IsEmpty())
                    return Response(false, Message.InvalidIdentity);

                UserProfile user = await _accountService
                    .GetByIdentity("CheckIdentity", userIdentity);

                //UserProfile user = _context.UsersProfiles
                //    .Where(x => x.Username == userIdentity || x.Email == userIdentity)
                //    .FirstOrDefault();

                if (user is null)
                    return Response(false, Message.UserNotExist);

                if (!user.IsActive)
                    return Response(false, Message.UserNotActive);

                if (user.IsDeleted)
                    return Response(false, Message.UserIsDeleted);

                user.VerificationCode = new Random().Next(100000, 999999).ToString("D6");

                _context.Entry(user).State = EntityState.Modified;

                //_context.Update(user);

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
        #endregion

        #region ChangePassword
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

                UserProfile user = await _accountService
                    .GetByIdentity("CheckIdentity", data.UserIdentity);

                //UserProfile user = _context.UsersProfiles
                //    .Where(x => x.Username == data.UserIdentity || x.Email == data.UserIdentity)
                //    .FirstOrDefault();

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

                _context.Entry(user).State = EntityState.Modified;

                //_context.Update(user);

                await _context.SaveChangesAsync();

                return Response(true);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "AccountController => ChangePassword => UserIdentity:" + data.UserIdentity);
                return Response(false, Message.ChangePasswordFaild);
            }
        }
        #endregion
    }
}
