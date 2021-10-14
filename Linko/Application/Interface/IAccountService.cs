using Linko.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linko.Application
{
    public interface IAccountService
    {
        public Task<List<Account>> GetByUsername(string Username);
        public Task<Account> GetByID(int Id, int UserId);
        public Task<List<Account>> GetData(int UserId);
    }
}
