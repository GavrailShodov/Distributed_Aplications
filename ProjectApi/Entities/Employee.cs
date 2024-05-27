using System.ComponentModel.DataAnnotations;

namespace BikesApi.Entities
{
    public class Employee: BaseEntity
    {
        public Employee(string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }

        public Employee()
        {
            
        }

        [Display(Name="First name")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email adress")]
        [StringLength(100)]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Salary")]
        [Required]
        public double Salary { get; set; }

        [Display(Name = "Full-Time")]
        [Required]
        public bool IsFullTime { get; set; }

        [Display(Name = "Registered on")]
        [Required]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders { get; set; }
    }
}
