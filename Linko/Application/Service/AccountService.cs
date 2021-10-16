using Linko.Domain;
using Linko.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linko.Application
{
    public class AccountService : IAccountService, IRegisterScopped
    {
        private readonly LinkoContext _context;
        private readonly IDapperRepository<Account> _dapper;

        public AccountService(LinkoContext context,
            IDapperRepository<Account> dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<List<Account>> GetByUsername(string Username)
        {
            return await _dapper.GetEntityListAsync("dbo.Account_GetByUsername", 
                new { Username });
        }

        public async Task<Account> GetByID(int Id, int UserId)
        {
            return await _dapper.GetEntityAsync("dbo.Account_GetByID",
                new { Id, UserId });
        }

        public async Task<List<Account>> GetData(int UserId)
        {
            return await _dapper.GetEntityListAsync("dbo.Account_GetData",
                new { UserId });
        }

        private static ResObj? CheckAccount(Account account)
        {
            if (account.Name.IsEmpty())
                return new() { Success = false, MsgCode = "" };

            if (account.Type.IsEmpty())
                return new() { Success = false, MsgCode = "" };

            if (account.Link.IsEmpty())
                return new() { Success = false, MsgCode = "" };

            return null;
        }

        public async Task<ResObj> Insert(Account account, UserManager UserManager)
        {
            ResObj res = CheckAccount(account);

            if (res is not null) return res;

            account.UserId = UserManager.Id;

            await _context.Accounts.AddAsync(account);

            await _context.SaveChangesAsync();

            return new() { Success = true, MsgCode = "", Data = new { account.Id } };
        }

        public async Task<ResObj> Update(Account account, UserManager UserManager)
        {
            ResObj res = CheckAccount(account);

            if (res is not null) return res;

            Account old = await GetByID(account.Id, UserManager.Id);

            old.Name = account.Name;
            old.Type = account.Type;
            old.Link = account.Link;
            old.IsActive = account.IsActive;
            old.UpdateDate = Key.DateTimeIQ;

            _context.Entry(old).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new() { Success = true, MsgCode = "" };
        }

        public async Task<ResObj> Delete(int id, UserManager UserManager)
        {
            Account old = await GetByID(id, UserManager.Id);

            old.IsDeleted = true;
            old.DeleteDate = Key.DateTimeIQ;

            _context.Entry(old).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new() { Success = true, MsgCode = "" };
        }

        public async Task<ResObj> UndoDelete(int id, UserManager UserManager)
        {
            Account old = await GetByID(id, UserManager.Id);

            old.IsDeleted = false;
            old.UpdateDate = Key.DateTimeIQ;

            _context.Entry(old).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new() { Success = true, MsgCode = "" };
        }

        public async Task<ResObj> PermanentlyDelete(int id, UserManager UserManager)
        {
            await _dapper.RunSpAsync("dbo.Account_PermanentlyDelete", 
                new { id, userId = UserManager.Id });

            return new() { Success = true, MsgCode = "" };
        }
        
    }
}
