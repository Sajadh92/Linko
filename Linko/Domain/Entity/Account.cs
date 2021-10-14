
namespace Linko.Domain
{
    public class Account : Master
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string ImgUrl { get; set; }
    }
}
