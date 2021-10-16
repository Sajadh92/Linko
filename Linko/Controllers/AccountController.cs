using AutoMapper;
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
        private readonly IMapper _mapper;
        #endregion

        #region Const
        public AccountController(
            ILoggerRepository logger,
            IAccountService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region GetByUsername
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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

        #region Insert
        [HttpPost]
        public async Task<IActionResult> Insert(InsertAccountDto data)
        {
            try
            {
                Account account = _mapper.Map<InsertAccountDto, Account>(data);

                ResObj res = await _service.Insert(account, UserManager);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/Insert/{UserManager.Id}");
                return Response(false, Message.InsertFaild);
            }
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAccountDto data)
        {
            try
            {
                Account account = _mapper.Map<UpdateAccountDto, Account>(data);

                ResObj res = await _service.Update(account, UserManager);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/Update/UserId={UserManager.Id}&AccountId={data.Id}");
                return Response(false, Message.UpdateFaild);
            }
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                ResObj res = await _service.Delete(Id, UserManager);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/Delete/UserId={UserManager.Id}&AccountId={Id}");
                return Response(false, Message.DeleteFaild);
            }
        }
        #endregion

        #region UndoDelete
        public async Task<IActionResult> UndoDelete(int Id)
        {
            try
            {
                ResObj res = await _service.UndoDelete(Id, UserManager);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/UndoDelete/UserId={UserManager.Id}&AccountId={Id}");
                return Response(false, Message.UndoDeleteFaild);
            }
        }
        #endregion

        #region PermanentlyDelete
        public async Task<IActionResult> PermanentlyDelete(int Id)
        {
            try
            {
                ResObj res = await _service.PermanentlyDelete(Id, UserManager);

                return Response(res.Success, res.MsgCode, res.Data);
            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, $"Account/PermanentlyDelete/UserId={UserManager.Id}&AccountId={Id}");
                return Response(false, Message.PermanentlyDeleteFaild);
            }
        }
        #endregion
    }
}