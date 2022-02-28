namespace Site.Application.Models.User
{
    public class GetUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TcNo { get; set; }
        public string VehicleInformation { get; set; }
        public int ApartmentId { get; set; }
    }
}
