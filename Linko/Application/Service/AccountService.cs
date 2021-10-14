using Linko.Domain;
using Linko.Helper;
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
    }
}
