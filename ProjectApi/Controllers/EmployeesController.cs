using BikesApi.Repositories;
using BikesApi.CommConstants;
using BikesApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BikesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public EmployeesController()
        {
        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeRequest request)
        {
            try
            {
                // Save to database
                EmployeesRepository employeesRepo = new EmployeesRepository();
                Employee employee = new Employee(request.FirstName, request.LastName, request.EmailAddress, request.Salary, request.IsFullTime, request.RegisteredOn);
                employeesRepo.Save(employee);

                // Generate response
                var response = GenerateResponse(employee);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                // Retrieve from database
                EmployeesRepository repo = new EmployeesRepository();
                Employee employee = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                // Generate response
                var response = GenerateResponse(employee);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                EmployeesRepository employeesRepo = new EmployeesRepository();
                OrdersRepository ordersRepo = new OrdersRepository();
                List<Employee> allEmployees = employeesRepo.GetAll(i => true);
                List<Order> allOrders = ordersRepo.GetAll(i => true);

                foreach (var employee in allEmployees)
                {
                    int currentCount = 0;
                    foreach (var order in allOrders)
                    {
                        if (order.Employee_ID == employee.Id)
                        {
                            currentCount++;
                        }
                    }

                    employee.TotalOrders = currentCount;
                }

                var response = allEmployees.Select(baker => GenerateResponse(baker)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBaker(int id, UpdateBakerRequest request)
        {
            try
            {
                EmployeesRepository repo = new EmployeesRepository();

                // Find the existing baker object
                var existingEmployee = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update the existing baker object
                existingEmployee.FirstName = request.FirstName;
                existingEmployee.LastName = request.LastName;
                existingEmployee.EmailAddress = request.EmailAddress;
                existingEmployee.Salary = request.Salary;
                existingEmployee.IsFullTime = request.IsFullTime;
                existingEmployee.RegisteredOn = request.RegisteredOn;

                // Save changes to the database
                repo.Save(existingEmployee);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBaker(int id)
        {
            try
            {
                EmployeesRepository repo = new EmployeesRepository();

                Employee employee = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                repo.Delete(employee);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("search/{searchWord}")]
        public IActionResult SearchBakersByFirstName(string searchWord)
        {
            try
            {
                EmployeesRepository repo = new EmployeesRepository();
                List<Employee> employeesSearchResult = repo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                var response = employeesSearchResult.Select(baker => GenerateResponse(baker)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private EmployeeResponse GenerateResponse(Employee employee)
        {
            var response = new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmailAddress = employee.EmailAddress,
                Salary = employee.Salary,
                IsFullTime = employee.IsFullTime,
                RegisteredOn = employee.RegisteredOn,
                TotalOrders = employee.TotalOrders
            };

            return response;
        }
    }
}