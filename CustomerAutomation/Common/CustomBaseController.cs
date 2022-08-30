using Microsoft.AspNetCore.Mvc;

namespace CustomerAutomation.Common
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseMessages<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.Status
            };
        }
    }
}
