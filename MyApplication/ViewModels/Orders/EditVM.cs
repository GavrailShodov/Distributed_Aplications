using BikesApi.Entities;
using System.ComponentModel;

namespace Bikes.ViewModels.Orders
{
	public class EditVM
	{
        public Order Order { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
