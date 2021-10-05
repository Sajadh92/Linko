using AutoMapper;
using Linko.Domain;
using Linko.Domain.General;
using Linko.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Linko.Application
{
    public class AccountService : IAccountService, IRegisterScopped
    {
        private readonly IMapper _mapper;
        private readonly LinkoContext _context;
        private readonly IDapperRepository<UserProfile> _userRepository;
        
        public AccountService(
            IMapper mapper,
            LinkoContext context,
            IDapperRepository<UserProfile> userRepository)
        {
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<ResObj> Login(LoginDto data)
        {
            if (data.Username.IsEmpty())
                return Result.Return(false, Message.InvalidUsername);

            if (data.Password.IsEmpty())
                return Result.Return(false, Message.InvalidPassword);

            UserProfile user = await GetByIdentity("Login", data.Username, data.Password);

            if (user is null)
                return Result.Return(false, Message.UsernameOrPasswordNotCorrect);

            if (!user.IsActive)
                return Result.Return(false, Message.UserNotActive);

            if (user.IsDeleted)
                return Result.Return(false, Message.UserIsDeleted);

            user.LastAccessDate = Key.DateTimeIQ;

            //_context.Update(user);

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            UserManager userManager = _mapper.Map<UserProfile, UserManager>(user);

            string token = JsonWebToken.GenerateToken(userManager);

            return Result.Return(true, new { token });
        }

        public async Task<ResObj> Register(RegisterDto data)
        {
            if (data.Username.IsEmpty())
                return Result.Return(false, Message.InvalidUsername);

            if (data.Password.IsEmpty())
                return Result.Return(false, Message.InvalidPassword);

            if (data.Email.IsEmpty())
                return Result.Return(false, Message.InvalidEmail);

            if (data.Password.IsPasswordStrength())
                return Result.Return(false, Message.PasswordNotStrength);

            UserProfile user = await GetByIdentity("CheckIdentity", data.Username, data.Email);

            if (user is null)
                return Result.Return(false, Message.UsernameOrEmailAlreadyExist);

            UserProfile profile = _mapper.Map<RegisterDto, UserProfile>(data);

            profile.VerificationCode = new Random().Next(100000, 999999).ToString("D6");

            await _context.UsersProfiles.AddAsync(profile);

            await _context.SaveChangesAsync();

            // TODO: Send profile.VerificationCode to profile.Email

            return Result.Return(true);
        }

        public async Task<UserProfile> GetByIdentity(string CallType, string Identity, string PassOrCode)
        {
            return await _userRepository.GetEntityAsync("dbo.UserProfile_GetByIdentity",
                new { CallType, Identity, PassOrCode });
        }

    }
}
