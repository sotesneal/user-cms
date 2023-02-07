using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class User
    {
        public User()
        {
            
            CreatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        public Address? PhysicalAddress { get; set; }
        public Address? BillingAddress { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class Address
    {
        [Required(ErrorMessage = "Landmark is required.")]
        public string Landmark { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
    }
}
