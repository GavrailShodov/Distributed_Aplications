using System.ComponentModel.DataAnnotations;

namespace BikesApi.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public Customer()
        {
            
        }

        [Display(Name = "First Name")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Account Balance")]
        [Required]
        public double AccountBalance { get; set; }

        [Display(Name = "Deluxe Account")]
        public bool DeluxeAccount { get; set; }

        [Display(Name = "Registered on")]
        [Required]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders { get; set; }
    }
}
