using System;
using System.Collections.Generic;

namespace Linko.Domain
{
    public class UserProfile : Master
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public int AccountNo { get; set; }
        public string FullName { get; set; }
        public string ProfileImg { get; set; }
        public string Lang { get; set; }
        public string Bio { get; set; }
        public DateTime LastAccessDate { get; set; }
        public int DeleteAfterPeriod { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
 