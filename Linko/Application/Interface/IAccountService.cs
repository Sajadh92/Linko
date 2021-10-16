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
        public Task<ResObj> Insert(Account account, UserManager UserManager);
        public Task<ResObj> Update(Account account, UserManager UserManager);
        public Task<ResObj> Delete(int id, UserManager UserManager);
        public Task<ResObj> UndoDelete(int id, UserManager UserManager);
        public Task<ResObj> PermanentlyDelete(int id, UserManager UserManager);
    }
}
