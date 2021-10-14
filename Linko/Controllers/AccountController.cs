using Linko.Application;
using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linko.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class AccountController : MasterController
    {
        #region Readonly
        private readonly ILoggerRepository _logger;
        private readonly IAccountService _service;
        #endregion

        #region Const
        public AccountController(
            ILoggerRepository logger,
            IAccountService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region GetByUsername
        public async Task<IActionResult> GetByUsername(string Username)
        {
            try
            {
                List<Account> data = await _service.GetByUsername(Username);

                return Response(true, data);
            }
            catch(Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/GetByUsername/{Username}");
                return Response(false, Message.GetUserAccountsFaild);
            }
        }
        #endregion

        #region GetByID
        public async Task<IActionResult> GetByID(int Id) 
        {
            try
            {
                Account data = await _service.GetByID(Id, UserManager.Id);

                return Response(true, data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/GetByID/{Id}");
                return Response(false, Message.GetFaild);
            }
        }
        #endregion

        #region GetData
        public async Task<IActionResult> GetData()
        {
            try
            {
                List<Account> data = await _service.GetData(UserManager.Id);

                return Response(true, data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/GetData/{UserManager.Id}");
                return Response(false, Message.GetFaild);
            }
        }
        #endregion
    }
}
