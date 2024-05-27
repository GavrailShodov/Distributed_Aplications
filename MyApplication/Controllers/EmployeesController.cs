using Bikes.Models;
using Bikes.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Bikes.Services;
using BikesApi.CommConstants;
using BikesApi.Entities;


namespace Bikes.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await EmployeeService.Instance.GetAllAsync<List<EmployeeResponse>>();

                if (response == null)
                    return BadRequest("Couldn't load bakers. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allEmployees = response.Select(employeeResponse => new Employee()
                {
                    Id = employeeResponse.Id,
                    FirstName = employeeResponse.FirstName,
                    LastName = employeeResponse.LastName,
                    EmailAddress = employeeResponse.EmailAddress,
                    Salary = employeeResponse.Salary,
                    IsFullTime = employeeResponse.IsFullTime,
                    RegisteredOn = employeeResponse.RegisteredOn,
                    TotalOrders = employeeResponse.TotalOrders,
                }).ToList();

                vm.Employees = allEmployees;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await EmployeeService.Instance.PostAsync<EmployeeResponse>(new CreateEmployeeRequest(addVM.FirstName, addVM.LastName, addVM.EmailAddress, addVM.Salary, addVM.IsFullTime, DateTime.Now));

                if (response == null)
                    return BadRequest("Couldn't add baker. Responce message from the server is null");    

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await EmployeeService.Instance.GetAsync<EmployeeResponse>(id.ToString());

            Employee employee = new Employee()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                EmailAddress = response.EmailAddress,
                Salary = response.Salary,
                IsFullTime = response.IsFullTime,
                RegisteredOn = response.RegisteredOn
            };
            EditVM vm = new EditVM();
            vm.Employee = employee;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await EmployeeService.Instance.PutAsync<OkResult>(vm.Employee.Id, new UpdateBakerRequest(vm.Employee.Id, vm.Employee.FirstName, vm.Employee.LastName, vm.Employee.EmailAddress, vm.Employee.Salary, vm.Employee.IsFullTime, vm.Employee.RegisteredOn));

                if (response == null)
                    return BadRequest("Couldn't edit baker. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await EmployeeService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Couldn't edit baker. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(string firstName)
        {
            try
            {
                var responseList = await EmployeeService.Instance.GetSearchAsync<List<EmployeeResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Couldn't add employee. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var EmployeesList = responseList.Select(response => new Employee()
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    EmailAddress = response.EmailAddress,
                    Salary = response.Salary,
                    IsFullTime = response.IsFullTime,
                    RegisteredOn = response.RegisteredOn,
                }).ToList();

                vm.Employees = EmployeesList;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
