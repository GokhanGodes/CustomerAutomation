using CustomerAutomationWeb.Models;
using CustomerAutomationWeb.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerAutomationWeb.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _service.GetAll();
            List<CustomerViewModel> list = new List<CustomerViewModel>();
            data?.ForEach(x =>
            {
                var viewModel = new CustomerViewModel
                {
                    Id = x.Id,
                    TCKN = x.TCKN.Replace(x.TCKN.Substring(0, 7), "*******"),
                    Name = x.Name.Replace(x.Name.Substring(2), "*****"),
                    LastName = x.LastName.Replace(x.LastName.Substring(2), "*****")
                };
                list.Add(viewModel);
            });
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(list);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var data = _service.Get(id);
            var viewModel = new CustomerViewModel
            {
                Address = data.Address,
                BirthDate = data.BirthDate,
                Id = data.Id,
                IsActive = data.IsActive,
                PhoneNumber = data.PhoneNumber,
                Name = data.Name,
                LastName = data.LastName,
                TCKN = data.TCKN
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var isDeleted = _service.Delete(id);
            if (isDeleted)
            {
                TempData["AlertMessage"] = "Customer deleted";
            }
            else
            {
                TempData["AlertMessage"] = "Customer not deleted";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerAddInput cusAdd)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var isCreated = _service.Create(cusAdd);
            if (isCreated)
            {
                TempData["AlertMessage"] = "Customer created";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.AlertMessage = "Customer not created, check that your credentials are correct";
                return View();
            }

        }

        [HttpGet]
        public IActionResult Search(CustomerFilterInput input)
        {
            var response = _service.Search(input);
            List<CustomerViewModel> list = new List<CustomerViewModel>();
            response?.ForEach(x =>
            {
                var viewModel = new CustomerViewModel
                {
                    Address = x.Address,
                    BirthDate = x.BirthDate,
                    IsActive = x.IsActive,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.Id,
                    TCKN = x.TCKN.Replace(x.TCKN.Substring(0, 7), "*******"),
                    Name = x.Name.Replace(x.Name.Substring(2), "*****"),
                    LastName = x.LastName.Replace(x.LastName.Substring(2), "*****")
                };
                list.Add(viewModel);
            });
            ViewData["Customers"] = list;

            return View();
        }

    }
}
