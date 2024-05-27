using BikesApi.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesApi.CommConstants
{
    #region Base Response
    public interface IResponse
    {
        bool Successfull { get; }
    }

    public abstract class Response : IResponse
    {
        public Response(bool isSuccessfull)
        {
            Successfull = isSuccessfull;
        }
        public bool Successfull { get; }
    }
    #endregion

    #region Customers Request
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public double AccountBalance { get; set; }

        public bool DeluxeAccount { get; set; }

        public DateTime RegisteredOn { get; set; }
        public int TotalOrders { get; set; }
    }

    public class EmployeeResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public double Salary { get; set; }

        public bool IsFullTime { get; set; }

        public DateTime RegisteredOn { get; set; }
        public int TotalOrders { get; set; }
    }

    public class OrderResponse
    {
        public int Id { get; set; }

        public string Details { get; set; }

        public string Problems { get; set; }

        public double Total { get; set; }

        public bool IsExpress { get; set; }

        public DateTime PlacedOn { get; set; }

        #region Foreign Keys

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }
        #endregion
    }
    #endregion
}
