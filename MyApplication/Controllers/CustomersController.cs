using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bikes.Models;
using Bikes.Services;
using Bikes.ViewModels.Customers;
using BikesApi.CommConstants;
using BikesApi.Entities;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;

namespace Bikes.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

                if (response == null)
                    return BadRequest("Couldn't load customers. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allCustomers = new List<Customer>();

                foreach (var customerResponse in response)
                {
                    var customer = new Customer();
                    customer.Id = customerResponse.Id;
                    customer.FirstName = customerResponse.FirstName;
                    customer.LastName = customerResponse.LastName;
                    customer.Address = customerResponse.Address;
                    customer.AccountBalance = customerResponse.AccountBalance;
                    customer.DeluxeAccount = customerResponse.DeluxeAccount;
                    customer.RegisteredOn = customerResponse.RegisteredOn;
                    customer.TotalOrders = customerResponse.TotalOrders;
                    allCustomers.Add(customer);
                }

                vm.Customers = allCustomers;
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
                var response = await CustomerService.Instance.PostAsync<CustomerResponse>(new CreateCustomerRequest(addVM.FirstName, addVM.LastName, addVM.Address, addVM.AccountBalance, addVM.DeluxeAccount, DateTime.Now));

                if (response == null)
                    return BadRequest("Couldn't add customer. Responce message from the server is null");    

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
            var response = await CustomerService.Instance.GetAsync<CustomerResponse>(id.ToString());

            Customer customer = new Customer()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Address = response.Address,
                AccountBalance = response.AccountBalance,
                DeluxeAccount = response.DeluxeAccount,
                RegisteredOn = response.RegisteredOn
            };
            EditVM vm = new EditVM();
            vm.Customer = customer;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await CustomerService.Instance.PutAsync<OkResult>(vm.Customer.Id, new UpdateCustomerRequest(vm.Customer.Id, vm.Customer.FirstName, vm.Customer.LastName, vm.Customer.Address, vm.Customer.AccountBalance, vm.Customer.DeluxeAccount, vm.Customer.RegisteredOn));

                if (response == null)
                    return BadRequest("Couldn't edit customer. Responce message from the server is null");

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
                var response = await CustomerService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Couldn't edit customer. Responce message from the server is null");

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
                var responseList = await CustomerService.Instance.GetSearchAsync<List<CustomerResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Couldn't search for customers. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var customersList = responseList.Select(response => new Customer()
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    Address = response.Address,
                    AccountBalance = response.AccountBalance,
                    DeluxeAccount = response.DeluxeAccount,
                    RegisteredOn = response.RegisteredOn,
                }).ToList();

                vm.Customers = customersList;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}