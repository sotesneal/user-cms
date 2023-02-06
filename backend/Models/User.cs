namespace backend.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address BillingAddress { get; set; }
        public long UpdatedAt { get; set; }
        public long CreatedAt { get; set; }
    }
    public class Address
    {
        public string Landmark { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
