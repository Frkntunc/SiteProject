using Site.Domain.Authentication;
using Site.Domain.Entities.Commons;

namespace Site.Domain.Entities
{
    public class Apartment :EntityBase
    {
        public byte Blok { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public byte Floor { get; set; }
        public byte ApartmentNumber { get; set; }
        public string Owner { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
