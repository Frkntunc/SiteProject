namespace Site.Application.Models.Apartment
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public byte Blok { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public byte Floor { get; set; }
        public byte ApartmentNumber { get; set; }
        public string Owner { get; set; }
        public int UserId { get; set; }
    }
}
