
namespace Linko.Domain
{
    public class Account : Master
    {
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string Prefix { get; set; }
        public string Link { get; set; }
    }
}
