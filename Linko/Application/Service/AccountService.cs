using Linko.Domain;
using Linko.Helper;
using System.Threading.Tasks;

namespace Linko.Application
{
    public class AccountService : IAccountService
    {
        private readonly IDapperRepository<UserProfile> _userRepository;
        private readonly LinkoContext _context;

        public AccountService(IDapperRepository<UserProfile> userRepository,
            LinkoContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<UserProfile> Login(LoginDto data)
        {
            return await _userRepository.GetEntityAsync("dbo.UserProfile_Login",
                new { data.Username, data.Password });

            //return _context.UsersProfiles
            //    .Where(x => x.Username == data.Username
            //    && x.Password == data.Password).FirstOrDefault();
        }

        public async Task<UserProfile> Register(RegisterDto data)
        {
            return await _userRepository.GetEntityAsync("dbo.UserProfile_Register",
                new { data.Username, data.Email });
        }

        public async Task<UserProfile> VerificationEmail(VerificationEmailDto data)
        {
            return await _userRepository.GetEntityAsync("dbo.UserProfile_VerificationEmail",
                new { data.Email, data.VerificationCode });
        }
    }
}
