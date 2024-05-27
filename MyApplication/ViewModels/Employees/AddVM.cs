using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bikes.ViewModels.Employees
{
    public class AddVM
    {

        [DisplayName("First name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("Last name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }

        [DisplayName("Email Address: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string EmailAddress { get; set; }

        [DisplayName("Salary: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Salary { get; set; }

        [DisplayName("Full-Time: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool IsFullTime { get; set; }

        [Display(Name = "Registered on")]
        [Required(ErrorMessage = "This field is Required!")]
        public DateTime RegisteredOn { get; set; }
    }
}
