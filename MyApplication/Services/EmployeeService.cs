namespace Bikes.Services
{
    public class EmployeeService : BaseService
    {
        public static EmployeeService Instance { get; } = new EmployeeService();

        public EmployeeService() : base("Employees")
        {

        }
    }
}
