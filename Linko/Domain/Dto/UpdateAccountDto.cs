namespace Linko.Domain
{
    public class UpdateAccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
