namespace BikesApi.CommConstants
{
    public static class Endpoints
    {
        #region Customer endpoints
        public const string CreateCustomerEndPoint = "createcustomer";
        public const string GetCustomerEndPoint = "getcustomer";
        public const string GetAllCustomersEndPoint = "getallcustomers";
        public const string UpdateCustomerEndPoint = "updatecustomer";
        public const string DeleteCustomerEndPoint = "deletecustomer";
        public const string SearchCustomersByFirstNameEndPoint = "searchcustomer";
        #endregion

        #region Employee endpoints
        public const string CreateEmployeeEndPoint = "createemployee";
      public const string GetEmployeeEndPoint = "getemployee";
      public const string GetAllEmployeesEndPoint = "getallemployees";
        public const string UpdateEmployeeEndPoint = "updateemployee";
       public const string DeleteEmployeeEndPoint = "deleteemployee";
       public const string SearchEmployeesByFirstNameEndPoint = "searchemployee";
        #endregion

        #region Order endpoints
        public const string CreateOrderEndPoint = "createorder";
        public const string GetOrderEndPoint = "getorder";
        public const string GetAllOrdersEndPoint = "getallorders";
        public const string UpdateOrderEndPoint = "updateorder";
        public const string DeleteOrderEndPoint = "deleteorder";
        public const string SearchOrdersByDetailsEndPoint = "searchorder";
        #endregion
    }
}
