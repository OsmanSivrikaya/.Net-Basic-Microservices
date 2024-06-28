using Microsoft.AspNetCore.Mvc;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Shared.BaseController
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}