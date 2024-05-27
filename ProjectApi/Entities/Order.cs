using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesApi.Entities
{
    public class Order : BaseEntity
    {
        public Order(string details, string problems, double total, bool isExpress, DateTime placedOn, int customer_ID, int employee_ID)
        {
            Details = details;
            Problems = problems;
            Total = total;
            IsExpress = isExpress;
            PlacedOn = placedOn;
            Customer_ID = customer_ID;
            Employee_ID = employee_ID;
        }

        public Order()
        {
            
        }

        public string Details { get; set; }

        public string Problems { get; set; }

        public double Total { get; set; }

        public bool IsExpress { get; set; }

        public DateTime PlacedOn { get; set; }

        #region Foreign Keys
        public int Customer_ID { get; set; }

        public int Employee_ID { get; set; }

        [ForeignKey("Customer_ID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("Employee_ID")]
        public virtual Employee? Employee { get; set; }
        #endregion
    }
}
