using CustomerAutomation.Common;
using CustomerAutomation.DTOs;
using CustomerAutomation.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAutomation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiLog]
    public class CustomerController : CustomBaseController
    {
        public readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult Add([FromBody] CustomerAddDto cusAddDto)
        {
            var response = _service.Create(cusAddDto);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public IActionResult Delete(CustomerDeleteDto cusDelDto)
        {
            var response = _service.Delete(cusDelDto);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [ResponseCache(Duration = 360000)]
        public IActionResult Get(int id)
        {
            var response = _service.Get(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [ResponseCache(Duration = 360000)]
        public IActionResult GetAll()
        {
            var response = _service.GetAll();
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        public IActionResult Search([FromQuery] CustomerFilterDto cusFilDto)
        {
            var response = _service.Search(cusFilDto);
            return CreateActionResultInstance(response);
        }
    }
}
