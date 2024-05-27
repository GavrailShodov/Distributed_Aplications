using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bikes.ViewModels.Customers
{
    public class AddVM
    {
        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }

        [DisplayName("Address: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Address { get; set; }

        [DisplayName("Account Balance: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double AccountBalance { get; set; }

        [DisplayName("Deluxe Account: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool DeluxeAccount { get; set; }
    }
}
