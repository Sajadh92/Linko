using System;

namespace Linko.Domain
{
    public class Master
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
        public byte[] Version { get; set; }
    }
}
